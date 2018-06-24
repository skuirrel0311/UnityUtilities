using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class MissionDialog : OKDialog
    {
        [SerializeField]
        MissionDialogElementPool elementPool = null;

        [SerializeField]
        ScrollRect scrollRect = null;

        [SerializeField]
        VerticalLayoutGroup verticalGroup = null;
        [SerializeField]
        RectTransform elementContainer = null;

        protected override void Start()
        {
            base.Start();
            List<MissionData> missionDataList = MyMissionManager.Instance.MissionDataList;

            Vector2 containerSize = elementContainer.sizeDelta;
            containerSize.y = (elementPool.GetOriginal.RectTransform.sizeDelta.y + verticalGroup.spacing) * missionDataList.Count + (verticalGroup.spacing * 2.0f);
            elementContainer.sizeDelta = containerSize;
            MissionDialogElement element = null;

            //ミッションの数だけ生成する
            for (int i = 0; i < missionDataList.Count; i++)
            {
                element = elementPool.GetInstance();

                element.Init(missionDataList[i], i + 1);
            }

            //ミッションの場所へ移動
            scrollRect.verticalNormalizedPosition = 0.5f;
        }
    }
}