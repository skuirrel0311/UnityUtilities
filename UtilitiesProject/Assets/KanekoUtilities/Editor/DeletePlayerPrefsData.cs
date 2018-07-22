using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KanekoUtilities
{
    public class DeletePlayerPrefsData : MonoBehaviour
    {
        [MenuItem("KanekoUtilities/DeletePlayerPrefsData")]
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}