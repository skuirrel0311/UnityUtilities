using System;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class LoginBonus : Singleton<LoginBonus>
    {
        class LoginBonusItem
        {
            IGameItem[] gameItems;

            public LoginBonusItem(IGameItem item)
            {
                gameItems = new IGameItem[1];
                gameItems[0] = item;
            }

            public LoginBonusItem(IGameItem[] items)
            {
                gameItems = items;
            }

            public IGameItem GetItem(int day, int weeklyLength)
            {
                int index = 0;
                if (day != 1)
                {
                    index = (day - 1) / weeklyLength;
                }
                //最期の週に設定したアイテムを以降繰り返す
                if (index >= gameItems.Length) index = gameItems.Length - 1;

                return gameItems[index];
            }
        }

        const string DateFormat = "yyyyMMdd";
        public const int WeeklyLength = 5;
        List<LoginBonusItem> loginBonusItemList = new List<LoginBonusItem>();

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
            if (IsNextDay())
            {
                CanGetLoginBonus = true;
                //2日またいでも１しか増やさない
                TotalLoginDay++;
            }
        }

        public LoginBonusDialog ShowLoginBonusDialog()
        {
            LoginBonusDialog dialog = DialogDisplayer.Instance.ShowDialog<LoginBonusDialog>("LoginBonusDialog");

            //todo:このタイミングで値の増加をする
            //IGameItem loginBonusItem = GetBonusItem(TotalLoginDay);
            
            dialog.Init(OnClick);

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
            int index = (day - 1) % WeeklyLength;

            return loginBonusItemList[index].GetItem(day, WeeklyLength);
        }
    }
}