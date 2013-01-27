using System;
using System.Collections.Generic;
using System.Text;
using LyricsMergeLib.Model;

namespace LyricsMergeLib.Parser
{
    internal interface ILyricParser
    {
        LyricContent ParseLyric(string file);
        void GenerateLyric(LyricContent lyric, string outputfile);
        string JoinSpec { get; }
    }
}
