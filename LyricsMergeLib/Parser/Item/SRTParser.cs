using System;
using System.Collections.Generic;
using System.Text;
using LyricsMergeLib.Model;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace LyricsMergeLib.Parser.Item
{
    class SRTParser: ILyricParser
    {
        /// <summary>
        ///  字幕示例：
        /// 1
        /// 00:00:39,789 --> 00:00:42,124
        /// When I was lying there in the VA hospital,
        ///
        /// 2
        /// 00:00:42,167 --> 00:00:45,127
        /// with a big hole blown through the middle of my life,
        /// </summary>

        string ILyricParser.JoinSpec
        {
            get
            {
                return "\r\n";
            }
        }

        private static Regex _timeRegex;
        private static Regex TimeRegex
        {
            get
            {
                if (_timeRegex == null)
                {
                    const string timePattern = @"^\s*(\d{2}):(\d{2}):(\d{2}),(\d{3})\s*-->\s*(\d{2}):(\d{2}):(\d{2}),(\d{3})\s*$";
                    _timeRegex = new Regex(timePattern, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }
                return _timeRegex;
            }
        }

        private static Regex _orderRegex;
        private static Regex OrderRegex
        {
            get
            {
                if (_orderRegex == null)
                {
                    const string orderPattern = @"^\s*(\d+)\s*$";
                    _orderRegex = new Regex(orderPattern, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }
                return _orderRegex;
            }
        }


        LyricContent ILyricParser.ParseLyric(string file)
        {
            LyricContent content = new LyricContent();
            file = (file ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(file))
                return content;
            using (StreamReader reader = new StreamReader(file, true))
            {
                LyricClip clip = null;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    Match orderMatch = OrderRegex.Match(line);
                    if (orderMatch.Success)
                    {
                        string subLine = (reader.ReadLine() ?? string.Empty).Trim();
                        Match timeMatch = TimeRegex.Match(subLine);
                        if (timeMatch.Success)
                        {
                            if (clip != null)
                            {
                                clip.ClipLyric = (clip.ClipLyric ?? string.Empty).Trim();
                            }
                            clip = new LyricClip();
                            GroupCollection group = timeMatch.Groups;
                            clip.AddStartTime(group[1].ToString(), group[2].ToString(), group[3].ToString(), group[4].ToString());
                            clip.AddEndTime(group[5].ToString(), group[6].ToString(), group[7].ToString(), group[8].ToString());
                            content.Add(clip);
                            continue;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(subLine))
                                line = string.Format("{0}\r\n{1}", line, subLine);
                        }
                    }
                    if (clip != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            string lyric = clip.ClipLyric ?? string.Empty;
                            if (string.IsNullOrEmpty(lyric))
                                clip.ClipLyric = line;
                            else
                                clip.ClipLyric = string.Format("{0}\r\n{1}", lyric, line);
                        }
                    }
                }
            }
            return content;
        }

        void ILyricParser.GenerateLyric(LyricContent lyric, string outputfile)
        {
            outputfile = (outputfile ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(outputfile))
                return;
            if (lyric == null)
                return;
            using (StreamWriter writer = new StreamWriter(outputfile, false, Encoding.Unicode))
            {
                for (int i = 0; i < lyric.Count; i++)
                {
                    LyricClip clip = lyric[i];
                    writer.WriteLine(i + 1);
                    writer.WriteLine(string.Format("{0} --> {1}",
                        clip.StartTime.ToString("HH:mm:ss,fff", DateTimeFormatInfo.InvariantInfo),
                        clip.EndTime.ToString("HH:mm:ss,fff", DateTimeFormatInfo.InvariantInfo)
                        ));
                    writer.WriteLine(clip.ClipLyric ?? string.Empty);
                    writer.WriteLine();
                }
            }
        }

    }
}
