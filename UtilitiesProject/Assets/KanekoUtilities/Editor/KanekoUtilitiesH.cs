using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KanekoUtilities
{
    public class KanekoUtilitiesHierarchyMenu
    {
        const string UIPrefabDirectory = "Prefabs/UI/";

        [MenuItem("GameObject/KKUtilities/UGUIImage", false, 20)]
        public static void CreateUGUIImage()
        {
            GameObject selectObj = Selection.activeGameObject;
            UGUIImage prefab = LoadUIPrefab<UGUIImage>("UGUIImage");
            GameObject obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            if (selectObj == null) return;


        }

        static T LoadUIPrefab<T>(string name) where T : Object
        {
            return Resources.Load<T>(UIPrefabDirectory + name);
        }
    }
}
