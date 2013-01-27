using System;
using System.Collections.Generic;
using System.Text;
using LyricsMergeLib.Model;
using LyricsMergeLib.Parser.Item;
using System.IO;
using LyricsMergeLib.Parser.Utility;

namespace LyricsMergeLib.Parser
{

    internal static class LyricParserFactory
    {
        internal static ILyricParser GetLyricParser(string lyric)
        {
            LyricParserTypeEnum type = parseParserType(lyric);
            ILyricParser parser = LyricParserTypeEnumUtility.GetLyricParserInstance(type);
            return parser;
        }

        private static LyricParserTypeEnum parseParserType(string lyric)
        {
            LyricParserTypeEnum type = LyricParserTypeEnum.NULL;
            string fileType = Path.GetExtension(lyric);
            type = LyricParserTypeEnumUtility.FindLyricParserByExtension(fileType);
            return type;
        }
    }
}
