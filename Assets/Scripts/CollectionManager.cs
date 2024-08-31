using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    
    [SerializeField] Image collectionNameImage;
    [SerializeField] Image targetKeywordImage;
    [SerializeField] Image targetdefaultImage;
    [SerializeField] Image coverImage;
    [SerializeField] Image coverNameImage;
    [SerializeField] Image backImage;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite defaultBackSprite;
    [SerializeField] GameObject targetPanel;
    [SerializeField] GameObject targetClearImageBox;
    [SerializeField] GameObject targetkeywordImageBox;

    [SerializeField] CollectionSEController collectionSEController;

    AudioSource audioSource;
    bool BGMMuted = false;

    private void Start()
    {
        string bgm = PlayerPrefs.GetString("BGM", "OFF");
        if (bgm == "OFF")
        {
            BGMMuted = false;
        }
        else if (bgm == "ON")
        {
            BGMMuted = true;
        }
        coverNameImage = collectionNameImage;
        coverImage = targetdefaultImage;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (BGMMuted)
        {
            audioSource.volume = 0;
            
        }
        else
        {
            audioSource.volume = 1;
        }
    }

    public void TitleButton()
    {
        StartCoroutine(OnTitle());
    }

    public void ShowTargetCollection(CollectionSO collectionSO)
    {
        OnSystemSE();
        targetPanel.SetActive(true);
        if (collectionSO.Condition == true)
        {
            targetdefaultImage.sprite = collectionSO.TargetClearSprite;
            targetClearImageBox.SetActive(true);
            targetkeywordImageBox.SetActive(false);
            
        }else if (collectionSO.Condition == false)
        {
            
            targetKeywordImage.sprite= collectionSO.KeywordSprite;
            targetClearImageBox.SetActive(false);
            targetkeywordImageBox.SetActive(true);
        }

        
    }
    public void OnSystemSE()
    {
        collectionSEController.SyatemSE();
    }
    IEnumerator OnTitle()
    {
        OnSystemSE();
        backImage.sprite = backSprite;
        yield return new WaitForSeconds(0.2f);
        backImage.sprite = defaultBackSprite;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("TitleScene");
    }

    public void BackTargetPanel()
    {
        targetPanel.SetActive(false);
    }
}
