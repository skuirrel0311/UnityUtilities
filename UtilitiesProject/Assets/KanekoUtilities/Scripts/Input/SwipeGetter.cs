using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class SwipeGetter : SingletonMonobehaviour<SwipeGetter>
    {
        [SerializeField]
        float swipePowerBoder = 10.0f;

        float screenSizeRate = 1.0f;

        Vector2 oldTouchPosition;

        /// <summary>
        /// 指の移動量
        /// </summary>
        public Vector2 DeltaPosition { get; private set; }

        Vector2 swipeValue;
        float swipePower = 0.0f;
        float touchTime = 0.0f;

        /// <summary>
        /// 画面がタップされた時（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTap;
        /// <summary>
        /// スワイプを検出したとき（移動量）
        /// </summary>
        public MyUnityEvent<Vector2> onSwipe;
        /// <summary>
        /// 画面に指が触れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTouchStart;
        /// <summary>
        /// 画面から指が離れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTouchEnd;

        protected override void Start()
        {
            base.Start();

            screenSizeRate = 1.0f / ((float)Screen.width / 1080);
        }

        void Update()
        {
            Vector2 touchPosition = TouchGetter.GetTouchPositon();
            TouchInfo touchInfo = TouchGetter.GetTouch();

            DeltaPosition = (touchPosition - oldTouchPosition) * screenSizeRate;

            if (touchInfo == TouchInfo.Touching) OnTouchUpdate();
            else if (touchInfo == TouchInfo.Began) OnTouchStart();
            else if (touchInfo == TouchInfo.Ended) OnTouchEnd();

            oldTouchPosition = touchPosition;
        }

        void OnTouchUpdate()
        {
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
        void OnTouchStart()
        {
            DeltaPosition = Vector2.zero;
            swipePower = 0.0f;
            swipeValue = Vector2.zero;
            touchTime = 0.0f;

            if (onTouchStart != null) onTouchStart.Invoke(TouchGetter.GetTouchPositon());
        }
        void OnTouchEnd()
        {
            if (swipePower < swipePowerBoder)
            {
                if (onTap != null) onTap.Invoke(oldTouchPosition);
            }

            if (onTouchEnd != null) onTouchEnd.Invoke(oldTouchPosition);
        }
    }
}
