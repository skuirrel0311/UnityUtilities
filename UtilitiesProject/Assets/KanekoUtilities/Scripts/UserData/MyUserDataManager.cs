using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public enum ItemType { VirtualCoin, Skin }

    public interface IGameItem
    {
        ItemType Type { get; }
        string ID { get; }
        int Count { get; }

        void Earn(int count);
        bool Spend(int count);
    }

    public class MyCoin : IGameItem
    {
        public ItemType Type { get { return ItemType.VirtualCoin; } }
        public string ID { get { return "Coin"; } }
        int count = 0;
        public virtual int Count { get { return count; } }
        public MyCoin(int count)
        {
            this.count = count;
        }
        public virtual void Earn(int value)
        {
            count += value;
        }
        public virtual bool Spend(int value)
        {
            if (value > count) return false;
            return true;
        }
    }

    public class MyUserDataManager : SingletonMonobehaviour<MyUserDataManager>
    {
        [SerializeField]
        CoinPresenter coinPresenter = null;
        public CoinPresenter CoinPresenter { get { return coinPresenter; } }

        RegisterIntParameter currentCoinNum;
        public int CurrentCoinNum { get { return currentCoinNum.GetValue(); } }

        MyCoin myCoin;
        
        protected override void Awake()
        {
            base.Awake();
            currentCoinNum = new RegisterIntParameter("CoinNum", 0);

            myCoin = new MyCoin(CurrentCoinNum);
            CoinPresenter.Init();
        }

        public void EarnCoin(int count)
        {
            myCoin.Earn(count);
            currentCoinNum.SetValue(currentCoinNum.GetValue() + count);
        }

        public void PresentCoin(int count, System.Action callback = null)
        {
            var startCoinNum = CurrentCoinNum;
            coinPresenter.PresentCoin(count, coinPresenter.transform.position, startCoinNum, callback);
            EarnCoin(count);
        }

        public void PresentCoinAnimation(int count, int startCoinNum, System.Action callback = null)
        {
            coinPresenter.PresentCoin(count, coinPresenter.transform.position, startCoinNum, callback);
        }

        public void SpendCoin(int count)
        {
            var start = CurrentCoinNum;
            var end = start - count;
            coinPresenter.CountUpText(start, end, 1.0f, false);
            myCoin.Spend(count);
            currentCoinNum.SetValue(end);
        }
    }
}