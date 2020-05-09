using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public  AudioSource bgmAudioSource;
    public AudioClip seaWaveClip;
    public AudioClip goldClip;
    public AudioClip rewardClip;
    public AudioClip fireClip;   
    public AudioClip changeClip;
    public AudioClip leveUpClip;

    private static AudioManager _instance;

    public static AudioManager Instance{
        get{
            return _instance;
        }
    }

    private bool isMute = false;
    void Awake(){
        _instance = this;
        isMute = PlayerPrefs.GetInt("mute",0) == 0 ? false : true ;
        Domute();

    }

    public void PlayEffectSound(AudioClip clip){
        if(!isMute){
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        } 
    }
    
    public void SwitchMuteState(bool isOn){
        isMute = !isOn;
        Domute();
    }

    public bool IsMute{
        get{
            return isMute;
        }
    }

    void Domute(){
        if(isMute){
            bgmAudioSource.Pause();
        }   
        else{
            bgmAudioSource.Play();
        }
    }
}
