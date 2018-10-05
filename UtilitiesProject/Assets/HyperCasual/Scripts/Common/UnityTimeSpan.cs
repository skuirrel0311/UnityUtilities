using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual
{
    [Serializable]
    public struct UnityTimeSpan: IComparable, IComparable<UnityTimeSpan>, IComparable<TimeSpan>
    {
        [SerializeField] TimeUnit unit;
        public TimeUnit Unit { get { return unit; } }
        [SerializeField] float value;
        public float Value { get { return value; } }

        public enum TimeUnit
        {
            /// <summary>
            /// 週。
            /// </summary>
            Weeks,
            /// <summary>
            /// 日。
            /// </summary>
            Days,
            /// <summary>
            /// 時間。
            /// </summary>
            Hours,
            /// <summary>
            /// 分。
            /// </summary>
            Minutes,
            /// <summary>
            /// 秒。
            /// </summary>
            Seconds,
            /// <summary>
            /// ミリ秒。
            /// </summary>
            Milliseconds,
        }

        const int DaysInYear = 365;
        const int DaysInWeek = 7;


        public UnityTimeSpan(TimeUnit unit, float value)
        {
            this.unit = unit;
            this.value = value;
        }
        public UnityTimeSpan(TimeSpan timespan)
        {
            this.unit = TimeUnit.Seconds;
            this.value = (float)timespan.TotalSeconds;
        }

        public static UnityTimeSpan CreateFromTotalWeeks(float totalWeeks)
        {
            return new UnityTimeSpan(TimeUnit.Weeks, totalWeeks);
        }
        public static UnityTimeSpan CreateFromTotalDays(float totalDays)
        {
            return new UnityTimeSpan(TimeUnit.Days, totalDays);
        }
        public static UnityTimeSpan CreateFromTotalHours(float totalHours)
        {
            return new UnityTimeSpan(TimeUnit.Hours, totalHours);
        }
        public static UnityTimeSpan CreateFromTotalMinutes(float totalMinutes)
        {
            return new UnityTimeSpan(TimeUnit.Minutes, totalMinutes);
        }
        public static UnityTimeSpan CreateFromTotalSeconds(float totalSeconds)
        {
            return new UnityTimeSpan(TimeUnit.Seconds, totalSeconds);
        }
        public static UnityTimeSpan CreateFromTotalMilliseconds(float totalMilliseconds)
        {
            return new UnityTimeSpan(TimeUnit.Milliseconds, totalMilliseconds);
        }


        public static UnityTimeSpan OneYear {
            get {
                return new UnityTimeSpan(TimeUnit.Days, DaysInYear);
            }
        }



        #region Convert

        public TimeSpan ToTimeSpan()
        {
            switch (unit) {
            case TimeUnit.Weeks:
                return TimeSpan.FromDays(value * DaysInWeek); 
            case TimeUnit.Days:
                return TimeSpan.FromDays(value); 
            case TimeUnit.Hours:
                return TimeSpan.FromHours(value); 
            case TimeUnit.Minutes:
                return TimeSpan.FromMinutes(value);
            case TimeUnit.Seconds:
                return TimeSpan.FromSeconds(value);
            case TimeUnit.Milliseconds:
                return TimeSpan.FromMilliseconds(value);

            default:
                return new TimeSpan();
            }
        }
        public float ToTotalSeconds()
        {
            return ToTimeUnit(TimeUnit.Seconds);
        }
        public float ToTimeUnit(TimeUnit unit)
        {
            var timespan = this.ToTimeSpan();

            switch (unit) {
            case TimeUnit.Weeks:
                return (float)timespan.TotalDays / DaysInWeek;
            case TimeUnit.Days:
                return (float)timespan.TotalDays;
            case TimeUnit.Hours:
                return (float)timespan.TotalHours;
            case TimeUnit.Minutes:
                return (float)timespan.TotalMinutes;
            case TimeUnit.Seconds:
                return (float)timespan.TotalSeconds;
            case TimeUnit.Milliseconds:
                return (float)timespan.TotalMilliseconds;

            default:
                return 0;
            }
        }
        public void CovertTo(TimeUnit unit)
        {
            this.value = ToTimeUnit(unit);
            this.unit = unit;
        }
        public void ConvertToSeconds()
        {
            CovertTo(TimeUnit.Seconds);
        }
        public void ConvertToProperTimeUnit()
        {
            float time = 0;

            const float ThresholdOfDays = 1;
            time = Mathf.Abs(this.ToTimeUnit(TimeUnit.Days));
            if (time >= DaysInWeek) {
                // 1週間以上は週間表記。7日間未満は日数表記。
                CovertTo(TimeUnit.Weeks);
                return;
            } else if (time > ThresholdOfDays) {
                // 1日間超は日数表記。24時間以内は時間表記。
                CovertTo(TimeUnit.Days);
                return;
            }

            const float ThresholdOfHours = 3;
            time = Mathf.Abs(this.ToTimeUnit(TimeUnit.Hours));
            if (time > ThresholdOfHours) {
                // 3時間超は時間表記。180分以内は分数表記。
                CovertTo(TimeUnit.Hours);
                return;
            }

            const float ThresholdOfMinutes = 3;
            time = Mathf.Abs(this.ToTimeUnit(TimeUnit.Minutes));
            if (time > ThresholdOfMinutes) {
                // 3分間超は分数表記。180秒以内は秒数表記。
                CovertTo(TimeUnit.Minutes);
                return;
            }

            const float ThresholdOfSeconds = 0.1f;
            time = Mathf.Abs(this.ToTimeUnit(TimeUnit.Seconds));
            if (time > ThresholdOfSeconds) {
                // 0.1秒超は秒数表記。それ以内はミリ秒表記。
                CovertTo(TimeUnit.Seconds);
                return;
            }

            CovertTo(TimeUnit.Milliseconds);
        }


        #endregion


        #region IComparable

        int IComparable.CompareTo(object obj)
        {
            if (obj == null) {
                return 1;
            }

            if (obj is UnityTimeSpan) {
                var span = (UnityTimeSpan)obj;
                return TimeSpan.Compare(this.ToTimeSpan(), span.ToTimeSpan());
            }

            if (obj is TimeSpan) {
                var span = (TimeSpan)obj;
                return TimeSpan.Compare(this.ToTimeSpan(), span);
            }

            return 1;
        }
        int IComparable<UnityTimeSpan>.CompareTo(UnityTimeSpan span)
        {
            return TimeSpan.Compare(this.ToTimeSpan(), span.ToTimeSpan());
        }
        int IComparable<TimeSpan>.CompareTo(TimeSpan span)
        {
            return TimeSpan.Compare(this.ToTimeSpan(), span);
        }

        #endregion


        #region Overrides

        public override string ToString()
        {
            return string.Format("{0:#,0}{1}s", value, unit);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }

            if (obj is UnityTimeSpan) {
                var span = (UnityTimeSpan)obj;
                return this.ToTimeSpan() == span.ToTimeSpan();
            }

            if (obj is TimeSpan) {
                var span = (TimeSpan)obj;
                return this.ToTimeSpan() == span;
            }

            return false;
        }
        public override int GetHashCode()
        {
            return this.ToTimeSpan().GetHashCode();
        }

        #endregion
    }
}