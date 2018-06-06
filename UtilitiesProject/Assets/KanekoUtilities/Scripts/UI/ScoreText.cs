using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class ScoreText : NumberText
    {
        //何桁まで表示するか？
        [SerializeField]
        int maxDigit = 7;

        protected override void SetText(int value)
        {
            this.value = value;
            Text = value.ToString().PadLeft(maxDigit, '0');
        }
    }
}