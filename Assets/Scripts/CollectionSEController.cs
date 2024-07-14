using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSEController : MonoBehaviour
{
    [SerializeField] AudioClip systemSE;

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
            
        }
        else
        {
            audioSource.volume = 1;
        }

    }

    
    public void SyatemSE()
    {
        audioSource.PlayOneShot(systemSE);
    }

}
