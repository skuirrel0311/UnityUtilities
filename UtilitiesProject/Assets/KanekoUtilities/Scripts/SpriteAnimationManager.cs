using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class SpriteAnimationManager : SingletonMonobehaviour<SpriteAnimationManager>
    {
        Dictionary<string, ObjectPooler> spriteAnimationDictionary = new Dictionary<string, ObjectPooler>();

        public void Play(string name, Vector3 position, float lifeTime)
        {
            ObjectPooler pooler = GetSpriteAnimationPooler(name);

            if (pooler == null) return;

            PoolMonoBehaviour spriteAnimation = pooler.GetInstance();

            spriteAnimation.transform.position = position;
            spriteAnimation.gameObject.SetActive(true);

            KKUtilities.Delay(lifeTime, () =>
            {
                spriteAnimation.gameObject.SetActive(false);
            }, this);
        }

        ObjectPooler GetSpriteAnimationPooler(string name)
        {
            ObjectPooler pooler;
            if(spriteAnimationDictionary.TryGetValue(name, out pooler))
            {
                return pooler;
            }

            GameObject poolerObjectPrefab = MyAssetStore.I.GetAsset<GameObject>(name, "SpriteAnimations/");

            if (poolerObjectPrefab == null) return null;

            GameObject poolerObject = Instantiate(poolerObjectPrefab, transform);

            pooler = poolerObject.GetComponent<ObjectPooler>();

            if (pooler == null) return null;

            //登録する
            spriteAnimationDictionary.Add(name, pooler);

            return pooler;
        }

    }
}
