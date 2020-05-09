using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public Toggle muteToogle;
    public GameObject settingPanel;

    public void  Start(){
        muteToogle.isOn = !AudioManager.Instance.IsMute;
    }
     
    public void SwitchMute(bool isOn)
    {
        AudioManager.Instance.SwitchMuteState(isOn);
    }

    public void OnBackButtonDown()
    {
        PlayerPrefs.SetInt("gold",CameManager.Instance.gold);
        PlayerPrefs.SetInt("lv",CameManager.Instance.lv);
        PlayerPrefs.SetFloat("scd",CameManager.Instance.smallTimer);
        PlayerPrefs.SetFloat("bcd",CameManager.Instance.bigTimer);
        PlayerPrefs.SetInt("exp",CameManager.Instance.exp);
        int tmp = AudioManager.Instance.IsMute== false ? 0 : 1;
        PlayerPrefs.SetInt("mute",tmp);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void OnSettingButtonDown()
    {
        settingPanel.SetActive(true);
    }

    public void OnCloseButtonDown()
    {
        settingPanel.SetActive(false);
    }
    
}
