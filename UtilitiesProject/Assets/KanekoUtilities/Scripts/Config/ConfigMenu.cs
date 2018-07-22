using UnityEngine;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menuConteinerObject = null;
    [SerializeField]
    Button viewConfigButton = null;

    void Start()
    {
        viewConfigButton.onClick.AddListener(() =>
        {
            if (menuConteinerObject.activeSelf) HideMenu();
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
        menuConteinerObject.SetActive(true);
    }
    public void HideMenu()
    {
        menuConteinerObject.SetActive(false);
    }
}
