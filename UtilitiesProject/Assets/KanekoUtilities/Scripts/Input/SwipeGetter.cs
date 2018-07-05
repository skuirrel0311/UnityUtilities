using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class SwipeGetter : SingletonMonobehaviour<SwipeGetter>
    {
        [SerializeField]
        TouchPanel touchPanel = null;
        [SerializeField]
        float swipePowerBoder = 10.0f;

        /// <summary>
        /// タッチ操作を受け付けるか？
        /// </summary>
        public bool CanTouch;

        /// <summary>
        /// 指の移動量
        /// </summary>
        public Vector2 DeltaPosition { get; private set; }

        /// <summary>
        /// 画面がタップされた時（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTap = new MyUnityEvent<Vector2>();
        /// <summary>
        /// スワイプを検出したとき（移動量）
        /// </summary>
        public MyUnityEvent<Vector2> onSwipe = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面に指が触れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTouchStart = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面から指が離れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTouchEnd = new MyUnityEvent<Vector2>();

        float screenSizeRate = 1.0f;
        
        Vector2 oldTouchPosition;
        Vector2 swipeValue;
        float swipePower = 0.0f;
        float touchTime = 0.0f;
        bool isStartTouch = false;

        protected override void Start()
        {
            base.Start();

            screenSizeRate = 1.0f / ((float)Screen.width / 1080);

            touchPanel.OnTouchStart.AddListener(OnTouchStart);
            touchPanel.OnTouchEnd.AddListener(OnTouchEnd);
            touchPanel.OnTouching.AddListener(OnTouching);
        }

        void OnTouchStart(Vector2 touchPosition)
        {
            isStartTouch = CanTouch;
            if (!isStartTouch) return;

            DeltaPosition = Vector2.zero;
            swipePower = 0.0f;
            swipeValue = Vector2.zero;
            touchTime = 0.0f;
            oldTouchPosition = TouchGetter.GetTouchPositon();

            if (onTouchStart != null) onTouchStart.Invoke(touchPosition);
        }
        void OnTouching(Vector2 touchPosition)
        {
            //移動量の計算
            touchPosition = TouchGetter.GetTouchPositon();
            DeltaPosition = (touchPosition - oldTouchPosition) * screenSizeRate;
            oldTouchPosition = touchPosition;

            //スワイプ中にCanTouchがfalseになった
            if (isStartTouch && !CanTouch)
            {
                isStartTouch = false;
                if (onTouchEnd != null) onTouchEnd.Invoke(oldTouchPosition);
            }

            //操作受け中ではなかった
            if (!isStartTouch || !CanTouch) return;

            if (swipePower < swipePowerBoder)
            {
                touchTime += Time.deltaTime;
                swipeValue += DeltaPosition;
                swipePower = swipeValue.magnitude;
            }
            else
            {
                if (onSwipe != null) onSwipe.Invoke(DeltaPosition);
            }
        }
        void OnTouchEnd(Vector2 touchPosition)
        {
            if (!isStartTouch || !CanTouch) return;
            if (swipePower < swipePowerBoder)
            {
                if (onTap != null) onTap.Invoke(touchPosition);
            }

            if (onTouchEnd != null) onTouchEnd.Invoke(touchPosition);

            DeltaPosition = Vector2.zero;
        }
    }
}
