using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    
    [SerializeField] GameObject optionPanel;
    [SerializeField] AdMobBanner adMobBanner;
   
    [SerializeField] Image startButtonImage;
    [SerializeField] Image optionButtonImage;
    [SerializeField] Image collectionButtonImage;
    [SerializeField] List<Sprite> buttonSprites;
    [SerializeField] GameObject musicOffButton;
    [SerializeField ]TitleSoundController titleSoundController;


    AudioSource audioSource;
    

    bool BGMMuted = false;
   


    private void Start()
    {
        string bgm = PlayerPrefs.GetString("BGM", "OFF");
        if (bgm == "OFF")
        {
            BGMMuted = false;
        }else if(bgm =="ON")
        {
            BGMMuted = true;
        }
        audioSource = GetComponent<AudioSource>();
        adMobBanner.BannerStart();
    }

    private void Update()
    {
        if (BGMMuted)
        {
            audioSource.volume = 0;
            musicOffButton.SetActive(true);
        }
        else
        {
            audioSource.volume = 1;
        }
        
    }

    public void OnStartButton()
    {
        titleSoundController.StartSE();
        StartCoroutine(OnStart());
    }
    public void OnCollectionButton()
    {
        titleSoundController.SyatemSE();
        StartCoroutine(OnCollection());
    }
    public void OnOptionButton()
    {
        StartCoroutine(OnOption());
    }
    public void OptionBackButton()
    {
        titleSoundController.SyatemSE();
        optionPanel.SetActive(false);
    }

    public void ResetButton()
    {
        //PlayerPrefs.DeleteKey("HISCORE");
        PlayerPrefs.DeleteAll();
    }

    IEnumerator OnStart()
    {
        startButtonImage.sprite = buttonSprites[1];
        yield return new WaitForSeconds(0.1f);
        startButtonImage.sprite = buttonSprites[0];
        yield return new WaitForSeconds(0.5f);
        adMobBanner.BannerDestroy();
        SceneManager.LoadScene("MainScene");
    }
    IEnumerator OnOption()
    {
        titleSoundController.SyatemSE();
        optionPanel.SetActive(true);
        optionButtonImage.sprite = buttonSprites[3];
        yield return new WaitForSeconds(0.1f);
        optionButtonImage.sprite = buttonSprites[2];
    }
    IEnumerator OnCollection()
    {
        collectionButtonImage.sprite = buttonSprites[5];
        yield return new WaitForSeconds(0.1f);
        collectionButtonImage.sprite = buttonSprites[4];
        yield return new WaitForSeconds(0.5f);
        adMobBanner.BannerDestroy();
        SceneManager.LoadScene("CollectionScene");
    }

    public void BGMOff()
    {
        musicOffButton.SetActive(true);
        BGMMuted = true;
        string bgm = "ON";
        PlayerPrefs.SetString("BGM", bgm);
        PlayerPrefs.Save();
    }
    public void BGMOn()
    {
        musicOffButton.SetActive(false);
        BGMMuted = false;
        string bgm = "OFF";
        PlayerPrefs.SetString("BGM", bgm);
        PlayerPrefs.Save();
    }
}
