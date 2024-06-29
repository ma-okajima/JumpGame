using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    
    [SerializeField] GameObject optionPanel;
    [SerializeField] AdMobBanner adMobBanner;
    [SerializeField] AudioClip systemSE;
    [SerializeField] AudioClip startSE;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        adMobBanner.BannerStart();
    }
    public void OnStartButton()
    {
        audioSource.PlayOneShot(startSE);
        StartCoroutine(OnStart());
    }
    public void OnCollectionButton()
    {
        audioSource.PlayOneShot(systemSE);
        StartCoroutine(OnCollection());
    }
    public void OnOptionButton()
    {
        audioSource.PlayOneShot(systemSE);
        optionPanel.SetActive(true);
    }
    public void OptionBackButton()
    {
        audioSource.PlayOneShot(systemSE);
        optionPanel.SetActive(false);
    }

    public void ResetButton()
    {
        //PlayerPrefs.DeleteKey("HISCORE");
        PlayerPrefs.DeleteAll();
    }

    IEnumerator OnStart()
    {
        yield return new WaitForSeconds(0.5f);
        adMobBanner.BannerDestroy();
        SceneManager.LoadScene("MainScene");
    }
    IEnumerator OnCollection()
    {
        yield return new WaitForSeconds(0.5f);
        adMobBanner.BannerDestroy();
        SceneManager.LoadScene("CollectionScene");
    }
}
