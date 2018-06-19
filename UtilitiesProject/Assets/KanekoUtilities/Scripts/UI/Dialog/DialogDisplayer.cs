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

        public OKDialog ShowOKDialog(string dialogName)
        {
            Dialog dialog = ShowDialog(dialogName);
            if (dialog == null) return null;

            OKDialog oKDialog = (OKDialog)dialog;
            if (oKDialog == null)
            {
                Debug.LogError(dialogName + " does not match the type " + '"' + "OkDialog" + '"' + ".");
                return null;
            }

            return oKDialog;
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