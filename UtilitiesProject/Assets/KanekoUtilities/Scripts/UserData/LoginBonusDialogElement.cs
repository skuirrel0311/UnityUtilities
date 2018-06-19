using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities {
    public class LoginBonusDialogElement : UGUIParts
    {
        [SerializeField]
        NumberText day = null;

        [SerializeField]
        UGUITextUnity value = null;

        [SerializeField]
        UGUIImage[] backGrounds = null;

        public void Init(int day, int value)
        {
            this.day.SetValue(day);
            this.value.Text = "+" + value;
        }

        public override Color Color
        {
            get
            {
                return backGrounds[0].Color;
            }

            set
            {
                for(int i = 0;i< backGrounds.Length;i++)
                {
                    backGrounds[i].Color = value;
                }
            }
        }
    }
}