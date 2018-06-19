using System;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class LoginBonus : MonoBehaviour
    {
        const string DateFormat = "yyyyMMdd";

        DateTime LastDate
        {
            get
            {
                var lastDateStr = MyPlayerPrefs.LoadString(SaveKeyName.LastLoginDate, DateTime.Today.ToString(DateFormat));
                return DateTime.ParseExact(lastDateStr, DateFormat, null);
            }
            set
            {
                MyPlayerPrefs.SaveString(SaveKeyName.LastLoginDate, value.ToString(DateFormat));
            }
        }

        int TotalLoginDay
        {
            get
            {
                return MyPlayerPrefs.LoadInt(SaveKeyName.TotalLoginDays, 0);
            }
            set
            {
                MyPlayerPrefs.SaveInt(SaveKeyName.TotalLoginDays, value);
            }
        }

        void Start()
        {
            if (IsNextDay())
            {
                //2日またいでも１しか増やさない
                TotalLoginDay++;
                OKDialog dialog = DialogDisplayer.Instance.ShowOKDialog("LoginBonusDialog");
                dialog.OnClick.AddListener(OnClick);
            }
        }

        bool IsNextDay()
        {
            //var currentDate = DateTime.Today;

            //int diffDays = (currentDate - LastDate).Days;

            //LastDate = currentDate;

            ////1日過ぎていたら
            //return diffDays >= 1;
            return true;
        }

        void OnClick()
        {

        }
    }
}