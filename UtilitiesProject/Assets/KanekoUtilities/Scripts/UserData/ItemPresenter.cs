using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    //アイテムを受け取った時の挙動を管理する
    public class ItemPresenter : SingletonMonobehaviour<ItemPresenter>
    {
        //[SerializeField]
        //Panel panel = null;

        public void PresentItem(IGameItem item)
        {
            //panel.Activate();
            
            //if(item.Type == ItemType.VirtualCoin)
            //{
            //    StartCoroutine(new MyCoroutine(PresentGem(item.Count)).OnCompleted(() => panel.Deactivate()));
            //}
        }

        //IEnumerator PresentGem(int count)
        //{
        //    UGUIImage[] images = new UGUIImage[Mathf.Min(gemImagePool.MaxCount, count)];
        //    //画像を生成
        //    for (int i = 0; i < images.Length; i++)
        //    {
        //        images[i] = gemImagePool.GetInstance();

        //        Vector2 offset = new Vector2(Random.Range(-30.0f, 30.0f), Random.Range(-30.0f, 30.0f));
        //        Vector2 generatePosition = origin + offset;
        //        images[i].RectTransform.anchoredPosition = generatePosition;

        //        StartCoroutine(KKUtilities.FloatLerp(Random.Range(1.5f, 2.0f), (t) =>
        //        {
        //            images[i].RectTransform.anchoredPosition = Vector2.Lerp(generatePosition, gemNumPosition, Easing.InQuad(t));
        //        }).OnCompleted(() => gemImagePool.ReturnInstance(images[i])));
        //    }

        //    yield return new WaitForSeconds(1.5f);


        //    //Countをアップさせる
        //    int targetNum = (int)MyUserDataManager.Instance.Gem.Current;
        //    int startNum = targetNum - count;

        //    yield return StartCoroutine(KKUtilities.FloatLerp(1.0f, (t) =>
        //    {
        //        gemNum.SetValue((int)Mathf.Lerp(startNum, targetNum, t));
        //    });
        //}
    }
}