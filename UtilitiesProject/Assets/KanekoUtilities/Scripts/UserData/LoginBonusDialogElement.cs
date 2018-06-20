using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class LoginBonusDialogElement : UGUIParts
    {
        [SerializeField]
        NumberText day = null;

        [SerializeField]
        UGUITextUnity value = null;

        [SerializeField]
        UGUIImage[] backGrounds = null;

        [SerializeField]
        UGUIImage checkImage = null;

        [SerializeField]
        UGUIImage[] itemImages = null;

        [SerializeField]
        Sprite willGetImage = null;
        [SerializeField]
        Sprite alreadyGetImage = null;

        public void Init(int day, ItemType itemType, string itemID, int value, bool isCompleted)
        {
            this.day.SetValue(day);
            if (itemType == ItemType.VirtualCoin)
            {
                this.value.Text = "+" + value;
            }
            
            if (isCompleted) checkImage.Sprite = alreadyGetImage;
            else checkImage.Sprite = willGetImage;
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