using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Text))]
    public class NumberText : MonoBehaviour
    {
        Text text;
        int value;

        public void SetValue(int value)
        {
            this.value = value;
        }

        public int GetValue()
        {
            return value;
        }

        void Awake()
        {
            text = GetComponent<Text>();
        }
    }
}