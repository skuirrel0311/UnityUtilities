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
        [SerializeField]
        UGUIImage gemImage = null;

        public UGUIImage GetItemImage(ItemType type, string id)
        {
            if(type == ItemType.VirtualCoin)
            {
                return gemImage;
            }

            return null;
        }
    }


}