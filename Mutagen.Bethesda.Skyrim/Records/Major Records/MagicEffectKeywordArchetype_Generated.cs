/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Loqui.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
#region Usings
using Loqui;
using Loqui.Internal;
using Mutagen.Bethesda.Binary;
using Mutagen.Bethesda.Internals;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Skyrim.Internals;
using Noggog;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
#endregion

#nullable enable
namespace Mutagen.Bethesda.Skyrim
{
    #region Class
    public partial class MagicEffectKeywordArchetype :
        MagicEffectArchetype,
        IEquatable<IMagicEffectKeywordArchetypeGetter>,
        ILoquiObjectSetter<MagicEffectKeywordArchetype>,
        IMagicEffectKeywordArchetypeInternal
    {

        #region To String

        public override void ToString(
            FileGeneration fg,
            string? name = null)
        {
            MagicEffectKeywordArchetypeMixIn.ToString(
                item: this,
                name: name);
        }

        #endregion

        #region Equals and Hash
        public override bool Equals(object? obj)
        {
            if (!(obj is IMagicEffectKeywordArchetypeGetter rhs)) return false;
            return ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)this).CommonInstance()!).Equals(this, rhs);
        }

        public bool Equals(IMagicEffectKeywordArchetypeGetter? obj)
        {
            return ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)this).CommonInstance()!).Equals(this, obj);
        }

        public override int GetHashCode() => ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)this).CommonInstance()!).GetHashCode(this);

        #endregion

        #region Mask
        public new class Mask<TItem> :
            MagicEffectArchetype.Mask<TItem>,
            IEquatable<Mask<TItem>>,
            IMask<TItem>
        {
            #region Ctors
            public Mask(TItem initialValue)
            : base(initialValue)
            {
            }

            public Mask(
                TItem Type,
                TItem AssociationKey,
                TItem ActorValue)
            : base(
                Type: Type,
                AssociationKey: AssociationKey,
                ActorValue: ActorValue)
            {
            }

            #pragma warning disable CS8618
            protected Mask()
            {
            }
            #pragma warning restore CS8618

            #endregion

            #region Equals
            public override bool Equals(object? obj)
            {
                if (!(obj is Mask<TItem> rhs)) return false;
                return Equals(rhs);
            }

            public bool Equals(Mask<TItem>? rhs)
            {
                if (rhs == null) return false;
                if (!base.Equals(rhs)) return false;
                return true;
            }
            public override int GetHashCode()
            {
                var hash = new HashCode();
                hash.Add(base.GetHashCode());
                return hash.ToHashCode();
            }

            #endregion

            #region All
            public override bool All(Func<TItem, bool> eval)
            {
                if (!base.All(eval)) return false;
                return true;
            }
            #endregion

            #region Any
            public override bool Any(Func<TItem, bool> eval)
            {
                if (base.Any(eval)) return true;
                return false;
            }
            #endregion

            #region Translate
            public new Mask<R> Translate<R>(Func<TItem, R> eval)
            {
                var ret = new MagicEffectKeywordArchetype.Mask<R>();
                this.Translate_InternalFill(ret, eval);
                return ret;
            }

            protected void Translate_InternalFill<R>(Mask<R> obj, Func<TItem, R> eval)
            {
                base.Translate_InternalFill(obj, eval);
            }
            #endregion

            #region To String
            public override string ToString()
            {
                return ToString(printMask: null);
            }

            public string ToString(MagicEffectKeywordArchetype.Mask<bool>? printMask = null)
            {
                var fg = new FileGeneration();
                ToString(fg, printMask);
                return fg.ToString();
            }

            public void ToString(FileGeneration fg, MagicEffectKeywordArchetype.Mask<bool>? printMask = null)
            {
                fg.AppendLine($"{nameof(MagicEffectKeywordArchetype.Mask<TItem>)} =>");
                fg.AppendLine("[");
                using (new DepthWrapper(fg))
                {
                }
                fg.AppendLine("]");
            }
            #endregion

        }

        public new class ErrorMask :
            MagicEffectArchetype.ErrorMask,
            IErrorMask<ErrorMask>
        {
            #region IErrorMask
            public override object? GetNthMask(int index)
            {
                MagicEffectKeywordArchetype_FieldIndex enu = (MagicEffectKeywordArchetype_FieldIndex)index;
                switch (enu)
                {
                    default:
                        return base.GetNthMask(index);
                }
            }

            public override void SetNthException(int index, Exception ex)
            {
                MagicEffectKeywordArchetype_FieldIndex enu = (MagicEffectKeywordArchetype_FieldIndex)index;
                switch (enu)
                {
                    default:
                        base.SetNthException(index, ex);
                        break;
                }
            }

            public override void SetNthMask(int index, object obj)
            {
                MagicEffectKeywordArchetype_FieldIndex enu = (MagicEffectKeywordArchetype_FieldIndex)index;
                switch (enu)
                {
                    default:
                        base.SetNthMask(index, obj);
                        break;
                }
            }

            public override bool IsInError()
            {
                if (Overall != null) return true;
                return false;
            }
            #endregion

            #region To String
            public override string ToString()
            {
                var fg = new FileGeneration();
                ToString(fg, null);
                return fg.ToString();
            }

            public override void ToString(FileGeneration fg, string? name = null)
            {
                fg.AppendLine($"{(name ?? "ErrorMask")} =>");
                fg.AppendLine("[");
                using (new DepthWrapper(fg))
                {
                    if (this.Overall != null)
                    {
                        fg.AppendLine("Overall =>");
                        fg.AppendLine("[");
                        using (new DepthWrapper(fg))
                        {
                            fg.AppendLine($"{this.Overall}");
                        }
                        fg.AppendLine("]");
                    }
                    ToString_FillInternal(fg);
                }
                fg.AppendLine("]");
            }
            protected override void ToString_FillInternal(FileGeneration fg)
            {
                base.ToString_FillInternal(fg);
            }
            #endregion

            #region Combine
            public ErrorMask Combine(ErrorMask? rhs)
            {
                if (rhs == null) return this;
                var ret = new ErrorMask();
                return ret;
            }
            public static ErrorMask? Combine(ErrorMask? lhs, ErrorMask? rhs)
            {
                if (lhs != null && rhs != null) return lhs.Combine(rhs);
                return lhs ?? rhs;
            }
            #endregion

            #region Factory
            public static new ErrorMask Factory(ErrorMaskBuilder errorMask)
            {
                return new ErrorMask();
            }
            #endregion

        }
        public new class TranslationMask :
            MagicEffectArchetype.TranslationMask,
            ITranslationMask
        {
            #region Ctors
            public TranslationMask(
                bool defaultOn,
                bool onOverall = true)
                : base(defaultOn, onOverall)
            {
            }

            #endregion

            public static implicit operator TranslationMask(bool defaultOn)
            {
                return new TranslationMask(defaultOn: defaultOn, onOverall: defaultOn);
            }

        }
        #endregion

        #region Binary Translation
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected override object BinaryWriteTranslator => MagicEffectKeywordArchetypeBinaryWriteTranslation.Instance;
        void IBinaryItem.WriteToBinary(
            MutagenWriter writer,
            RecordTypeConverter? recordTypeConverter = null)
        {
            ((MagicEffectKeywordArchetypeBinaryWriteTranslation)this.BinaryWriteTranslator).Write(
                item: this,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }
        #region Binary Create
        public new static MagicEffectKeywordArchetype CreateFromBinary(
            MutagenFrame frame,
            RecordTypeConverter? recordTypeConverter = null)
        {
            var ret = new MagicEffectKeywordArchetype();
            ((MagicEffectKeywordArchetypeSetterCommon)((IMagicEffectKeywordArchetypeGetter)ret).CommonSetterInstance()!).CopyInFromBinary(
                item: ret,
                frame: frame,
                recordTypeConverter: recordTypeConverter);
            return ret;
        }

        #endregion

        public static bool TryCreateFromBinary(
            MutagenFrame frame,
            out MagicEffectKeywordArchetype item,
            RecordTypeConverter? recordTypeConverter = null)
        {
            var startPos = frame.Position;
            item = CreateFromBinary(frame, recordTypeConverter);
            return startPos != frame.Position;
        }
        #endregion

        void IPrintable.ToString(FileGeneration fg, string? name) => this.ToString(fg, name);

        void IClearable.Clear()
        {
            ((MagicEffectKeywordArchetypeSetterCommon)((IMagicEffectKeywordArchetypeGetter)this).CommonSetterInstance()!).Clear(this);
        }

        internal static new MagicEffectKeywordArchetype GetNew()
        {
            return new MagicEffectKeywordArchetype();
        }

    }
    #endregion

    #region Interface
    public partial interface IMagicEffectKeywordArchetype :
        ILoquiObjectSetter<IMagicEffectKeywordArchetypeInternal>,
        IMagicEffectArchetypeInternal,
        IMagicEffectKeywordArchetypeGetter
    {
    }

    public partial interface IMagicEffectKeywordArchetypeInternal :
        IMagicEffectArchetypeInternal,
        IMagicEffectKeywordArchetype,
        IMagicEffectKeywordArchetypeGetter
    {
    }

    public partial interface IMagicEffectKeywordArchetypeGetter :
        IMagicEffectArchetypeGetter,
        IBinaryItem,
        ILoquiObject<IMagicEffectKeywordArchetypeGetter>
    {
        static new ILoquiRegistration Registration => MagicEffectKeywordArchetype_Registration.Instance;

    }

    #endregion

    #region Common MixIn
    public static partial class MagicEffectKeywordArchetypeMixIn
    {
        public static void Clear(this IMagicEffectKeywordArchetypeInternal item)
        {
            ((MagicEffectKeywordArchetypeSetterCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonSetterInstance()!).Clear(item: item);
        }

        public static MagicEffectKeywordArchetype.Mask<bool> GetEqualsMask(
            this IMagicEffectKeywordArchetypeGetter item,
            IMagicEffectKeywordArchetypeGetter rhs,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            return ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).GetEqualsMask(
                item: item,
                rhs: rhs,
                include: include);
        }

        public static string ToString(
            this IMagicEffectKeywordArchetypeGetter item,
            string? name = null,
            MagicEffectKeywordArchetype.Mask<bool>? printMask = null)
        {
            return ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).ToString(
                item: item,
                name: name,
                printMask: printMask);
        }

        public static void ToString(
            this IMagicEffectKeywordArchetypeGetter item,
            FileGeneration fg,
            string? name = null,
            MagicEffectKeywordArchetype.Mask<bool>? printMask = null)
        {
            ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).ToString(
                item: item,
                fg: fg,
                name: name,
                printMask: printMask);
        }

        public static bool Equals(
            this IMagicEffectKeywordArchetypeGetter item,
            IMagicEffectKeywordArchetypeGetter rhs)
        {
            return ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).Equals(
                lhs: item,
                rhs: rhs);
        }

        public static void DeepCopyIn(
            this IMagicEffectKeywordArchetypeInternal lhs,
            IMagicEffectKeywordArchetypeGetter rhs,
            out MagicEffectKeywordArchetype.ErrorMask errorMask,
            MagicEffectKeywordArchetype.TranslationMask? copyMask = null)
        {
            var errorMaskBuilder = new ErrorMaskBuilder();
            ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: errorMaskBuilder,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: false);
            errorMask = MagicEffectKeywordArchetype.ErrorMask.Factory(errorMaskBuilder);
        }

        public static void DeepCopyIn(
            this IMagicEffectKeywordArchetypeInternal lhs,
            IMagicEffectKeywordArchetypeGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask)
        {
            ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: false);
        }

        public static MagicEffectKeywordArchetype DeepCopy(
            this IMagicEffectKeywordArchetypeGetter item,
            MagicEffectKeywordArchetype.TranslationMask? copyMask = null)
        {
            return ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask);
        }

        public static MagicEffectKeywordArchetype DeepCopy(
            this IMagicEffectKeywordArchetypeGetter item,
            out MagicEffectKeywordArchetype.ErrorMask errorMask,
            MagicEffectKeywordArchetype.TranslationMask? copyMask = null)
        {
            return ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask,
                errorMask: out errorMask);
        }

        public static MagicEffectKeywordArchetype DeepCopy(
            this IMagicEffectKeywordArchetypeGetter item,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask = null)
        {
            return ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask,
                errorMask: errorMask);
        }

        #region Binary Translation
        public static void CopyInFromBinary(
            this IMagicEffectKeywordArchetypeInternal item,
            MutagenFrame frame,
            RecordTypeConverter? recordTypeConverter = null)
        {
            ((MagicEffectKeywordArchetypeSetterCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonSetterInstance()!).CopyInFromBinary(
                item: item,
                frame: frame,
                recordTypeConverter: recordTypeConverter);
        }

        #endregion

    }
    #endregion

}

namespace Mutagen.Bethesda.Skyrim.Internals
{
    #region Field Index
    public enum MagicEffectKeywordArchetype_FieldIndex
    {
        Type = 0,
        AssociationKey = 1,
        ActorValue = 2,
    }
    #endregion

    #region Registration
    public partial class MagicEffectKeywordArchetype_Registration : ILoquiRegistration
    {
        public static readonly MagicEffectKeywordArchetype_Registration Instance = new MagicEffectKeywordArchetype_Registration();

        public static ProtocolKey ProtocolKey => ProtocolDefinition_Skyrim.ProtocolKey;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_Skyrim.ProtocolKey,
            msgID: 123,
            version: 0);

        public const string GUID = "3675b91e-c3b8-491e-a449-d3891f88c467";

        public const ushort AdditionalFieldCount = 0;

        public const ushort FieldCount = 3;

        public static readonly Type MaskType = typeof(MagicEffectKeywordArchetype.Mask<>);

        public static readonly Type ErrorMaskType = typeof(MagicEffectKeywordArchetype.ErrorMask);

        public static readonly Type ClassType = typeof(MagicEffectKeywordArchetype);

        public static readonly Type GetterType = typeof(IMagicEffectKeywordArchetypeGetter);

        public static readonly Type? InternalGetterType = null;

        public static readonly Type SetterType = typeof(IMagicEffectKeywordArchetype);

        public static readonly Type? InternalSetterType = typeof(IMagicEffectKeywordArchetypeInternal);

        public const string FullName = "Mutagen.Bethesda.Skyrim.MagicEffectKeywordArchetype";

        public const string Name = "MagicEffectKeywordArchetype";

        public const string Namespace = "Mutagen.Bethesda.Skyrim";

        public const byte GenericCount = 0;

        public static readonly Type? GenericRegistrationType = null;

        public static readonly Type BinaryWriteTranslation = typeof(MagicEffectKeywordArchetypeBinaryWriteTranslation);
        #region Interface
        ProtocolKey ILoquiRegistration.ProtocolKey => ProtocolKey;
        ObjectKey ILoquiRegistration.ObjectKey => ObjectKey;
        string ILoquiRegistration.GUID => GUID;
        ushort ILoquiRegistration.FieldCount => FieldCount;
        ushort ILoquiRegistration.AdditionalFieldCount => AdditionalFieldCount;
        Type ILoquiRegistration.MaskType => MaskType;
        Type ILoquiRegistration.ErrorMaskType => ErrorMaskType;
        Type ILoquiRegistration.ClassType => ClassType;
        Type ILoquiRegistration.SetterType => SetterType;
        Type? ILoquiRegistration.InternalSetterType => InternalSetterType;
        Type ILoquiRegistration.GetterType => GetterType;
        Type? ILoquiRegistration.InternalGetterType => InternalGetterType;
        string ILoquiRegistration.FullName => FullName;
        string ILoquiRegistration.Name => Name;
        string ILoquiRegistration.Namespace => Namespace;
        byte ILoquiRegistration.GenericCount => GenericCount;
        Type? ILoquiRegistration.GenericRegistrationType => GenericRegistrationType;
        ushort? ILoquiRegistration.GetNameIndex(StringCaseAgnostic name) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsEnumerable(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsLoqui(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsSingleton(ushort index) => throw new NotImplementedException();
        string ILoquiRegistration.GetNthName(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.IsNthDerivative(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.IsProtected(ushort index) => throw new NotImplementedException();
        Type ILoquiRegistration.GetNthType(ushort index) => throw new NotImplementedException();
        #endregion

    }
    #endregion

    #region Common
    public partial class MagicEffectKeywordArchetypeSetterCommon : MagicEffectArchetypeSetterCommon
    {
        public new static readonly MagicEffectKeywordArchetypeSetterCommon Instance = new MagicEffectKeywordArchetypeSetterCommon();

        partial void ClearPartial();
        
        public void Clear(IMagicEffectKeywordArchetypeInternal item)
        {
            ClearPartial();
            base.Clear(item);
        }
        
        public override void Clear(IMagicEffectArchetypeInternal item)
        {
            Clear(item: (IMagicEffectKeywordArchetypeInternal)item);
        }
        
        #region Mutagen
        public void RemapLinks(IMagicEffectKeywordArchetype obj, IReadOnlyDictionary<FormKey, FormKey> mapping)
        {
        }
        
        #endregion
        
        #region Binary Translation
        public virtual void CopyInFromBinary(
            IMagicEffectKeywordArchetypeInternal item,
            MutagenFrame frame,
            RecordTypeConverter? recordTypeConverter = null)
        {
            UtilityTranslation.SubrecordParse(
                record: item,
                frame: frame,
                recordTypeConverter: recordTypeConverter,
                fillStructs: MagicEffectKeywordArchetypeBinaryCreateTranslation.FillBinaryStructs);
        }
        
        public override void CopyInFromBinary(
            IMagicEffectArchetypeInternal item,
            MutagenFrame frame,
            RecordTypeConverter? recordTypeConverter = null)
        {
            CopyInFromBinary(
                item: (MagicEffectKeywordArchetype)item,
                frame: frame,
                recordTypeConverter: recordTypeConverter);
        }
        
        #endregion
        
    }
    public partial class MagicEffectKeywordArchetypeCommon : MagicEffectArchetypeCommon
    {
        public new static readonly MagicEffectKeywordArchetypeCommon Instance = new MagicEffectKeywordArchetypeCommon();

        public MagicEffectKeywordArchetype.Mask<bool> GetEqualsMask(
            IMagicEffectKeywordArchetypeGetter item,
            IMagicEffectKeywordArchetypeGetter rhs,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            var ret = new MagicEffectKeywordArchetype.Mask<bool>(false);
            ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).FillEqualsMask(
                item: item,
                rhs: rhs,
                ret: ret,
                include: include);
            return ret;
        }
        
        public void FillEqualsMask(
            IMagicEffectKeywordArchetypeGetter item,
            IMagicEffectKeywordArchetypeGetter rhs,
            MagicEffectKeywordArchetype.Mask<bool> ret,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            if (rhs == null) return;
            base.FillEqualsMask(item, rhs, ret, include);
        }
        
        public string ToString(
            IMagicEffectKeywordArchetypeGetter item,
            string? name = null,
            MagicEffectKeywordArchetype.Mask<bool>? printMask = null)
        {
            var fg = new FileGeneration();
            ToString(
                item: item,
                fg: fg,
                name: name,
                printMask: printMask);
            return fg.ToString();
        }
        
        public void ToString(
            IMagicEffectKeywordArchetypeGetter item,
            FileGeneration fg,
            string? name = null,
            MagicEffectKeywordArchetype.Mask<bool>? printMask = null)
        {
            if (name == null)
            {
                fg.AppendLine($"MagicEffectKeywordArchetype =>");
            }
            else
            {
                fg.AppendLine($"{name} (MagicEffectKeywordArchetype) =>");
            }
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
                ToStringFields(
                    item: item,
                    fg: fg,
                    printMask: printMask);
            }
            fg.AppendLine("]");
        }
        
        protected static void ToStringFields(
            IMagicEffectKeywordArchetypeGetter item,
            FileGeneration fg,
            MagicEffectKeywordArchetype.Mask<bool>? printMask = null)
        {
            MagicEffectArchetypeCommon.ToStringFields(
                item: item,
                fg: fg,
                printMask: printMask);
        }
        
        public static MagicEffectKeywordArchetype_FieldIndex ConvertFieldIndex(MagicEffectArchetype_FieldIndex index)
        {
            switch (index)
            {
                case MagicEffectArchetype_FieldIndex.Type:
                    return (MagicEffectKeywordArchetype_FieldIndex)((int)index);
                case MagicEffectArchetype_FieldIndex.AssociationKey:
                    return (MagicEffectKeywordArchetype_FieldIndex)((int)index);
                case MagicEffectArchetype_FieldIndex.ActorValue:
                    return (MagicEffectKeywordArchetype_FieldIndex)((int)index);
                default:
                    throw new ArgumentException($"Index is out of range: {index.ToStringFast_Enum_Only()}");
            }
        }
        
        #region Equals and Hash
        public virtual bool Equals(
            IMagicEffectKeywordArchetypeGetter? lhs,
            IMagicEffectKeywordArchetypeGetter? rhs)
        {
            if (lhs == null && rhs == null) return false;
            if (lhs == null || rhs == null) return false;
            if (!base.Equals((IMagicEffectArchetypeGetter)lhs, (IMagicEffectArchetypeGetter)rhs)) return false;
            return true;
        }
        
        public override bool Equals(
            IMagicEffectArchetypeGetter? lhs,
            IMagicEffectArchetypeGetter? rhs)
        {
            return Equals(
                lhs: (IMagicEffectKeywordArchetypeGetter?)lhs,
                rhs: rhs as IMagicEffectKeywordArchetypeGetter);
        }
        
        public virtual int GetHashCode(IMagicEffectKeywordArchetypeGetter item)
        {
            var hash = new HashCode();
            hash.Add(base.GetHashCode());
            return hash.ToHashCode();
        }
        
        public override int GetHashCode(IMagicEffectArchetypeGetter item)
        {
            return GetHashCode(item: (IMagicEffectKeywordArchetypeGetter)item);
        }
        
        #endregion
        
        
        public override object GetNew()
        {
            return MagicEffectKeywordArchetype.GetNew();
        }
        
        #region Mutagen
        public IEnumerable<FormLinkInformation> GetContainedFormLinks(IMagicEffectKeywordArchetypeGetter obj)
        {
            yield break;
        }
        
        #endregion
        
    }
    public partial class MagicEffectKeywordArchetypeSetterTranslationCommon : MagicEffectArchetypeSetterTranslationCommon
    {
        public new static readonly MagicEffectKeywordArchetypeSetterTranslationCommon Instance = new MagicEffectKeywordArchetypeSetterTranslationCommon();

        #region DeepCopyIn
        public void DeepCopyIn(
            IMagicEffectKeywordArchetypeInternal item,
            IMagicEffectKeywordArchetypeGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask,
            bool deepCopy)
        {
            base.DeepCopyIn(
                item,
                rhs,
                errorMask,
                copyMask,
                deepCopy: deepCopy);
        }
        
        public void DeepCopyIn(
            IMagicEffectKeywordArchetype item,
            IMagicEffectKeywordArchetypeGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask,
            bool deepCopy)
        {
            base.DeepCopyIn(
                (IMagicEffectArchetype)item,
                (IMagicEffectArchetypeGetter)rhs,
                errorMask,
                copyMask,
                deepCopy: deepCopy);
        }
        
        public override void DeepCopyIn(
            IMagicEffectArchetypeInternal item,
            IMagicEffectArchetypeGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask,
            bool deepCopy)
        {
            this.DeepCopyIn(
                item: (IMagicEffectKeywordArchetypeInternal)item,
                rhs: (IMagicEffectKeywordArchetypeGetter)rhs,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: deepCopy);
        }
        
        public override void DeepCopyIn(
            IMagicEffectArchetype item,
            IMagicEffectArchetypeGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask,
            bool deepCopy)
        {
            this.DeepCopyIn(
                item: (IMagicEffectKeywordArchetype)item,
                rhs: (IMagicEffectKeywordArchetypeGetter)rhs,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: deepCopy);
        }
        
        #endregion
        
        public MagicEffectKeywordArchetype DeepCopy(
            IMagicEffectKeywordArchetypeGetter item,
            MagicEffectKeywordArchetype.TranslationMask? copyMask = null)
        {
            MagicEffectKeywordArchetype ret = (MagicEffectKeywordArchetype)((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).GetNew();
            ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: ret,
                rhs: item,
                errorMask: null,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: true);
            return ret;
        }
        
        public MagicEffectKeywordArchetype DeepCopy(
            IMagicEffectKeywordArchetypeGetter item,
            out MagicEffectKeywordArchetype.ErrorMask errorMask,
            MagicEffectKeywordArchetype.TranslationMask? copyMask = null)
        {
            var errorMaskBuilder = new ErrorMaskBuilder();
            MagicEffectKeywordArchetype ret = (MagicEffectKeywordArchetype)((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).GetNew();
            ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                ret,
                item,
                errorMask: errorMaskBuilder,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: true);
            errorMask = MagicEffectKeywordArchetype.ErrorMask.Factory(errorMaskBuilder);
            return ret;
        }
        
        public MagicEffectKeywordArchetype DeepCopy(
            IMagicEffectKeywordArchetypeGetter item,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask = null)
        {
            MagicEffectKeywordArchetype ret = (MagicEffectKeywordArchetype)((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)item).CommonInstance()!).GetNew();
            ((MagicEffectKeywordArchetypeSetterTranslationCommon)((IMagicEffectKeywordArchetypeGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: ret,
                rhs: item,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: true);
            return ret;
        }
        
    }
    #endregion

}

namespace Mutagen.Bethesda.Skyrim
{
    public partial class MagicEffectKeywordArchetype
    {
        #region Common Routing
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => MagicEffectKeywordArchetype_Registration.Instance;
        public new static MagicEffectKeywordArchetype_Registration Registration => MagicEffectKeywordArchetype_Registration.Instance;
        [DebuggerStepThrough]
        protected override object CommonInstance() => MagicEffectKeywordArchetypeCommon.Instance;
        [DebuggerStepThrough]
        protected override object CommonSetterInstance()
        {
            return MagicEffectKeywordArchetypeSetterCommon.Instance;
        }
        [DebuggerStepThrough]
        protected override object CommonSetterTranslationInstance() => MagicEffectKeywordArchetypeSetterTranslationCommon.Instance;

        #endregion

    }
}

#region Modules
#region Binary Translation
namespace Mutagen.Bethesda.Skyrim.Internals
{
    public partial class MagicEffectKeywordArchetypeBinaryWriteTranslation :
        MagicEffectArchetypeBinaryWriteTranslation,
        IBinaryWriteTranslator
    {
        public new readonly static MagicEffectKeywordArchetypeBinaryWriteTranslation Instance = new MagicEffectKeywordArchetypeBinaryWriteTranslation();

        public void Write(
            MutagenWriter writer,
            IMagicEffectKeywordArchetypeGetter item,
            RecordTypeConverter? recordTypeConverter = null)
        {
            MagicEffectArchetypeBinaryWriteTranslation.WriteEmbedded(
                item: item,
                writer: writer);
        }

        public override void Write(
            MutagenWriter writer,
            object item,
            RecordTypeConverter? recordTypeConverter = null)
        {
            Write(
                item: (IMagicEffectKeywordArchetypeGetter)item,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }

        public override void Write(
            MutagenWriter writer,
            IMagicEffectArchetypeGetter item,
            RecordTypeConverter? recordTypeConverter = null)
        {
            Write(
                item: (IMagicEffectKeywordArchetypeGetter)item,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }

    }

    public partial class MagicEffectKeywordArchetypeBinaryCreateTranslation : MagicEffectArchetypeBinaryCreateTranslation
    {
        public new readonly static MagicEffectKeywordArchetypeBinaryCreateTranslation Instance = new MagicEffectKeywordArchetypeBinaryCreateTranslation();

    }

}
namespace Mutagen.Bethesda.Skyrim
{
    #region Binary Write Mixins
    public static class MagicEffectKeywordArchetypeBinaryTranslationMixIn
    {
    }
    #endregion


}
namespace Mutagen.Bethesda.Skyrim.Internals
{
    public partial class MagicEffectKeywordArchetypeBinaryOverlay :
        MagicEffectArchetypeBinaryOverlay,
        IMagicEffectKeywordArchetypeGetter
    {
        #region Common Routing
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => MagicEffectKeywordArchetype_Registration.Instance;
        public new static MagicEffectKeywordArchetype_Registration Registration => MagicEffectKeywordArchetype_Registration.Instance;
        [DebuggerStepThrough]
        protected override object CommonInstance() => MagicEffectKeywordArchetypeCommon.Instance;
        [DebuggerStepThrough]
        protected override object CommonSetterTranslationInstance() => MagicEffectKeywordArchetypeSetterTranslationCommon.Instance;

        #endregion

        void IPrintable.ToString(FileGeneration fg, string? name) => this.ToString(fg, name);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected override object BinaryWriteTranslator => MagicEffectKeywordArchetypeBinaryWriteTranslation.Instance;
        void IBinaryItem.WriteToBinary(
            MutagenWriter writer,
            RecordTypeConverter? recordTypeConverter = null)
        {
            ((MagicEffectKeywordArchetypeBinaryWriteTranslation)this.BinaryWriteTranslator).Write(
                item: this,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }

        partial void CustomFactoryEnd(
            OverlayStream stream,
            int finalPos,
            int offset);

        partial void CustomCtor();
        protected MagicEffectKeywordArchetypeBinaryOverlay(
            ReadOnlyMemorySlice<byte> bytes,
            BinaryOverlayFactoryPackage package)
            : base(
                bytes: bytes,
                package: package)
        {
            this.CustomCtor();
        }

        public static MagicEffectKeywordArchetypeBinaryOverlay MagicEffectKeywordArchetypeFactory(
            OverlayStream stream,
            BinaryOverlayFactoryPackage package,
            RecordTypeConverter? recordTypeConverter = null)
        {
            var ret = new MagicEffectKeywordArchetypeBinaryOverlay(
                bytes: stream.RemainingMemory,
                package: package);
            int offset = stream.Position;
            ret.CustomFactoryEnd(
                stream: stream,
                finalPos: stream.Length,
                offset: offset);
            return ret;
        }

        public static MagicEffectKeywordArchetypeBinaryOverlay MagicEffectKeywordArchetypeFactory(
            ReadOnlyMemorySlice<byte> slice,
            BinaryOverlayFactoryPackage package,
            RecordTypeConverter? recordTypeConverter = null)
        {
            return MagicEffectKeywordArchetypeFactory(
                stream: new OverlayStream(slice, package),
                package: package,
                recordTypeConverter: recordTypeConverter);
        }

        #region To String

        public override void ToString(
            FileGeneration fg,
            string? name = null)
        {
            MagicEffectKeywordArchetypeMixIn.ToString(
                item: this,
                name: name);
        }

        #endregion

        #region Equals and Hash
        public override bool Equals(object? obj)
        {
            if (!(obj is IMagicEffectKeywordArchetypeGetter rhs)) return false;
            return ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)this).CommonInstance()!).Equals(this, rhs);
        }

        public bool Equals(IMagicEffectKeywordArchetypeGetter? obj)
        {
            return ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)this).CommonInstance()!).Equals(this, obj);
        }

        public override int GetHashCode() => ((MagicEffectKeywordArchetypeCommon)((IMagicEffectKeywordArchetypeGetter)this).CommonInstance()!).GetHashCode(this);

        #endregion

    }

}
#endregion

#endregion

