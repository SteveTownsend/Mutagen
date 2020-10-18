using Loqui;
using Loqui.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutagen.Bethesda.Generation
{
    /// <summary>
    /// One of the harder parts to offer via natural intellisense is informing all the viable options
    /// a IMajorRecordGetter generic might allow, for example.
    /// 
    /// This module is in charge of generating extension methods and other tricks to help the compiler 
    /// help the user know the options available to them.
    /// </summary>
    public class TypeOptionSolidifier : GenerationModule
    {
        public override async Task FinalizeGeneration(ProtocolGeneration proto)
        {
            if (proto.Protocol.Namespace.Equals("Bethesda")) return;
            bool generate = false;
            FileGeneration fg = new FileGeneration();

            fg.AppendLine("using System.Collections.Generic;");
            using (var n = new NamespaceWrapper(fg, proto.DefaultNamespace))
            {
                using (var c = new ClassWrapper(fg, "TypeOptionSolidifierMixIns"))
                {
                    c.Static = true;
                }
                using (new BraceWrapper(fg))
                {
                    using (new RegionWrapper(fg, "Normal"))
                    {
                        foreach (var obj in proto.ObjectGenerationsByName
                            .OrderBy(x => x.Key)
                            .Select(x => x.Value))
                        {
                            if (!await obj.IsMajorRecord()) continue;

                            using (var args = new FunctionWrapper(fg,
                                $"public static TypedLoadOrderAccess<{obj.Interface(getter: true)}> {obj.Name}"))
                            {
                                args.Add($"this IEnumerable<IModListing<I{proto.Protocol.Namespace}ModGetter>> listings");
                                args.Add("bool includeDeletedRecords = false");
                            }
                            using (new BraceWrapper(fg))
                            {
                                fg.AppendLine($"return new TypedLoadOrderAccess<{obj.Interface(getter: true)}>(() => listings.WinningOverrides<{obj.Interface(getter: true)}>(includeDeletedRecords: includeDeletedRecords));");
                            }
                            fg.AppendLine();

                            using (var args = new FunctionWrapper(fg,
                                $"public static TypedLoadOrderAccess<{obj.Interface(getter: true)}> {obj.Name}"))
                            {
                                args.Add($"this IEnumerable<I{proto.Protocol.Namespace}ModGetter> mods");
                                args.Add("bool includeDeletedRecords = false");
                            }
                            using (new BraceWrapper(fg))
                            {
                                fg.AppendLine($"return new TypedLoadOrderAccess<{obj.Interface(getter: true)}>(() => mods.WinningOverrides<{obj.Interface(getter: true)}>(includeDeletedRecords: includeDeletedRecords));");
                            }
                            fg.AppendLine();
                            generate = true;
                        }
                    }

                    using (new RegionWrapper(fg, "Link Interfaces"))
                    {
                        if (LinkInterfaceModule.ObjectMappings.TryGetValue(proto.Protocol, out var interfs))
                        {
                            foreach (var interf in interfs)
                            {
                                var getter = $"{interf.Key}Getter";
                                using (var args = new FunctionWrapper(fg,
                                    $"public static TypedLoadOrderAccess<{getter}> {interf.Key}"))
                                {
                                    args.Add($"this IEnumerable<IModListing<I{proto.Protocol.Namespace}ModGetter>> listings");
                                    args.Add("bool includeDeletedRecords = false");
                                }
                                using (new BraceWrapper(fg))
                                {
                                    fg.AppendLine($"return new TypedLoadOrderAccess<{getter}>(() => listings.WinningOverrides<{getter}>(includeDeletedRecords: includeDeletedRecords));");
                                }
                                fg.AppendLine();

                                using (var args = new FunctionWrapper(fg,
                                    $"public static TypedLoadOrderAccess<{getter}> {interf.Key}"))
                                {
                                    args.Add($"this IEnumerable<I{proto.Protocol.Namespace}ModGetter> mods");
                                    args.Add("bool includeDeletedRecords = false");
                                }
                                using (new BraceWrapper(fg))
                                {
                                    fg.AppendLine($"return new TypedLoadOrderAccess<{getter}>(() => mods.WinningOverrides<{getter}>(includeDeletedRecords: includeDeletedRecords));");
                                }
                                fg.AppendLine();
                            }
                        }
                    }
                }
            }

            if (!generate) return;
            var path = Path.Combine(proto.DefFileLocation.FullName, $"TypeSolidifier{Loqui.Generation.Constants.AutogeneratedMarkerString}.cs");
            fg.Generate(path);
            proto.GeneratedFiles.Add(path, ProjItemType.Compile);
        }
    }
}
