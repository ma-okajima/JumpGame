using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSoundController : MonoBehaviour
{

    [SerializeField] AudioClip systemSE;
    [SerializeField] AudioClip startSE;
    [SerializeField] GameObject soundOffButton;


    AudioSource audioSource;

    bool soundMuted = false;

    private void Start()
    {

        string sound = PlayerPrefs.GetString("SOUND", "OFF");
        if (sound == "OFF")
        {
            soundMuted = false;
        }
        else if (sound == "ON")
        {
            soundMuted = true;
        }
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (soundMuted)
        {
            audioSource.volume = 0;
            soundOffButton.SetActive(true);
        }
        else
        {
            audioSource.volume = 1;
        }

    }

    public void StartSE()
    {
        audioSource.PlayOneShot(startSE);
    }
    public void SyatemSE()
    {
        audioSource.PlayOneShot(systemSE);
    }


    public void SoundOff()
    {
        soundOffButton.SetActive(true);
        soundMuted = true;
        string sound = "ON";
        PlayerPrefs.SetString("SOUND", sound);
        PlayerPrefs.Save();
    }
    public void SoundOn()
    {
        soundOffButton.SetActive(false);
        soundMuted = false;
        string sound = "OFF";
        PlayerPrefs.SetString("SOUND", sound);
        PlayerPrefs.Save();
    }
}
