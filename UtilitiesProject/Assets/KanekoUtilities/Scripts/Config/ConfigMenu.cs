using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class ConfigMenu : MonoBehaviour
    {
        [SerializeField]
        UGUIButton showButton = null;

        [SerializeField]
        UGUIButton[] hideButtons = null;

        [SerializeField]
        Panel configPanel = null;

        [SerializeField]
        ConfigButton[] configButtons = null;

        void Start()
        {
            showButton.AddListener(configPanel.Activate);
            foreach(var h in hideButtons) h.AddListener(configPanel.Deactivate);
            foreach(var c in configButtons) c.Init();
        }
    }
}