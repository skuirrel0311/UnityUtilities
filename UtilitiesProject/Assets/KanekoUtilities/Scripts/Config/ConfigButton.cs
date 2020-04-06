using UnityEngine;

namespace KanekoUtilities
{
    public class ConfigButton : MonoBehaviour
    {
        [SerializeField]
        ConfigValueName valueName = 0;

        [SerializeField]
        ToggleSwitch toggleSwitch = null;

        public void Init()
        {
            toggleSwitch.IsOn = GameConfig.Instance.GetValue(valueName);

            toggleSwitch.OnValueChanged.AddListener((value) =>
            {
                GameConfig.Instance.OnChangeValue(valueName, value);
            });
        }
    }
}