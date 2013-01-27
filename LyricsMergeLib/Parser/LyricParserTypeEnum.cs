using System;
using System.Collections.Generic;
using System.Text;
using LyricsMergeLib.Parser.Item;
using LyricsMergeLib.Parser.Attribute;

namespace LyricsMergeLib.Parser
{
    internal enum LyricParserTypeEnum
    {
        NULL,
        [LyricParser("srt", typeof(SRTParser))]
        SRT,
        LRC,
        [LyricParser("ssa", typeof(SSAParser))]
        SSA,
        ASS
    }
}
