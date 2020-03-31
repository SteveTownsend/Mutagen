﻿using Mutagen.Bethesda.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutagen.Bethesda.Binary
{
    public struct HeaderExport : IDisposable
    {
        public readonly MutagenWriter Writer;
        public readonly long SizePosition;
        public readonly RecordHeaderConstants RecordConstants;

        private HeaderExport(
            MutagenWriter writer,
            long sizePosition,
            RecordHeaderConstants recordConstants)
        {
            this.Writer = writer;
            this.RecordConstants = recordConstants;
            this.SizePosition = sizePosition;
        }

        public static HeaderExport ExportHeader(
            MutagenWriter writer,
            RecordType record,
            ObjectType type)
        {
            writer.Write(record.TypeInt);
            var sizePosition = writer.Position;
            writer.Write(UtilityTranslation.Zeros.Slice(0, writer.Meta.Constants(type).LengthLength));
            return new HeaderExport(writer, sizePosition, writer.Meta.Constants(type));
        }

        public static HeaderExport ExportSubrecordHeader(
            MutagenWriter writer,
            RecordType record)
        {
            return ExportHeader(writer, record, ObjectType.Subrecord);
        }

        public void Dispose()
        {
            var endPos = this.Writer.Position;
            this.Writer.Position = this.SizePosition;
            var diff = endPos - this.Writer.Position;
            if (this.RecordConstants.HeaderIncludedInLength)
            {
                diff += Constants.HeaderLength;
            }
            else
            {
                diff -= this.RecordConstants.LengthAfterType;
            }

            switch (this.RecordConstants.ObjectType)
            {
                case ObjectType.Subrecord:
                    {
                        this.Writer.Write((ushort)diff);
                    }
                    break;
                case ObjectType.Record:
                case ObjectType.Group:
                    {
                        this.Writer.Write((uint)diff);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            this.Writer.Position = endPos;
        }
    }
}
