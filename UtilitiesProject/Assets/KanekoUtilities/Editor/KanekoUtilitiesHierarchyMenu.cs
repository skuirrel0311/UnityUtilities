using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KanekoUtilities
{
    public class KanekoUtilitiesHierarchyMenu
    {
        const string UIPrefabDirectory = "Assets/KanekoUtilities/Prefabs/UI/";

        [MenuItem("GameObject/KKUtilities/UGUIImage", false, 20)]
        public static void CreateUGUIImage()
        {
            CreateUGUIPartsObj("UGUIImage");
        }

        [MenuItem("GameObject/KKUtilities/UGUIText/Text", false, 20)]
        public static void CreateUGUIText()
        {
            CreateUGUIPartsObj("UGUIText");
        }

        [MenuItem("GameObject/KKUtilities/UGUIText/TextPro", false, 20)]
        public static void CreateUGUITextPro()
        {
            CreateUGUIPartsObj("UGUITextPro");
        }

        [MenuItem("GameObject/KKUtilities/Panel", false, 20)]
        public static void CreatePanel()
        {
            CreateUGUIPartsObj("Panel");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/Daialog", false, 20)]
        public static void CreateDialog()
        {
            CreateUGUIPartsObj("Dialog");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/OkDaialog", false, 20)]
        public static void CreateOkDialog()
        {
            CreateUGUIPartsObj("OkDialog");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/OkCancelDaialog", false, 20)]
        public static void CreateOkCancelDialog()
        {
            CreateUGUIPartsObj("OkCancelDialog");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/MessageDaialog", false, 20)]
        public static void CreateMessageDialog()
        {
            CreateUGUIPartsObj("MessageDialog");
        }

        [MenuItem("GameObject/KKUtilities/Canvas", false, 20)]
        public static void CreateCanvas()
        {
            CreateObject(UIPrefabDirectory, "Canvas");
        }

        [MenuItem("GameObject/KKUtilities/Text3D/Text", false, 20)]
        public static void CreateText3DUnity()
        {
            CreateObject(UIPrefabDirectory, "Text3D");
        }

        [MenuItem("GameObject/KKUtilities/Text3D/TextPro", false, 20)]
        public static void CreateText3DPro()
        {
            CreateObject(UIPrefabDirectory, "Text3DPro");
        }

        static T LoadPrefab<T>(string path, string name) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath(path + name + ".prefab", typeof(T)) as T;
        }
        
        static void CreateUGUIPartsObj(string name)
        {
            CreateObject(UIPrefabDirectory, name);
        }

        static void CreateObject(string path, string name)
        {
            GameObject prefab = LoadPrefab<GameObject>(path, name);
            Transform parent = Selection.activeGameObject != null ? Selection.activeGameObject.transform : null;
            GameObject obj = Object.Instantiate(prefab, parent);
            obj.name = prefab.name;
            Selection.activeGameObject = obj;
        }
    }
}
