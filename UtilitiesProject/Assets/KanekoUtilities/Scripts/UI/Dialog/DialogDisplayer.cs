using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class DialogDisplayer : SingletonMonobehaviour<DialogDisplayer>
    {
        Dictionary<string, Dialog> dialogDictionary = new Dictionary<string, Dialog>();

        public Dialog ShowDialog(string dialogName)
        {
            Dialog dialog = GetDialog(dialogName);

            dialog.Show();

            return dialog;
        }

        public T ShowDialog<T>(string dialogName) where T : Dialog
        {
            Dialog temp = ShowDialog(dialogName);
            if (temp == null) return null;

            T dialog = (T)temp;
            if (dialog == null)
            {
                Debug.LogError(dialogName + " does not match the type " + '"' + typeof(T).Name + '"' + ".");
                return null;
            }

            return dialog;
        }

        Dialog GetDialog(string dialogName)
        {
            Dialog dialog = null;

            if(dialogDictionary.TryGetValue(dialogName,out dialog))
            {
                return dialog;
            }

            Dialog dialogPrefab = MyAssetStore.Instance.GetAsset<Dialog>(dialogName, "Dialogs/");

            if (dialogPrefab == null) return null;

            dialog = Instantiate(dialogPrefab, transform);

            dialogDictionary.Add(dialogName, dialog);

            return dialog;
        }
    }
}