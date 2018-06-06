using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class NumberText : UGUIText
    {
        protected int value;

        public int Value { get; set; }

        bool TryGetValue(ref int value)
        {
            value = 2;
            return true;
        }

        public void SetValue(int value, bool isUpdate = false)
        {
            if (!isUpdate && this.value == value) return;
            this.value = value;
            SetText(value);
        }

        protected virtual void SetText(int value)
        {
            this.value = value;
            Text = value.ToString();

            Vector3 pos = transform.position;
            pos.x = 2;
            transform.position = pos;
        }

        public virtual int GetValue()
        {
            return value;
        }
    }
}