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
        UGUITextUnity[] texts = null;

        [SerializeField]
        UGUIImage image = null;

        [SerializeField]
        UGUIImage checkImage = null;

        public void Init(int day, ItemType itemType, string itemID, int count, bool isCompleted)
        {
            this.day.SetValue(day);
            SetRewordImage(itemType, itemID, count);
            checkImage.gameObject.SetActive(isCompleted);
        }

        void SetRewordImage(ItemType itemType, string itemID, int count)
        {
            if (itemType == ItemType.VirtualCoin)
            {
                value.Text = "+" + count;
            }
        }

        public override Color Color
        {
            get
            {
                return texts[0].Color;
            }

            set
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i].Color = value;
                }
                image.Color = value;
            }
        }
    }
}