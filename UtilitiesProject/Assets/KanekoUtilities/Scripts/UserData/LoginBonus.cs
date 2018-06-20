using System;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class LoginBonus : Singleton<LoginBonus>
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
                return MyPlayerPrefs.LoadInt(SaveKeyName.TotalLoginDays);
            }
            set
            {
                MyPlayerPrefs.SaveInt(SaveKeyName.TotalLoginDays, value);
            }
        }

        public bool CanGetLoginBonus
        {
            get
            {
                return MyPlayerPrefs.LoadBool(SaveKeyName.CanGetLoginBonus);
            }
            set
            {
                MyPlayerPrefs.SaveBool(SaveKeyName.CanGetLoginBonus, value);
            }
        }

        public LoginBonus()
        {
            if(IsNextDay())
            {
                CanGetLoginBonus = true;
            //2日またいでも１しか増やさない
                TotalLoginDay++;
            }
        }

        public LoginBonusDialog ShowLoginBonusDialog()
        {
            LoginBonusDialog dialog = DialogDisplayer.Instance.ShowDialog<LoginBonusDialog>("LoginBonusDialog");
            
            dialog.OnClick.AddListener(OnClick);

            return dialog;
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
            CanGetLoginBonus = false;
        }

        public IGameItem GetBonusItem(int day)
        {
            Gem gem = new Gem();
            gem.Earn(day * 50);
            return gem;
        }
    }

    public class Gem : IGameItem
    {
        public ItemType Type { get { return ItemType.VirtualCoin; } }
        public string ID { get { return "0"; } }
        int count = 0;
        public int Count { get { return count; } }

        public void Earn(int value)
        {
            count += value;
        }

        public bool Spend(int value)
        {
            if (value > count) return false;
            
            return true;
        }
    }
}