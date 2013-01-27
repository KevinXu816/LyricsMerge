using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricsMergeLib.Model;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LyricsMergeLib.Parser.Item
{
    class SSAParser : ILyricParser
    {
        private const string ScriptInfoHeader = @"
[Script Info]
PlayResX: 384
PlayResY: 288
Timer: 100.0000";

        private const string StylesHeader = @"
[V4 Styles]
Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, TertiaryColour, BackColour, Bold, Italic, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, AlphaLevel, Encoding
Style: 123, Arial, 16, 16777215, 4227327, 8404992, 16744448, 0, 0, 1, 1, 2, 2, 30, 30, 14, 0, 134";

        private const string EventsHeader = @"
[Events]
Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text";

        private const string ClipFormat = @"
Dialogue: Marked = 0, {0}, {1}, *123, {2}, 0000, 0000, 0000, ,{3}";

        string ILyricParser.JoinSpec
        {
            get
            {
                return "\\N";
            }
        }

        private static Regex _eventRegex;
        private static Regex EventRegex
        {
            get
            {
                if (_eventRegex == null)
                {
                    const string eventPattern = @"^\s*\[\s*Events\s*\]\s*$";
                    _eventRegex = new Regex(eventPattern, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }
                return _eventRegex;
            }
        }

        private static Regex _formatRegex;
        private static Regex FormatRegex
        {
            get
            {
                if (_formatRegex == null)
                {
                    const string formatPattern = @"^\s*Format\s*:([^\n|\r]+Text)\s*$";
                    _formatRegex = new Regex(formatPattern, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }
                return _formatRegex;
            }
        }

        private static Regex _dialogueRegex;
        private static Regex DialogueRegex
        {
            get
            {
                if (_dialogueRegex == null)
                {
                    const string dialoguePattern = @"^\s*Dialogue\s*:\s*Marked\s*=\s*([^\n|\r]+)$";
                    _dialogueRegex = new Regex(dialoguePattern, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }
                return _dialogueRegex;
            }
        }

        private static Regex _timeRegex;
        private static Regex TimeRegex
        {
            get
            {
                if (_timeRegex == null)
                {
                    const string timePattern = @"^\s*(\d{1}):(\d{2}):(\d{2})\.(\d{2})\s*$";
                    _timeRegex = new Regex(timePattern, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }
                return _timeRegex;
            }
        }

        private GroupCollection matchDateTime(string time)
        {
            time = (time ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(time))
                return null;
            Match timeMatch = TimeRegex.Match(time);
            if (timeMatch.Success)
            {
                return timeMatch.Groups;
            }
            return null;
        }

        private class LocationInfo
        {
            public int StartLocation = -1;
            public int EndLocation = -1;
            public int TextLocation = -1;

            public bool IsAvailable
            {
                get
                {
                    return (StartLocation > -1 && EndLocation > -1 && TextLocation > -1);
                }
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
                string line;
                bool isEventPass = false;
                LocationInfo formatLocation = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    Match eventMatch = EventRegex.Match(line);
                    if (isEventPass == false && eventMatch.Success)
                    {
                        isEventPass = true;
                        continue;
                    }
                    if (isEventPass)
                    {
                        Match formatMatch = FormatRegex.Match(line);
                        if (formatLocation == null && formatMatch.Success)
                        {
                            LocationInfo tmpInfo = new LocationInfo();
                            string[] infos = formatMatch.Groups[1].ToString().Split(',');
                            for (int i = 0; i < infos.Length; i++)
                            {
                                string info = (infos[i] ?? string.Empty).Trim().ToLower();
                                switch (info)
                                {
                                    case "start":
                                        tmpInfo.StartLocation = i;
                                        break;
                                    case "end":
                                        tmpInfo.EndLocation = i;
                                        break;
                                    case "text":
                                        tmpInfo.TextLocation = i;
                                        break;
                                }
                            }
                            if (tmpInfo.IsAvailable)
                            {
                                formatLocation = tmpInfo;
                            }
                            continue;
                        }
                        if (formatLocation != null)
                        {
                            Match dialogueMatch = DialogueRegex.Match(line);
                            if (dialogueMatch.Success)
                            {
                                string[] infos = dialogueMatch.Groups[1].ToString().Split(',');
                                LyricClip clip = new LyricClip();
                                GroupCollection group = this.matchDateTime(infos[formatLocation.StartLocation]);
                                if (group != null)
                                    clip.AddStartTime(group[1].ToString(), group[2].ToString(), group[3].ToString(), group[4].ToString());
                                group = this.matchDateTime(infos[formatLocation.EndLocation]);
                                if (group != null)
                                    clip.AddEndTime(group[1].ToString(), group[2].ToString(), group[3].ToString(), group[4].ToString());
                                clip.ClipLyric = infos[formatLocation.TextLocation] ?? string.Empty;
                                content.Add(clip);
                            }
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
                writer.WriteLine(ScriptInfoHeader);
                writer.WriteLine();
                writer.WriteLine(StylesHeader);
                writer.WriteLine();
                writer.WriteLine(EventsHeader);
                writer.WriteLine();
                for (int i = 0; i < lyric.Count; i++)
                {
                    LyricClip clip = lyric[i];
                    writer.Write(string.Format(ClipFormat,
                        clip.StartTime.ToString("h:mm:ss.ff", DateTimeFormatInfo.InvariantInfo),
                        clip.EndTime.ToString("h:mm:ss.ff", DateTimeFormatInfo.InvariantInfo),
                        i + 1,
                        clip.ClipLyric ?? string.Empty
                        ));
                }
                writer.WriteLine();
            }
        }
    }
}
