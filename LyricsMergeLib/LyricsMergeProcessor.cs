using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricsMergeLib.Parser;
using LyricsMergeLib.Model;

namespace LyricsMergeLib
{
    public static class LyricsMergeProcessor
    {
        public static void MergeLyrics(string[] mergeLyrics, string outputLyric)
        {
            outputLyric = (outputLyric ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(outputLyric))
                return;
            if (mergeLyrics == null || mergeLyrics.Length == 0)
                return;

            List<LyricContent> lyricsContent = new List<LyricContent>();
            for (int i = 0; i < mergeLyrics.Length; i++)
            {
                string lyric = (mergeLyrics[i] ?? string.Empty).Trim();
                if (string.IsNullOrEmpty(lyric))
                    continue;
                LyricContent content = null;
                ILyricParser parser = LyricParserFactory.GetLyricParser(lyric);
                if (!parser.IsNull())
                    content = parser.ParseLyric(lyric);
                if (content == null)
                    continue;
                else
                {
                    content.SortClip();
                    lyricsContent.Add(content);
                }
            }
            ILyricParser outputParser = LyricParserFactory.GetLyricParser(outputLyric);
            if (!outputParser.IsNull())
            {
                string joinSpec = outputParser.JoinSpec;
                LyricContent mergeContent = MergeContent(lyricsContent, joinSpec);
                outputParser.GenerateLyric(mergeContent, outputLyric);
            }
        }

        private static LyricContent MergeContent(List<LyricContent> lyricsContent, string joinSpec)
        {
            LyricContent merge = new LyricContent();
            foreach (LyricContent lyric in lyricsContent)
            {
                merge = MergeContent(merge, lyric, joinSpec);
            }
            return merge;
        }

        private static LyricContent MergeContent(LyricContent aLyric, LyricContent bLyric, string joinSpec)
        {
            LyricContent merge = new LyricContent();
            if (aLyric == null && bLyric == null)
                return merge;
            else
            {
                if (aLyric == null)
                    return bLyric;
                if (bLyric == null)
                    return aLyric;
                foreach (LyricClip aClip in aLyric)
                {
                    LyricClip clip = aClip.Clone();
                    List<LyricClip> bClippes = bLyric.FindSameClippes(clip);
                    if (bClippes != null)
                    {
                        foreach (LyricClip bClip in bClippes)
                        {
                            string fLyric = (clip.ClipLyric ?? string.Empty).Trim();
                            string lLyric = (bClip.ClipLyric ?? string.Empty).Trim();
                            string mSpec = joinSpec;
                            if (string.IsNullOrEmpty(fLyric) || string.IsNullOrEmpty(lLyric))
                                mSpec = string.Empty;
                            clip.ClipLyric = string.Format("{0}{1}{2}", fLyric, mSpec, lLyric);
                            bLyric.Remove(bClip);
                        }
                    }
                    merge.Add(clip);
                }
                foreach (LyricClip bClip in bLyric)
                {
                    List<LyricClip> mixedClippes = merge.FindMixedClippes(bClip);
                    if (mixedClippes.Count == 0)
                    {
                        merge.Add(bClip.Clone());
                    }
                    else
                    {
                        mixedClippes.Add(bClip);
                        LyricClip clip = new LyricClip();
                        foreach (LyricClip mixedClip in mixedClippes)
                        {
                            clip.StartTime = (clip.StartTime < mixedClip.StartTime) ? clip.StartTime : mixedClip.StartTime;
                            clip.EndTime = (clip.EndTime > mixedClip.EndTime) ? clip.EndTime : mixedClip.EndTime;
                            string fLyric = (clip.ClipLyric ?? string.Empty).Trim();
                            string lLyric = (mixedClip.ClipLyric ?? string.Empty).Trim();
                            string mSpec = joinSpec;
                            if (string.IsNullOrEmpty(fLyric) || string.IsNullOrEmpty(lLyric))
                                mSpec = string.Empty;
                            clip.ClipLyric = string.Format("{0}{1}{2}", fLyric, mSpec, lLyric);
                            if (mixedClip == bClip)
                                continue;
                            else
                                merge.Remove(mixedClip);
                        }
                        merge.Add(clip);
                    }
                }
            }
            merge.SortClip();
            return merge;
        }
    }
}
