using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Text))]
    public class MessageText : PoolMonoBehaviour
    {
        Text message;
        public Text Message
        {
            get
            {
                if(message == null)
                {
                    message = GetComponent<Text>();
                }

                return message;
            }
        }
    }
}
