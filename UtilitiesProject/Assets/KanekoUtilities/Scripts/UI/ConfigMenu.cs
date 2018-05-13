using UnityEngine;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menuRootObject = null;
    [SerializeField]
    Button viewConfigButton = null;

    void Start()
    {
        viewConfigButton.onClick.AddListener(() =>
        {
            if (menuRootObject.activeSelf) HideMenu();
            else ShowMenu();
        });
        
        //ここでOnClickを代入していく
        //vibrationSettingButton.OnClick += ()=>
        //{
        //    GameConfig.I.UseVibration = !GameConfig.I.UseVibration;
        //};
        //vibrationSettingButton.SetEnable(GameConfig.I.UseVibration);
    }

    public void ShowMenu()
    {
        menuRootObject.SetActive(true);
    }
    public void HideMenu()
    {
        menuRootObject.SetActive(false);
    }
}
