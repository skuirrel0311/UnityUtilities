using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class DialogDisplayer : SingletonMonobehaviour<DialogDisplayer>
    {
        [SerializeField]
        UGUIImage panel = null;

        //ゆくゆくは複数出すのにも対応したい
        //List<Dialog> currentViewDialog = new List<Dialog>();
        
        Dictionary<string, Dialog> dialogDictionary = new Dictionary<string, Dialog>();

        protected override void Awake()
        {
            base.Awake();

            panel.gameObject.SetActive(false);
        }

        public Dialog ShowDialog(string dialogName)
        {
            Dialog dialog = GetDialog(dialogName);

            ShowDialog(dialog);

            return dialog;
        }

        public T ShowDialog<T>(string dialogName) where T : Dialog
        {
            Dialog temp = GetDialog(dialogName);
            if (temp == null) return null;

            T dialog = (T)temp;
            if (dialog == null)
            {
                Debug.LogError(dialogName + " does not match the type " + '"' + typeof(T).Name + '"' + ".");
                return null;
            }

            ShowDialog(dialog);

            return dialog;
        }

        public void ShowDialog(Dialog dialog)
        {
            panel.gameObject.SetActive(true);

            dialog.Show();
            
            StartCoroutine(KKUtilities.WaitAction(dialog.OnHideAnimationEnd,() =>
            {
                panel.gameObject.SetActive(false);
            }));
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