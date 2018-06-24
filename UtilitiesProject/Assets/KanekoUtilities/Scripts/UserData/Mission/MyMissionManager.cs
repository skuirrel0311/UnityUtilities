using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class MyMissionManager : Singleton<MyMissionManager>
    {
        List<MissionData> missionDataList = new List<MissionData>();
        public List<MissionData> MissionDataList { get { return missionDataList; } }

        public MyMissionManager()
        {
            SetMissionData();
        }

        void SetMissionData()
        {
            missionDataList.Add(new MissionData(MissionType.ScoreSingle, 100, 100, 200));
            missionDataList.Add(new MissionData(MissionType.ScoreTotal, 300, 100, 200));
            missionDataList.Add(new MissionData(MissionType.TotalTouchingGems, 5, 100, 200));
            missionDataList.Add(new MissionData(MissionType.ConsecutiveTouchingGems, 2, 100, 200));
            missionDataList.Add(new MissionData(MissionType.ReachScoreWithoutTouchingGems, 50, 100, 200));

            missionDataList.Add(new MissionData(MissionType.ScoreSingle, 300, 150, 250));
            missionDataList.Add(new MissionData(MissionType.ScoreTotal, 500, 150, 250));
            missionDataList.Add(new MissionData(MissionType.TotalTouchingGems, 10, 150, 250));
            missionDataList.Add(new MissionData(MissionType.ConsecutiveTouchingGems, 3, 150, 250));
            missionDataList.Add(new MissionData(MissionType.ReachScoreWithoutTouchingGems, 100, 150, 250));

            missionDataList.Add(new MissionData(MissionType.ScoreSingle, 500, 200, 300));
            missionDataList.Add(new MissionData(MissionType.ScoreTotal, 1000, 200, 300));
            missionDataList.Add(new MissionData(MissionType.TotalTouchingGems, 20, 200, 300));
            missionDataList.Add(new MissionData(MissionType.ConsecutiveTouchingGems, 5, 200, 300));
            missionDataList.Add(new MissionData(MissionType.ReachScoreWithoutTouchingGems, 300, 200, 300));

            missionDataList.Add(new MissionData(MissionType.ScoreSingle, 1000, 300, 400));
            missionDataList.Add(new MissionData(MissionType.ScoreTotal, 1500, 300, 400));
            missionDataList.Add(new MissionData(MissionType.TotalTouchingGems, 30, 300, 400));
            missionDataList.Add(new MissionData(MissionType.ConsecutiveTouchingGems, 8, 300, 400));
            missionDataList.Add(new MissionData(MissionType.ReachScoreWithoutTouchingGems, 500, 300, 400));
        }

        public MissionState GetMissionState(MissionData missionData)
        {
            return (MissionState)PlayerPrefs.GetInt(missionData.GetKey(), 0);
        }

        public bool UpdateMissionState()
        {
            for (int i = 0; i < missionDataList.Count; i++)
            {
            }

            return false;
        }
    }
}