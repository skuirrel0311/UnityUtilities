using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual
{
    public static class IOUtility
    {
        /// <summary>
        /// Resources の拡張。
        /// </summary>
        public static class Resources
        {
            /// <summary>
            /// 最上位の Resouces フォルダへのパス。
            /// </summary>
            public const string TopResourcesPath = "Assets/Resources/";

            /// <summary>
            /// 指定した ResourcesPath にある BinaryFile をロードします。
            /// </summary>
            /// <returns>The bytes.</returns>
            /// <param name="resourcesPath">Resources path.</param>
            public static byte[] LoadBytes (string resourcesPath)
            {
                resourcesPath = Path.Combine (
                    Path.GetDirectoryName (resourcesPath),
                    Path.GetFileNameWithoutExtension (resourcesPath)
                );
                var asset = UnityEngine.Resources.Load (resourcesPath, typeof(TextAsset)) as TextAsset;
                if (asset == null) {
                    return null;
                }
                return asset.bytes;
            }

            /// <summary>
            /// 指定した ResourcesPath にある BinaryFile から、class T としてロードします。
            /// </summary>
            /// <returns>The bytes.</returns>
            /// <param name="resourcesPath">Resources path.</param>
            /// <typeparam name="T">The 1st type parameter.</typeparam>
            public static T LoadBytes<T> (string resourcesPath)
            {
                var bytes = LoadBytes (resourcesPath);
                if (bytes == null) {
                    return default(T);
                }

                using (var stream = new MemoryStream (bytes)) {
                    var formatter = new BinaryFormatter ();
                    return (T)formatter.Deserialize (stream);
                }
            }
        }

        #if UNITY_EDITOR
        /// <summary>
        /// EDITOR ONLY.
        /// AssetDatabase の拡張。
        /// </summary>
        public static class AssetDatabase
        {
            public const string TopDirectory = "Assets/";

            public const string BytesExtention = ".bytes";
            public const string AssetExtension = ".asset";


            /// <summary>
            /// EDITOR ONLY.
            /// 指定した ScriptableObject を AssetDatabase に保存します。
            /// </summary>
            /// <returns><c>true</c>, if scriptable object was saved, <c>false</c> otherwise.</returns>
            /// <param name="directoryPath">Directory path.</param>
            /// <param name="asset">Asset.</param>
            /// <param name="overwrite">If set to <c>true</c> overwrite.</param>
            public static bool SaveScriptableObject (string directoryPath, ScriptableObject asset, bool overwrite = false)
            {
                if (string.IsNullOrEmpty (directoryPath) || !directoryPath.StartsWith (TopDirectory)) {
                    Debug.LogErrorFormat ("Wrong Directory Path. It must start with \"{0}\".", TopDirectory);
                    return false;
                }
                if (asset == null || string.IsNullOrEmpty (asset.name)) {
                    Debug.LogErrorFormat ("Wrong Asset. It must have a name.");
                    return false;
                }

                CreateFolderRecursively (directoryPath);

                var assetName = string.Format ("{0}{1}", asset.name, AssetExtension);
                var projectPath = System.IO.Path.Combine (directoryPath, assetName);
                if (!overwrite) {
                    projectPath = UnityEditor.AssetDatabase.GenerateUniqueAssetPath (projectPath);
                }

                UnityEditor.AssetDatabase.CreateAsset (asset, projectPath);
                EditorUtility.SetDirty (asset);
                UnityEditor.AssetDatabase.SaveAssets ();
                UnityEditor.AssetDatabase.Refresh ();

                return true;
            }

            /// <summary>
            /// EDITOR ONLY.
            /// object を BinaryFile として AssetDatabase に保存します。
            /// </summary>
            /// <param name="resourcesPath">Resources path.</param>
            /// <param name="data">Data.</param>
            /// <param name="overwrite">If set to <c>true</c> overwrite.</param>
            public static bool SaveAsBinaryFile (string directoryPath, string name, object data, bool overwrite = false)
            {
                if (string.IsNullOrEmpty (directoryPath) || !directoryPath.StartsWith (TopDirectory)) {
                    Debug.LogErrorFormat ("Wrong Directory Path. It must start with {0}", TopDirectory);
                    return false;
                }
                if (string.IsNullOrEmpty (name)) {
                    Debug.LogError ("Wrong asset name.");
                    return false;
                }

                CreateFolderRecursively (directoryPath);

                if (!name.EndsWith (BytesExtention)) {
                    name += BytesExtention;
                }
                directoryPath = Path.Combine (directoryPath, name);
                if (!overwrite) {
                    directoryPath = UnityEditor.AssetDatabase.GenerateUniqueAssetPath (directoryPath);
                }

                using (var stream = new FileStream (directoryPath, FileMode.Create, FileAccess.Write)) {
                    var formatter = new BinaryFormatter ();
                    formatter.Serialize (stream, data);
                }

                UnityEditor.AssetDatabase.SaveAssets ();

                return true;
            }

            /// <summary>
            /// EDITOR ONLY.
            /// 指定した AssetDatabasePath から、 BinaryFile を class T としてロードします。
            /// </summary>
            /// <returns>The binary file as.</returns>
            /// <param name="resourcesPath">Resources path.</param>
            /// <typeparam name="T">The 1st type parameter.</typeparam>
            public static T LoadBinaryFileAs<T> (string assetsPath)
            {
                if (string.IsNullOrEmpty (assetsPath) || !assetsPath.StartsWith (TopDirectory)) {
                    Debug.LogErrorFormat ("Wrong Assets path. It must start with {0}", TopDirectory);
                    return default(T);
                }

                if (!assetsPath.EndsWith (BytesExtention)) {
                    assetsPath += BytesExtention;
                }
                if (!File.Exists (assetsPath)) {
                    Debug.LogErrorFormat ("Wrong Assets path. It is not found: {0}", assetsPath);
                    return default(T);
                }

                using (var stream = new FileStream (assetsPath, FileMode.Open, FileAccess.Read)) {
                    var formatter = new BinaryFormatter ();
                    var data = (T)formatter.Deserialize (stream);
                    return data;
                }
            }

            /// <summary>
            /// EDITOR ONLY. For Resources, use "UnityEngine.Resources.LoadAll<T>".
            /// 指定した AssetDatabasePath に存在する class T のアセットをすべて取得します。 
            /// </summary>
            /// <returns>The assets.</returns>
            /// <param name="directoryPath">Directory path.</param>
            /// <param name="option">Option.</param>
            /// <typeparam name="T">The 1st type parameter.</typeparam>
            public static T[] LoadAll<T> (string assetsPath, SearchOption option = SearchOption.TopDirectoryOnly) where T : UnityEngine.Object
            {
                var assets = new List<T> ();

                if (string.IsNullOrEmpty (assetsPath) || !assetsPath.StartsWith (TopDirectory)) {
                    Debug.LogErrorFormat ("Wrong Assets path. It must start with {0}", TopDirectory);
                    return assets.ToArray ();
                }

                if (!Directory.Exists (assetsPath)) {
                    Debug.LogErrorFormat ("Wrong Assets path. It is not found: {0}", assetsPath);
                    return assets.ToArray ();
                }

                string[] paths = Directory.GetFiles (assetsPath, "*", option);
                foreach (var path in paths) {
                    var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T> (path);
                    if (asset != null) {
                        assets.Add (asset);
                    }
                }
                return assets.ToArray ();
            }

            public static bool RenameAsset(Object obj, string name) {

                if (obj == null) {
                    Debug.LogErrorFormat ("Obj must not be null.");
                    return false;
                }
                if (!UnityEditor.AssetDatabase.IsMainAsset(obj)) {
                    Debug.LogFormat ("Obj is not a main asset.");
                    return false;
                }

                if (string.IsNullOrEmpty(name)) {
                    Debug.LogErrorFormat ("Obj must not be null or empty.");
                    return false;
                }

                var path = UnityEditor.AssetDatabase.GetAssetPath(obj);
                var result = UnityEditor.AssetDatabase.RenameAsset(path, name);
                if (result.Length > 0) {
                    Debug.LogErrorFormat(result);
                    return false;
                } else {
                    UnityEditor.EditorUtility.SetDirty(obj);
                    return true;
                }
            }


            /// <summary>
            /// 複数階層のフォルダを作成します。
            /// </summary>
            /// <remarks>パスは"Assets/"で始まっている必要があります。Splitなので最後のスラッシュ(/)は不要です</remarks>
            public static bool CreateFolderRecursively (string assetsPath)
            {
                if (string.IsNullOrEmpty (assetsPath) || !assetsPath.StartsWith (TopDirectory)) {
                    Debug.LogErrorFormat ("Wrong Directory Path. It must start with \"{0}\".", TopDirectory);
                    return false;
                }

                // もう存在すれば処理は不要
                if (UnityEditor.AssetDatabase.IsValidFolder (assetsPath))
                    return true;

                // スラッシュで終わっていたら除去
                if (assetsPath [assetsPath.Length - 1] == '/') {
                    assetsPath = assetsPath.Substring (0, assetsPath.Length - 1);
                }

                var names = assetsPath.Split ('/');
                for (int i = 1; i < names.Length; i++) {
                    var parent = string.Join ("/", names.Take (i).ToArray ());
                    var target = string.Join ("/", names.Take (i + 1).ToArray ());
                    var child = names [i];
                    if (!UnityEditor.AssetDatabase.IsValidFolder (target)) {
                        UnityEditor.AssetDatabase.CreateFolder (parent, child);
                    }
                }

                return true;
            }
        }
        #endif

        /// <summary>
        /// 暗号化PlayerPrefs
        /// https://gist.github.com/naichilab/f72fe6aeca0467454c3d7b0ef285cb96
        /// </summary>
        public static class PlayerPrefs
        {
            const string TrueString = "1";
            const string FalseString = "0";

            #region Set

            /// <summary>
            /// Int 値を設定します。
            /// 保存はしないので Save() を実行してください。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SetEncryptedInt (string key, int value)
            {
                string valueString = value.ToString ();
                SetEncryptedString (key, valueString);
            }
            /// <summary>
            /// Float 値を設定します。
            /// 保存はしないので Save() を実行してください。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SetEncryptedFloat (string key, float value)
            {
                string valueString = value.ToString ();
                SetEncryptedString (key, valueString);
            }
            /// <summary>
            /// Bool 値を設定します。
            /// 保存はしないので Save() を実行してください。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SetEncryptedBool (string key, bool value)
            {
                string valueString = (value == true) ? TrueString : FalseString;
                SetEncryptedString (key, valueString);
            }
            /// <summary>
            /// (シリアライズ可能な) object を設定します。
            /// 保存はしないので Save() を実行してください。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SetEncryptedObject (string key, object value)
            {
                string json = JsonUtility.ToJson (value);
                SetEncryptedString (key, json);
            }
            /// <summary>
            /// 文字列を設定します。
            /// 保存はしないので Save() を実行してください。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SetEncryptedString (string key, string value)
            {
                string encKey = Enc.EncryptString (key);
                string encValue = Enc.EncryptString (value.ToString ());
                UnityEngine.PlayerPrefs.SetString (encKey, encValue);
            }

            #endregion


            #region Save

            /// <summary>
            /// Int 値を設定し、保存を行います。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SaveEncryptedInt (string key, int value)
            {
                SetEncryptedInt(key, value);
                Save();
            }
            /// <summary>
            /// Float 値を設定し、保存を行います。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SaveEncryptedFloat (string key, float value)
            {
                SetEncryptedFloat(key, value);
                Save();
            }
            /// <summary>
            /// Bool 値を設定し、保存を行います。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">If set to <c>true</c> value.</param>
            public static void SaveEncryptedBool (string key, bool value)
            {
                SetEncryptedBool(key, value);
                Save();
            }
            /// <summary>
            /// (シリアライズ可能な) object を設定し、保存を行います。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SaveEncryptedObject (string key, object value)
            {
                SetEncryptedObject(key, value);
                Save();
            }
            /// <summary>
            /// 文字列を設定し、保存を行います。
            /// </summary>
            /// <param name="key">Key.</param>
            /// <param name="value">Value.</param>
            public static void SaveEncryptedString (string key, string value)
            {
                SetEncryptedString(key, value);
                Save();
            }
            /// <summary>
            /// PlayerPrefs を保存します。
            /// </summary>
            public static void Save ()
            {
                UnityEngine.PlayerPrefs.Save ();
            }

            #endregion


            #region Load

            /// <summary>
            /// 保存されている Int 値を取得します。
            /// Key がない場合は、デフォルト値を取得します（保存は行いません）。
            /// </summary>
            /// <returns>The encrypted int.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">Default value.</param>
            public static int LoadEncryptedInt (string key, int defaultValue)
            {
                string defaultValueString = defaultValue.ToString ();
                string valueString = LoadEncryptedString (key, defaultValueString);

                int res;
                if (int.TryParse (valueString, out res)) {
                    return res;
                }
                return defaultValue;
            }
            /// <summary>
            /// 保存されている Float 値を取得します。
            /// Key がない場合は、デフォルト値を取得します（保存は行いません）。
            /// </summary>
            /// <returns>The encrypted float.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">Default value.</param>
            public static float LoadEncryptedFloat (string key, float defaultValue)
            {
                string defaultValueString = defaultValue.ToString ();
                string valueString = LoadEncryptedString (key, defaultValueString);

                float res;
                if (float.TryParse (valueString, out res)) {
                    return res;
                }
                return defaultValue;
            }
            /// <summary>
            /// 保存されている Bool 値を取得します。
            /// Key がない場合は、デフォルト値を取得します（保存は行いません）。
            /// </summary>
            /// <returns>The encypted bool.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">If set to <c>true</c> default value.</param>
            public static bool LoadEncryptedBool (string key, bool defaultValue)
            {
                string defaultValueString = (defaultValue == true) ? TrueString : FalseString;
                string valueString = LoadEncryptedString (key, defaultValueString);

                if (!string.IsNullOrEmpty (valueString)) {
                    return valueString == TrueString;
                }
                return defaultValue;
            }
            /// <summary>
            /// 保存されている object を取得します。 object はシリアライズ可能なものに限ります。
            /// </summary>
            /// <returns>The encrypted object.</returns>
            /// <param name="key">Key.</param>
            /// <typeparam name="T">The 1st type parameter.</typeparam>
            public static T LoadEncryptedObject<T> (string key)
            {
                string valueString = LoadEncryptedString (key, string.Empty);
                if (!string.IsNullOrEmpty (valueString)) {
                    return JsonUtility.FromJson<T> (valueString);
                }
                return default(T);
            }
            /// <summary>
            /// 保存されている文字列を取得します。
            /// Key がない場合は、デフォルト値を取得します（保存は行いません）。
            /// </summary>
            /// <returns>The encrypted string.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">Default value.</param>
            public static string LoadEncryptedString (string key, string defaultValue)
            {
                string encKey = Enc.EncryptString (key);
                string encString = UnityEngine.PlayerPrefs.GetString (encKey, string.Empty);
                if (string.IsNullOrEmpty (encString)) {
                    return defaultValue;
                }
                string decryptedValueString = Enc.DecryptString (encString);
                return decryptedValueString;
            }

            #endregion


            #region Load and Save

            /// <summary>
            /// 保存されている Int 値を取得します。
            /// Key がない場合は、デフォルト値をその Key で保存し、デフォルト値を取得します。
            /// </summary>
            /// <returns>The encrypted int.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">Default value.</param>
            public static int LoadAndSaveEncryptedInt (string key, int defaultValue)
            {
                string defaultValueString = defaultValue.ToString ();
                string valueString = LoadEncryptedString (key, defaultValueString);

                int res;
                if (!int.TryParse(valueString, out res)) {
                    res = defaultValue;
                }

                SaveEncryptedInt(key, res);
                return res;
            }
            /// <summary>
            /// 保存されている Float 値を取得します。
            /// Key がない場合は、デフォルト値をその Key で保存し、デフォルト値を取得します。
            /// </summary>
            /// <returns>The encrypted float.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">Default value.</param>
            public static float LoadAndSaveEncryptedFloat (string key, float defaultValue)
            {
                string defaultValueString = defaultValue.ToString ();
                string valueString = LoadEncryptedString (key, defaultValueString);

                float res;
                if (!float.TryParse (valueString, out res)) {
                    res = defaultValue;
                }

                SaveEncryptedFloat(key, res);
                return res;
            }
            /// <summary>
            /// 保存されている Bool 値を取得します。
            /// Key がない場合は、デフォルト値をその Key で保存し、デフォルト値を取得します。
            /// </summary>
            /// <returns>The encrypted bool.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">If set to <c>true</c> default value.</param>
            public static bool LoadAndSaveEncryptedBool (string key, bool defaultValue)
            {
                string defaultValueString = (defaultValue == true) ? TrueString : FalseString;
                string valueString = LoadEncryptedString (key, defaultValueString);

                bool res;
                if (!string.IsNullOrEmpty(valueString)) {
                    res =  valueString == TrueString;
                } else {
                    res = defaultValue;
                }

                SaveEncryptedBool(key, res);
                return res;
            }
            /// <summary>
            /// 保存されている文字列を取得します。
            /// Key がない場合は、デフォルト値をその Key で保存し、デフォルト値を取得します。
            /// </summary>
            /// <returns>The encrypted string.</returns>
            /// <param name="key">Key.</param>
            /// <param name="defaultValue">Default value.</param>
            public static string LoadAndSaveEncryptedString (string key, string defaultValue)
            {
                string encKey = Enc.EncryptString (key);
                string encString = UnityEngine.PlayerPrefs.GetString (encKey, string.Empty);

                string res;
                if (string.IsNullOrEmpty(encString)) {
                    res = defaultValue;
                } else {
                    res = Enc.DecryptString (encString);
                }

                if (!string.IsNullOrEmpty(res)) {
                    SaveEncryptedString(key, res);
                }
                return res;
            }

            #endregion


            #region Delete

            /// <summary>
            /// 指定した Key の値を削除します。
            /// （IOUtility.PreyerPrefs は、 Key も暗号化するため、通常の UnityEngine.PlayerPrefs.DeleteKey では削除できません。）
            /// </summary>
            /// <param name="key">Key.</param>
            public static void DeleteEncryptedKey (string key)
            {
                string encKey = Enc.EncryptString (key);
                UnityEngine.PlayerPrefs.DeleteKey (encKey);
            }
            public static void DeleteAll ()
            {
                UnityEngine.PlayerPrefs.DeleteAll ();
            }

            #endregion


            /// <summary>
            /// 指定した Key が存在するかを取得します。
            /// （IOUtility.PreyerPrefs は、 Key も暗号化するため、通常の UnityEngine.PlayerPrefs.HasKey では取得できません。）
            /// </summary>
            /// <returns><c>true</c> if has encrypted key the specified key; otherwise, <c>false</c>.</returns>
            /// <param name="key">Key.</param>
            public static bool HasEncryptedKey (string key)
            {
                string encKey = Enc.EncryptString (key);
                return UnityEngine.PlayerPrefs.HasKey (encKey);
            }



            /// <summary>
            /// 文字列の暗号化・復号化
            /// 参考：http://dobon.net/vb/dotnet/string/encryptstring.html
            /// </summary>
            private static class Enc
            {
                // ▼ 要変更
                const string PASS = "sVWb3tuPSKZEg3H9qapmRwur";
                const string SALT = "YpUd9eFRH6r7tf3n2dMLPAsc";

                static System.Security.Cryptography.RijndaelManaged rijndael;

                static Enc ()
                {
                    //RijndaelManagedオブジェクトを作成
                    rijndael = new System.Security.Cryptography.RijndaelManaged ();
                    byte[] key, iv;
                    GenerateKeyFromPassword (rijndael.KeySize, out key, rijndael.BlockSize, out iv);
                    rijndael.Key = key;
                    rijndael.IV = iv;
                }


                /// <summary>
                /// 文字列を暗号化する
                /// </summary>
                /// <param name="sourceString">暗号化する文字列</param>
                /// <returns>暗号化された文字列</returns>
                public static string EncryptString (string sourceString)
                {
                    //文字列をバイト型配列に変換する
                    byte[] strBytes = System.Text.Encoding.UTF8.GetBytes (sourceString);
                    //対称暗号化オブジェクトの作成
                    System.Security.Cryptography.ICryptoTransform encryptor = rijndael.CreateEncryptor ();
                    //バイト型配列を暗号化する
                    byte[] encBytes = encryptor.TransformFinalBlock (strBytes, 0, strBytes.Length);
                    //閉じる
                    encryptor.Dispose ();
                    //バイト型配列を文字列に変換して返す
                    return System.Convert.ToBase64String (encBytes);
                }

                /// <summary>
                /// 暗号化された文字列を復号化する
                /// </summary>
                /// <param name="sourceString">暗号化された文字列</param>
                /// <returns>復号化された文字列</returns>
                public static string DecryptString (string sourceString)
                {
                    //文字列をバイト型配列に戻す
                    byte[] strBytes = System.Convert.FromBase64String (sourceString);
                    //対称暗号化オブジェクトの作成
                    System.Security.Cryptography.ICryptoTransform decryptor = rijndael.CreateDecryptor ();
                    //バイト型配列を復号化する
                    //復号化に失敗すると例外CryptographicExceptionが発生
                    byte[] decBytes = decryptor.TransformFinalBlock (strBytes, 0, strBytes.Length);
                    //閉じる
                    decryptor.Dispose ();
                    //バイト型配列を文字列に戻して返す
                    return System.Text.Encoding.UTF8.GetString (decBytes);
                }

                /// <summary>
                /// パスワードから共有キーと初期化ベクタを生成する
                /// </summary>
                /// <param name="password">基になるパスワード</param>
                /// <param name="keySize">共有キーのサイズ（ビット）</param>
                /// <param name="key">作成された共有キー</param>
                /// <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
                /// <param name="iv">作成された初期化ベクタ</param>
                private static void GenerateKeyFromPassword (int keySize, out byte[] key, int blockSize, out byte[] iv)
                {
                    //パスワードから共有キーと初期化ベクタを作成する
                    //saltを決める
                    byte[] salt = System.Text.Encoding.UTF8.GetBytes (SALT);//saltは必ず8byte以上
                    //Rfc2898DeriveBytesオブジェクトを作成する
                    System.Security.Cryptography.Rfc2898DeriveBytes deriveBytes = new System.Security.Cryptography.Rfc2898DeriveBytes (PASS, salt);
                    //.NET Framework 1.1以下の時は、PasswordDeriveBytesを使用する
                    //System.Security.Cryptography.PasswordDeriveBytes deriveBytes =
                    //    new System.Security.Cryptography.PasswordDeriveBytes(password, salt);
                    //反復処理回数を指定する デフォルトで1000回
                    deriveBytes.IterationCount = 1000;
                    //共有キーと初期化ベクタを生成する
                    key = deriveBytes.GetBytes (keySize / 8);
                    iv = deriveBytes.GetBytes (blockSize / 8);
                }
            }
        }
    }
}
