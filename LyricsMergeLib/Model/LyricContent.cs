using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricsMergeLib.Model
{
    internal class LyricContent : List<LyricClip>
    {
        public object AttachedInfo
        {
            get;
            set;
        }

        public void SortClip()
        {
            this.Sort(this.CompareClipSort);
        }

        private int CompareClipSort(LyricClip a, LyricClip b)
        {
            DateTime aTime = DateTime.MinValue;
            if (a != null)
            {
                a.ValidateTime();
                aTime = a.StartTime;
            }
            DateTime bTime = DateTime.MinValue;
            if (b != null)
            {
                b.ValidateTime();
                bTime = b.StartTime;
            }
            if (aTime == bTime)
                return 0;
            else
            {
                if (aTime > bTime)
                    return 1;
                else
                    return -1;
            }
        }

        public List<LyricClip> FindMixedClippes(LyricClip clip)
        {
            List<LyricClip> mixedClippes = this.FindAll(item => (
                    (item.StartTime > clip.StartTime && item.StartTime < clip.EndTime && item.EndTime > clip.EndTime) ||
                    (item.EndTime > clip.StartTime && item.EndTime < clip.EndTime && item.StartTime < clip.StartTime) ||
                    (item.StartTime > clip.StartTime && item.EndTime < clip.EndTime) ||
                    (item.StartTime < clip.StartTime && item.EndTime > clip.EndTime)
                 ));
            if (mixedClippes == null)
                mixedClippes = new List<LyricClip>();
            mixedClippes.Sort(this.CompareClipSort);
            return mixedClippes;
        }

        public List<LyricClip> FindSameClippes(LyricClip clip)
        {
            List<LyricClip> sameClippes = this.FindAll(item => (item.StartTime == clip.StartTime && item.EndTime == clip.EndTime));
            if (sameClippes == null)
                sameClippes = new List<LyricClip>();
            sameClippes.Sort(this.CompareClipSort);
            return sameClippes;
        }

    }
}
