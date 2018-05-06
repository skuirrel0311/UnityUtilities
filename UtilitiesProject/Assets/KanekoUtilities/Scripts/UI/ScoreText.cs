using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Text))]
    public class ScoreText : NumberText
    {
        //何桁まで表示するか？
        [SerializeField]
        int maxDigit = 7;

        protected override void SetText(int value)
        {
            text.text = value.ToString().PadLeft(maxDigit, '0');
        }
    }
}