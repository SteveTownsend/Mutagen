﻿using Noggog;
using System;
using System.IO;

namespace Mutagen.Binary
{
    public class CharBinaryTranslation : PrimitiveBinaryTranslation<char>
    {
        public readonly static CharBinaryTranslation Instance = new CharBinaryTranslation();
        public override byte? ExpectedLength => 1;
        
        protected override char ParseValue(MutagenFrame reader)
        {
            return reader.Reader.ReadChar();
        }

        protected override void WriteValue(MutagenWriter writer, char item)
        {
            writer.Write(item);
        }
    }
}
