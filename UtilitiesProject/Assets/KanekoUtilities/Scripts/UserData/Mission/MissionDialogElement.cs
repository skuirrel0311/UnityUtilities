using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class MissionDialogElement : UGUIParts
    {
        [SerializeField]
        NumberText missionNo = null;

        [SerializeField]
        UGUIImage[] backGrounds = null;

        [SerializeField]
        GameObject[] states = null;

        [SerializeField]
        UGUITextUnity descriptionText = null;

        [SerializeField]
        UGUITextUnity progressText = null;

        [SerializeField]
        ProgressBar progressBar = null;

        [SerializeField]
        UGUIButton skipButton = null;

        public void Init(MissionData missionData, int missionNo)
        {
            this.missionNo.SetValue(missionNo);

            if (missionData.MissionState == MissionState.Lock)
            {
                progressBar.gameObject.SetActive(false);
                descriptionText.Text = "???";
            }
            else
            {
                descriptionText.Text = missionData.GetDescription();
                progressBar.Progress = missionData.GetProgress();
                progressText.Text = missionData.GetProgressText();
            }

            SetState(missionData.MissionState); ;
        }

        void SetState(MissionState state)
        {
            for (int i = 0; i < states.Length; i++)
            {
                states[i].SetActive(i == (int)state);
            }

            if (state == MissionState.Current)
            {
                //skipButton.Button.interactable = 
            }
        }

        public override Color Color
        {
            get
            {
                return backGrounds[0].Color;
            }

            set
            {
                for (int i = 0; i < backGrounds.Length; i++)
                {
                    backGrounds[i].Color = value;
                }
            }
        }
    }
}