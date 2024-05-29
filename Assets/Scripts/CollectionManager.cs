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
    private void Start()
    {
        coverNameImage = collectionNameImage;
        coverImage = defaultImage;
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ShowTargetCollection(CollectionSO collectionSO)
    {
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
}
