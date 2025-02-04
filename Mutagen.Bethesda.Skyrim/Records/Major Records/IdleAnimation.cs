using Mutagen.Bethesda.Binary;
using Noggog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mutagen.Bethesda.Skyrim
{
    public partial class IdleAnimation
    {
        [Flags]
        public enum Flag
        {
            Parent = 0x01,
            Sequence = 0x02,
            NoAttacking = 0x04,
            Blocking = 0x08,
        }
    }

    namespace Internals
    {
        public partial class IdleAnimationBinaryOverlay
        {
            public IReadOnlyList<IConditionGetter> Conditions { get; private set; } = ListExt.Empty<IConditionGetter>();

            partial void ConditionsCustomParse(OverlayStream stream, long finalPos, int offset, RecordType type, int? lastParsed)
            {
                Conditions = ConditionBinaryOverlay.ConstructBinayOverlayList(stream, _package);
            }
        }
    }
}
