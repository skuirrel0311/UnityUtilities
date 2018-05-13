using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Image))]
    public class TouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler , IPointerClickHandler
    {
        /// <summary>
        /// 画面に指が触れたとき（座標）
        /// </summary>
        public event Action<Vector2> OnTouchStart;
        /// <summary>
        /// 画面に指が触れているとき（座標）
        /// </summary>
        public event Action<Vector2> OnTouching;
        /// <summary>
        /// 画面から指が離れたとき（座標）
        /// </summary>
        public event Action<Vector2> OnTouchEnd;
        /// <summary>
        /// 画面に指が触れているか
        /// </summary>
        public bool IsTouching { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsTouching = true;
            if (OnTouchStart != null) OnTouchStart(TouchGetter.GetTouchPositon());
        }

        void Update()
        {
            if (!IsTouching) return;
            if (OnTouching != null) OnTouching(TouchGetter.GetTouchPositon());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsTouching) return;
            
            IsTouching = false;
            if (OnTouchEnd != null) OnTouchEnd(TouchGetter.GetTouchPositon());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}
