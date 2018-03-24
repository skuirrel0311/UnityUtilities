using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class SpriteAnimationManager : SingletonMonobehaviour<SpriteAnimationManager>
    {
        Dictionary<string, ObjectPooler> spriteAnimationDictionary = new Dictionary<string, ObjectPooler>();

        public PoolMonoBehaviour Play(string name, Vector3 localPosition, Transform parent)
        {
            ObjectPooler pooler = GetSpriteAnimationPooler(name);

            if (pooler == null) return null;

            PoolMonoBehaviour spriteAnimation = pooler.GetInstance();

            spriteAnimation.transform.parent = parent;
            spriteAnimation.transform.localPosition = localPosition;
            spriteAnimation.gameObject.SetActive(true);

            return spriteAnimation;
        }

        public PoolMonoBehaviour Play(string name, Vector3 position)
        {
            ObjectPooler pooler = GetSpriteAnimationPooler(name);

            if (pooler == null) return null;

            PoolMonoBehaviour spriteAnimation = pooler.GetInstance();

            spriteAnimation.transform.parent = pooler.transform;
            spriteAnimation.transform.position = position;
            spriteAnimation.gameObject.SetActive(true);

            return spriteAnimation;
        }

        public PoolMonoBehaviour Play(string name, Vector3 position, float limitLife)
        {
            PoolMonoBehaviour spriteAnimation = Play(name, position);

            KKUtilities.Delay(limitLife, () => spriteAnimation.gameObject.SetActive(false), this);

            return spriteAnimation;
        }

        public PoolMonoBehaviour Play(string name, Vector3 position,float limitLife, Transform parent)
        {
            PoolMonoBehaviour spriteAnimation = Play(name, position, parent);

            KKUtilities.Delay(limitLife, () => spriteAnimation.gameObject.SetActive(false), this);

            return spriteAnimation;
        }

        ObjectPooler GetSpriteAnimationPooler(string name)
        {
            ObjectPooler pooler;
            if(spriteAnimationDictionary.TryGetValue(name, out pooler))
            {
                return pooler;
            }

            ObjectPooler poolerPrefab = MyAssetStore.I.GetAsset<ObjectPooler>(name, "SpriteAnimations/");

            if (poolerPrefab == null) return null;

            pooler = Instantiate(poolerPrefab, transform);

            //登録する
            spriteAnimationDictionary.Add(name, pooler);

            return pooler;
        }

    }
}
