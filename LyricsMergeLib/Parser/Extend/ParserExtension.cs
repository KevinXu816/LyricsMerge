using System;
using System.Collections.Generic;
using System.Text;

namespace LyricsMergeLib.Parser
{
    internal static class ParserExtension
    {
        public static bool IsNull(this ILyricParser parser)
        {
            return null == parser;
        }
    }
}
