using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public enum MissionType
    {
        ScoreSingle, ScoreTotal, ConsecutiveTouchingGems, TotalTouchingGems, ReachScoreWithoutTouchingGems
    }
    public enum MissionState
    {
        Lock, Current, Complete, Skipped, Received
    }

    public class MissionData
    {
        MissionType type = 0;
        public MissionType Type { get { return type; } }

        MissionState state = 0;
        public MissionState MissionState { get { return state; } }

        int targetValue;
        public int TargetValue { get { return targetValue; } }

        int currentValue;
        public int CurrentValue { get { return currentValue; } }

        int skipCost;
        public int SkipCost { get { return skipCost; } }

        public MissionData(MissionType type, int targetValue, int rewordValue, int skipCost)
        {
            this.type = type;
            this.targetValue = targetValue;
            this.skipCost = skipCost;
            currentValue = 0;
        }

        public string GetDescription()
        {
            switch (type)
            {
                case MissionType.ScoreSingle:
                    return string.Format("score {0} points in single round", targetValue);
                case MissionType.ScoreTotal:
                    return string.Format("score a total of {0} points", targetValue);
                case MissionType.ConsecutiveTouchingGems:
                    return string.Format("perform {0} consecutive touching gems", targetValue);
                case MissionType.TotalTouchingGems:
                    return string.Format("collect {0} gems in total", targetValue);
                case MissionType.ReachScoreWithoutTouchingGems:
                    return string.Format("reach score {0} without touching gems", targetValue);
            }

            return "";
        }

        public float GetProgress()
        {
            return currentValue / targetValue;
        }

        public string GetProgressText()
        {
            return string.Format("{0} / {1}", currentValue, targetValue);
        }

        /// <summary>
        /// 条件などから自動でKeyを作成する
        /// </summary>
        public string GetKey()
        {
            return string.Format("{0}{1}", type.ToString(), targetValue.ToString());
        }
    }
}