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

    public class MyAchievementManager : SingletonMonobehaviour<MyAchievementManager>
    {

    }
}