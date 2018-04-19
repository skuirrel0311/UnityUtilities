using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class MessageText : PoolMonoBehaviour
    {
        [SerializeField]
        Text message = null;
        public Text Message { get { return message; } }
    }
}
