using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Text))]
    public class NumberText : MonoBehaviour
    {
        protected Text text;
        protected int value;

        void Awake()
        {
            text = GetComponent<Text>();
        }

        public void SetValue(int value, bool isUpdate = false)
        {
            if (!isUpdate && this.value == value) return;
            this.value = value;
            SetText(value);
        }

        protected virtual void SetText(int value)
        {
            text.text = value.ToString();
        }

        public virtual int GetValue()
        {
            return value;
        }
    }
}