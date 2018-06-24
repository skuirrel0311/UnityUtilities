using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class LoginBonusDialog : OKDialog
    {
        [SerializeField]
        LoginBonusDialogElement[] elements = null;
        
        [SerializeField]
        Color alreadyGetElementColor = Color.Lerp(Color.yellow, Color.blue, 0.5f);
        [SerializeField]
        Color currentDayColor = Color.Lerp(Color.yellow, Color.red, 0.5f);
        [SerializeField]
        Color willGetElementColor = Color.Lerp(Color.black, Color.white, 0.3f);

        protected override void Start()
        {
            base.Start();
            int totalLoginDays = MyPlayerPrefs.LoadInt(SaveKeyName.TotalLoginDays, 0);

            if (totalLoginDays <= 0) totalLoginDays = 1;

            int weeklyCount = (totalLoginDays / elements.Length);
            int currentElementIndex = (totalLoginDays - 1) % elements.Length;

            for (int i = 0; i < elements.Length; i++)
            {
                int day = (weeklyCount * elements.Length) + i + 1;
                IGameItem item = LoginBonus.Instance.GetBonusItem(day);
                elements[i].Init(i + 1, item.Type, item.ID, item.Count, i <= currentElementIndex);

                //色変更
                if (i < currentElementIndex) elements[i].Color = alreadyGetElementColor;
                else if (i == currentElementIndex) elements[i].Color = currentDayColor;
                else if (i > currentElementIndex) elements[i].Color = willGetElementColor;
            }
        }
    }
}