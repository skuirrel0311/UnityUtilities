using System;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Button))]
    public class ConfigButton : MonoBehaviour
    {
        Button button;

        [SerializeField]
        GameObject enableVisual = null;
        [SerializeField]
        GameObject disableVisual = null;

        public event Action OnClick;

        bool currentEnable = true;

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SetEnable(!currentEnable);
                if (OnClick != null) OnClick();
            });
        }

        public void SetEnable(bool enable)
        {
            currentEnable = enable;
            enableVisual.SetActive(enable);
            disableVisual.SetActive(!enable);
        }
    }
}