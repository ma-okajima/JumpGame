using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    
    [SerializeField] Image collectionNameImage;
    [SerializeField] Image descriptionImage;
    [SerializeField] Image defaultImage;
    [SerializeField] Image coverImage;
    [SerializeField] Image coverNameImage;

    [SerializeField] AudioClip systemSE;

    AudioSource audioSource;

    private void Start()
    {
        coverNameImage = collectionNameImage;
        coverImage = defaultImage;
        audioSource = GetComponent<AudioSource>();
    }

    public void TitleButton()
    {
        StartCoroutine(OnTitle());
    }

    public void ShowTargetCollection(CollectionSO collectionSO)
    {
        OnSystemSE();
        if (collectionSO.Condition == true)
        {
            defaultImage.sprite = collectionSO.Sprite;
            collectionNameImage.sprite= collectionSO.NameSprite;
            descriptionImage.sprite= collectionSO.TextSprite;
        }else if (collectionSO.Condition == false)
        {
            defaultImage= coverImage;
            collectionNameImage= coverNameImage;
            descriptionImage.sprite= collectionSO.KeywordSprite;
        }

        
    }
    public void OnSystemSE()
    {
        audioSource.PlayOneShot(systemSE);
    }
    IEnumerator OnTitle()
    {
        OnSystemSE();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("TitleScene");
    }
}
