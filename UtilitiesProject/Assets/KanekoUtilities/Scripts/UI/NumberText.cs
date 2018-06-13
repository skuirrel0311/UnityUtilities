using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class NumberText : MonoBehaviour
    {
        [SerializeField]
        protected AbstractUGUIText text = null;
        protected int value;

        public virtual void SetValue(int value, bool isUpdate = false)
        {
            if (!isUpdate && this.value == value) return;
            this.value = value;
            text.Text = value.ToString();
        }

        public virtual int GetValue()
        {
            return value;
        }
    }
}