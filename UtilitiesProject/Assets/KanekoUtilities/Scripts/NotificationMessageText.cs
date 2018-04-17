using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class NotificationMessageText : PoolMonoBehaviour
    {
        [SerializeField]
        Text message = null;
        public Text Message { get { return message; } }
    }
}
