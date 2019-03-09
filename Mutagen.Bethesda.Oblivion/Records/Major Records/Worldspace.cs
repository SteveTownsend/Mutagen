﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Loqui;
using Loqui.Internal;
using Mutagen.Bethesda.Binary;
using Mutagen.Bethesda.Internals;
using Mutagen.Bethesda.Oblivion.Internals;
using Noggog;
using Noggog.Utility;

namespace Mutagen.Bethesda.Oblivion
{
    public partial class Worldspace
    {
        private byte[] _timeStamp;
        private bool usingOffsetLength;

        [Flags]
        public enum Flag
        {
            SmallWorld = 0x01,
            CantFastTravel = 0x02,
            OblivionWorldspace = 0x04,
            NoLODWater = 0x10,
        }

        private static readonly Worldspace_TranslationMask XmlFolderTranslation = new Worldspace_TranslationMask(true)
        {
            SubCells = new MaskItem<bool, WorldspaceBlock_TranslationMask>(false, null),
            Road = new MaskItem<bool, Road_TranslationMask>(false, null),
            TopCell = new MaskItem<bool, Cell_TranslationMask>(false, null),
        };
        private static readonly TranslationCrystal XmlFolderTranslationCrystal = XmlFolderTranslation.GetCrystal();

        private static WorldspaceBlock_CopyMask duplicateBlockCopyMask = new WorldspaceBlock_CopyMask(true)
        {
            Items = new MaskItem<CopyOption, WorldspaceSubBlock_CopyMask>(CopyOption.Skip, null)
        };

        private static WorldspaceSubBlock_CopyMask duplicateSubBlockCopyMask = new WorldspaceSubBlock_CopyMask(true)
        {
            Items = new MaskItem<CopyOption, Cell_CopyMask>(CopyOption.Skip, null)
        };

        partial void PostDuplicate(Worldspace obj, Worldspace rhs, Func<FormKey> getNextFormKey, IList<(MajorRecord Record, FormKey OriginalFormKey)> duplicatedRecords)
        {
            if (rhs.Road_IsSet
                && rhs.Road != null)
            {
                obj.Road = (Road)rhs.Road.Duplicate(getNextFormKey, duplicatedRecords);
            }
            if (rhs.TopCell_IsSet
                && rhs.TopCell != null)
            {
                obj.TopCell = (Cell)rhs.TopCell.Duplicate(getNextFormKey, duplicatedRecords);
            }
            obj.SubCells.SetTo(rhs.SubCells.Items.Select((block) =>
            {
                var blockRet = new WorldspaceBlock();
                blockRet.CopyFieldsFrom(block, duplicateBlockCopyMask);
                blockRet.Items.SetTo(block.Items.Select((subBlock) =>
                {
                    var subBlockRet = new WorldspaceSubBlock();
                    subBlockRet.CopyFieldsFrom(subBlock, duplicateSubBlockCopyMask);
                    subBlockRet.Items.SetTo(subBlock.Items.Select(c => (Cell)c.Duplicate(getNextFormKey, duplicatedRecords)));
                    return subBlockRet;
                }));
                return blockRet;
            }));
        }

        static partial void FillBinary_OffsetLength_Custom(MutagenFrame frame, Worldspace item, MasterReferences masterReferences, ErrorMaskBuilder errorMask)
        {
            item.usingOffsetLength = true;
            if (!HeaderTranslation.ReadNextSubRecordType(frame.Reader, out var xLen).Equals(Worldspace_Registration.XXXX_HEADER)
                || xLen != 4)
            {
                errorMask.ReportExceptionOrThrow(new ArgumentException());
                return;
            }
            var contentLen = frame.Reader.ReadInt32();
            if (!HeaderTranslation.ReadNextSubRecordType(frame.Reader, out var oLen).Equals(Worldspace_Registration.OFST_HEADER)
                || oLen != 0)
            {
                errorMask.ReportExceptionOrThrow(new ArgumentException());
                return;
            }
            item.OffsetData = frame.Reader.ReadBytes(contentLen);
        }

        static partial void WriteBinary_OffsetLength_Custom(MutagenWriter writer, Worldspace item, MasterReferences masterReferences, ErrorMaskBuilder errorMask)
        {
            if (!item.OffsetData_IsSet) return;
            if (!item.usingOffsetLength) return;
            using (HeaderExport.ExportSubRecordHeader(writer, Worldspace_Registration.XXXX_HEADER))
            {
                writer.Write(item.OffsetData.Length);
            }
            writer.Write(Worldspace_Registration.OFST_HEADER.Type);
            writer.WriteZeros(2);
            writer.Write(item.OffsetData);
        }

        static partial void FillBinary_OffsetData_Custom(MutagenFrame frame, Worldspace item, MasterReferences masterReferences, ErrorMaskBuilder errorMask)
        {
            if (item.usingOffsetLength) return;
            if (!HeaderTranslation.ReadNextSubRecordType(frame.Reader, out var len).Equals(Worldspace_Registration.OFST_HEADER))
            {
                throw new ArgumentException();
            }
            item.OffsetData = frame.Reader.ReadBytes(len);
        }

        static partial void WriteBinary_OffsetData_Custom(MutagenWriter writer, Worldspace item, MasterReferences masterReferences, ErrorMaskBuilder errorMask)
        {
            if (item.usingOffsetLength) return;
            if (!item.OffsetData_IsSet) return;
            using (HeaderExport.ExportSubRecordHeader(writer, Worldspace_Registration.OFST_HEADER))
            {
                ByteArrayBinaryTranslation.Instance.Write(writer, item.OffsetData);
            }
        }

        static partial void CustomBinaryEnd_Import(MutagenFrame frame, Worldspace obj, MasterReferences masterReferences, ErrorMaskBuilder errorMask)
        {
            if (frame.Reader.Complete) return;
            var next = HeaderTranslation.GetNextType(frame.Reader, out var len, hopGroup: false);
            if (!next.Equals(Group_Registration.GRUP_HEADER)) return;
            frame.Reader.Position += 8;
            var formKey = FormKey.Factory(masterReferences, frame.Reader.ReadUInt32());
            var grupType = (GroupTypeEnum)frame.Reader.ReadInt32();
            if (grupType == GroupTypeEnum.WorldChildren)
            {
                obj._timeStamp = frame.Reader.ReadBytes(4);
                if (formKey != obj.FormKey)
                {
                    errorMask.ReportExceptionOrThrow(
                        new ArgumentException("Cell children group did not match the FormID of the parent worldspace."));
                    return;
                }
            }
            else
            {
                frame.Reader.Position -= 16;
                return;
            }
            using (var subFrame = MutagenFrame.ByLength(frame.Reader, len - 20))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (subFrame.Complete) return;
                    var subType = HeaderTranslation.ReadNextSubRecordType(frame.Reader, out var subLen);
                    subFrame.Reader.Position -= 6;
                    switch (subType.TypeInt)
                    {
                        case 0x44414F52: // "ROAD":
                            if (LoquiBinaryTranslation<Road>.Instance.Parse(
                                frame: subFrame,
                                item: out var road,
                                fieldIndex: (int)Worldspace_FieldIndex.Road,
                                masterReferences: masterReferences,
                                errorMask: errorMask))
                            {
                                obj.Road = road;
                            }
                            else
                            {
                                obj.Road_Unset();
                            }
                            break;
                        case 0x4C4C4543: // "CELL":
                            if (LoquiBinaryTranslation<Cell>.Instance.Parse(
                                frame: subFrame,
                                item: out var topCell,
                                fieldIndex: (int)Worldspace_FieldIndex.TopCell,
                                masterReferences: masterReferences,
                                errorMask: errorMask))
                            {
                                obj.TopCell = topCell;
                            }
                            else
                            {
                                obj.TopCell_Unset();
                            }
                            break;
                        case 0x50555247: // "GRUP":
                            Mutagen.Bethesda.Binary.ListBinaryTranslation<WorldspaceBlock>.Instance.ParseRepeatedItem(
                                frame: frame,
                                item: obj._SubCells,
                                triggeringRecord: Worldspace_Registration.GRUP_HEADER,
                                fieldIndex: (int)Worldspace_FieldIndex.SubCells,
                                lengthLength: Mutagen.Bethesda.Constants.RECORD_LENGTHLENGTH,
                                errorMask: errorMask,
                                transl: (MutagenFrame r, out WorldspaceBlock block, ErrorMaskBuilder subErrorMask) =>
                                {
                                    return LoquiBinaryTranslation<WorldspaceBlock>.Instance.Parse(
                                        frame: r.Spawn(snapToFinalPosition: false),
                                        item: out block,
                                        masterReferences: masterReferences,
                                        errorMask: errorMask);
                                });
                            break;
                        default:
                            return;
                    }
                }
            }
        }

        static partial void CustomBinaryEnd_Export(MutagenWriter writer, Worldspace obj, MasterReferences masterReferences, ErrorMaskBuilder errorMask)
        {
            if (obj._SubCells.Count == 0
                && !obj.Road_IsSet
                && !obj.TopCell_IsSet) return;
            using (HeaderExport.ExportHeader(writer, Group_Registration.GRUP_HEADER, ObjectType.Group))
            {
                FormKeyBinaryTranslation.Instance.Write(
                    writer,
                    obj.FormKey,
                    masterReferences,
                    errorMask);
                writer.Write((int)GroupTypeEnum.WorldChildren);
                if (obj._timeStamp != null)
                {
                    writer.Write(obj._timeStamp);
                }
                else
                {
                    writer.WriteZeros(4);
                }

                if (obj.Road_IsSet)
                {
                    LoquiBinaryTranslation<Road>.Instance.Write(
                        writer,
                        obj.Road,
                        (int)Worldspace_FieldIndex.Road,
                        masterReferences: masterReferences,
                        errorMask: errorMask);
                }
                if (obj.TopCell_IsSet)
                {
                    LoquiBinaryTranslation<Cell>.Instance.Write(
                        writer,
                        obj.TopCell,
                        (int)Worldspace_FieldIndex.TopCell,
                        masterReferences: masterReferences,
                        errorMask: errorMask);
                }
                Mutagen.Bethesda.Binary.ListBinaryTranslation<WorldspaceBlock>.Instance.Write(
                    writer: writer,
                    items: obj.SubCells,
                    fieldIndex: (int)Worldspace_FieldIndex.SubCells,
                    errorMask: errorMask,
                    transl: (MutagenWriter subWriter, WorldspaceBlock subItem, ErrorMaskBuilder listSubMask) =>
                    {
                        LoquiBinaryTranslation<WorldspaceBlock>.Instance.Write(
                            writer: subWriter,
                            item: subItem,
                            masterReferences: masterReferences,
                            errorMask: listSubMask);
                    });
            }
        }

        public static bool TryCreate_Xml_Folder(
            DirectoryPath dir,
            out Worldspace ret,
            ErrorMaskBuilder errorMask)
        {
            var path = Path.Combine(dir.Path, $"{nameof(Worldspace)}.xml");
            if (!File.Exists(path))
            {
                ret = default;
                return false;
            }
            ret = Create_Xml(
                path: path,
                errorMask: errorMask,
                translationMask: XmlFolderTranslation);
            var roadPath = Path.Combine(dir.Path, $"{nameof(Road)}.xml");
            if (File.Exists(roadPath))
            {
                ret.Road = Road.Create_Xml(
                    roadPath,
                    translationMask: null,
                    errorMask: errorMask);
            }
            var topCellpath = Path.Combine(dir.Path, $"{nameof(TopCell)}.xml");
            if (File.Exists(roadPath))
            {
                ret.TopCell = Cell.Create_Xml(
                    topCellpath,
                    translationMask: null,
                    errorMask: errorMask);
            }
            var subCellsDir = new DirectoryPath(Path.Combine(dir.Path, $"SubCells"));
            if (!subCellsDir.Exists) return true;

            bool TryGetIndices(string str, out short x, out short y)
            {
                var split = str.Split(',');
                x = -1;
                y = -1;
                if (split.Length != 2) return false;
                split[0] = split[0].Trim();
                split[1] = split[1].Trim();
                if (split[0].Length < 2 || split[1].Length < 2) return false;
                if (!split[0].EndsWith("Y")) return false;
                if (!split[1].EndsWith("X")) return false;
                return short.TryParse(split[0].Substring(0, split[0].Length - 1), out y)
                    && short.TryParse(split[1].Substring(0, split[1].Length - 1), out x);
            }

            foreach (var blockDir in subCellsDir.EnumerateDirectories(includeSelf: false, recursive: false))
            {
                if (!TryGetIndices(blockDir.Name, out var blockX, out var blockY)) continue;
                WorldspaceBlock wb = new WorldspaceBlock()
                {
                    BlockNumberX = blockX,
                    BlockNumberY = blockY,
                };
                ret.SubCells.Add(wb);
                foreach (var subBlockFile in blockDir.EnumerateFiles())
                {
                    if (!TryGetIndices(subBlockFile.NameWithoutExtension, out var subBlockX, out var subBlockY)) continue;
                    WorldspaceSubBlock wsb = WorldspaceSubBlock.Create_Xml(
                        path: subBlockFile.Path,
                        errorMask: errorMask,
                        translationMask: null);
                    wsb.BlockNumberX = subBlockX;
                    wsb.BlockNumberY = subBlockY;
                    wb.Items.Add(wsb);
                }
            }
            return true;
        }

        public override void Write_Xml_Folder(
            DirectoryPath? dir, 
            string name, 
            XElement node, 
            int counter,
            ErrorMaskBuilder errorMask)
        {
            dir = new DirectoryPath(Path.Combine(dir.Value.Path, $"{counter} - {this.FormKey.IDString()} - {this.EditorID}"));
            dir.Value.Create();

            Write_Xml(
                path: Path.Combine(dir.Value.Path, $"{nameof(Worldspace)}.xml"),
                errorMask: errorMask,
                translationMask: XmlFolderTranslationCrystal,
                name: null);
            if (this.Road_IsSet
                && this.Road != null)
            {
                this.Road.Write_Xml(
                    path: Path.Combine(dir.Value.Path, $"{nameof(Road)}.xml"),
                    errorMask: errorMask,
                    translationMask: null);
            }
            if (this.TopCell_IsSet
                && this.TopCell != null)
            {
                this.TopCell.Write_Xml(
                    path: Path.Combine(dir.Value.Path, $"{nameof(TopCell)}.xml"),
                    errorMask: errorMask,
                    translationMask: null);
            }
            foreach (var block in this.SubCellsEnumerable)
            {
                foreach (var subBlock in block.Items)
                {
                    if (subBlock.Items.Count == 0) continue;
                    var parentDir = new DirectoryInfo(Path.Combine(dir.Value.Path, $"SubCells/{block.BlockNumberY}Y, {block.BlockNumberX}X/"));
                    parentDir.Create();
                    subBlock.Write_Xml(
                        path: Path.Combine(parentDir.FullName, $"{subBlock.BlockNumberY}Y, {subBlock.BlockNumberX}X.xml"),
                        translationMask: null,
                        errorMask: errorMask);
                }
            }
        }
    }
}
