using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if IMPORT_HYPERCOMMON
using HyperCasual;
#endif

namespace KanekoUtilities
{
    /// <summary>
    /// PlayerPrefsのラッパー(のちにHyperCommonのIOUtilies.PlayerPrefsと差し替えるため)
    /// </summary>
    public static class MyPlayerPrefs
    {
#if !INPORT_HYPERCOMMON
        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        public static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        public static string LoadString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        public static float LoadFloat(string key, float defaultValue = 0.0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
        public static bool LoadBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }
        
        public static void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
#else
        public static void SaveInt(string key, int value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedInt(key, value);
        }
        public static void SaveString(string key, string value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedString(key, value);
        }
        public static void SaveFloat(string key, float value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedFloat(key, value);
        }
        public static void SaveBool(string key, bool value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedBool(key, value);
        }
        public static int LoadInt(string key, int defaultValue = 0)
        {
            return IOUtility.PlayerPrefs.LoadEncryptedInt(key, defaultValue);
        }
        public static string LoadString(string key, string defaultValue = "")
        {
            return IOUtility.PlayerPrefs.LoadEncryptedString(key, defaultValue);
        }
        public static float LoadFloat(string key, float defaultValue = 0.0f)
        {
            return IOUtility.PlayerPrefs.LoadEncryptedFloat(key, defaultValue);
        }
        public static bool LoadBool(string key, bool defaultValue = false)
        {
            return IOUtility.PlayerPrefs.LoadEncryptedBool(key, defaultValue);
        }
        
        public static void Delete(string key)
        {
            IOUtility.PlayerPrefs.DeleteEncryptedKey(key);
        }
        public static void DeleteAll()
        {
            IOUtility.PlayerPrefs.DeleteAll();
        }
#endif
    }
}
