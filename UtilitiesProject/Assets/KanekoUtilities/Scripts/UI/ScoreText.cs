using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField]
        Text score = null;

        //何桁まで表示するか？
        [SerializeField]
        int maxDigit = 7;

        int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

                score.text = this.value.ToString().PadLeft(maxDigit, '0');
            }
        }
    }
}