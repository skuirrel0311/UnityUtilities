using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using HyperCommon;

namespace KanekoUtilities
{
    public enum SaveKeyName { TotalLoginDays, LastLoginDate }

    /// <summary>
    /// PlayerPrefsのラッパー(のちにHyperCommonのIOUtilies.PlayerPrefsと差し替えるため)
    /// </summary>
    public static class MyPlayerPrefs
    {
        public static void SaveInt(SaveKeyName key, int value)
        {
            PlayerPrefs.SetInt(key.ToString(), value);
        }
        public static void SaveString(SaveKeyName key, string value)
        {
            PlayerPrefs.SetString(key.ToString(), value);
        }
        public static void SaveFloat(SaveKeyName key, float value)
        {
            PlayerPrefs.SetFloat(key.ToString(), value);
        }
        public static void SaveBool(SaveKeyName key, bool value)
        {
            PlayerPrefs.SetInt(key.ToString(), value ? 1 : 0);
        }

        public static int LoadInt(SaveKeyName key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key.ToString(), defaultValue);
        }
        public static string LoadString(SaveKeyName key, string defaultValue)
        {
            return PlayerPrefs.GetString(key.ToString(), defaultValue);
        }
        public static float LoadFloat(SaveKeyName key, float defaultValue)
        {
            return PlayerPrefs.GetFloat(key.ToString(), defaultValue);
        }
        public static bool LoadBool(SaveKeyName key, bool defaultValue)
        {
            return PlayerPrefs.GetInt(key.ToString(), defaultValue ? 1 : 0) == 1;
        }

        /*// HyperCommonを導入したらこちらをコメントアウトして使う
        public static void SaveInt(SaveKeyName key, int value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedInt(key.ToString(), value);
        }
        public static void SaveString(SaveKeyName key, string value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedString(key.ToString(), value);
        }
        public static void SaveFloat(SaveKeyName key, float value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedFloat(key.ToString(), value);
        }
        public static void SaveBool(SaveKeyName key, bool value)
        {
            IOUtility.PlayerPrefs.SaveEncryptedBool(key.ToString(), value);
        }
        public static int LoadInt(SaveKeyName key, int defaultValue)
        {
            return IOUtility.PlayerPrefs.LoadEncryptedInt(key.ToString(), defaultValue);
        }
        public static string LoadString(SaveKeyName key, string defaultValue)
        {
            return IOUtility.PlayerPrefs.LoadEncryptedString(key.ToString(), defaultValue);
        }
        public static float LoadFloat(SaveKeyName key, float defaultValue)
        {
            return IOUtility.PlayerPrefs.LoadEncryptedFloat(key.ToString(), defaultValue);
        }
        public static bool LoadBool(SaveKeyName key, bool defaultValue)
        {
            return IOUtility.PlayerPrefs.LoadEncryptedBool(key.ToString(), defaultValue);
        }
        */
    }
}
