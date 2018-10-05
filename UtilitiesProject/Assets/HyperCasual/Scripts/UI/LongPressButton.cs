using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using HyperCasual;

namespace HyperCasual.UI
{
    [RequireComponent (typeof(Button))]
    public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent onLongPress = new UnityEvent ();
        bool isPressed;
        float pressedTime;

        void Update ()
        {
            if (isPressed && Time.time >= pressedTime) {
                pressedTime += Time.deltaTime;
                if (pressedTime >= InputUtility.LongPressInterval) {
                    isPressed = false;
                    if (onLongPress != null) {
                        onLongPress.Invoke ();
                    }
                }
            }
        }

        public void OnPointerDown (PointerEventData eventData)
        {
            isPressed = true;
            pressedTime = 0;
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            isPressed = false;
        }
    }
}