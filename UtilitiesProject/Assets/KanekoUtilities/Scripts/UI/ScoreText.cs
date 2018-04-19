using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Text))]
    public class ScoreText : MonoBehaviour
    {
        //何桁まで表示するか？
        [SerializeField]
        int maxDigit = 7;
        Text score;

        void Awake()
        {
            score = GetComponent<Text>();
        }

        int value;
        public int GetValue()
        {
            return value;
        }

        public void SetValue(int value)
        {
            this.value = value;

            score.text = this.value.ToString().PadLeft(maxDigit, '0');
        }
    }
}