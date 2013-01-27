using System;
using System.Collections.Generic;
using System.Text;

namespace LyricsMergeLib.Model
{
    internal class LyricClip
    {

        private DateTime convertDateTime(string h, string m, string s, string ms)
        {
            int hour, minute, second, millisecond;
            if (!int.TryParse(h, out hour))
                hour = 0;
            if (!int.TryParse(m, out minute))
                minute = 0;
            if (!int.TryParse(s, out second))
                second = 0;
            if (!int.TryParse(ms, out millisecond))
                millisecond = 0;
            DateTime now = DateTime.Now;
            DateTime time = new DateTime(now.Year, now.Month, now.Day, hour, minute, second, millisecond);
            return time;
        }

        private DateTime _startTime;
        public void AddStartTime(string hour, string minute, string second, string millisecond)
        {
            this._startTime = this.convertDateTime(hour, minute, second, millisecond);
        }

        public void AddStartTime(int hour, int minute, int second, int millisecond)
        {
            DateTime now = DateTime.Now;
            this._startTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second, millisecond);
        }

        public DateTime StartTime
        {
            get { return this._startTime; }
            set { this._startTime = value; }
        }


        private DateTime _endTime;
        public void AddEndTime(string hour, string minute, string second, string millisecond)
        {
            this._endTime = this.convertDateTime(hour, minute, second, millisecond);
        }

        public void AddEndTime(int hour, int minute, int second, int millisecond)
        {
            DateTime now = DateTime.Now;
            this._endTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second, millisecond);
        }

        public DateTime EndTime
        {
            get { return this._endTime; }
            set { this._endTime = value; }
        }

        public void ValidateTime()
        {
            if (this._startTime > this._endTime)
            {
                DateTime tmp = this._startTime;
                this._startTime = this._endTime;
                this._endTime = tmp;
            }
        }

        public string ClipLyric
        {
            get;
            set;
        }

        public string AttachedInfo
        {
            get;
            set;
        }

        public LyricClip Clone()
        {
            LyricClip clip = new LyricClip();
            clip.StartTime = this.StartTime;
            clip.EndTime = this.EndTime;
            clip.ClipLyric = this.ClipLyric;
            clip.AttachedInfo = this.AttachedInfo;
            return clip;
        }

    }
}
