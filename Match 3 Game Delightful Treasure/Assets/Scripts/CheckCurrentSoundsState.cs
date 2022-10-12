using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CheckCurrentSoundsState : MonoBehaviour
{
    public AudioMixerGroup grp;

    public Image sounds;
    public Image music;

    public Sprite soundsOn;
    public Sprite soundsOff;

    public Sprite musicOn;
    public Sprite musicOff;
    // Start is called before the first frame update
    void Start()
    {
        if (GetMasterLevel("sfx_volume") == -13)
            sounds.sprite = soundsOn;
        else
            sounds.sprite = soundsOff;
        if (GetMasterLevel("music_volume") == 0)
            music.sprite = musicOn;
        else
            music.sprite = musicOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundsOnOff()
    {
        if (sounds.sprite == soundsOn)
        {
            sounds.sprite = soundsOff;
            grp.audioMixer.SetFloat("sfx_volume", -80);
            PlayerPrefs.SetInt("sfx_on", 0);
        }
        else
        {
            sounds.sprite = soundsOn;
            grp.audioMixer.SetFloat("sfx_volume", -13);
            PlayerPrefs.SetInt("sfx_on", 1);
        }

    }
    public void MusicOnOff()
    {
        if (music.sprite == musicOn)
        {
            music.sprite = musicOff;
            grp.audioMixer.SetFloat("music_volume", -80);
            PlayerPrefs.SetInt("music_on", 0);
        }
        else
        {
            music.sprite = musicOn;
            grp.audioMixer.SetFloat("music_volume", 0);
            PlayerPrefs.SetInt("music_on", 1);
        }
    }
    public float GetMasterLevel(string channelName)
    {
        float value;
        bool result = grp.audioMixer.GetFloat(channelName, out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }
}
