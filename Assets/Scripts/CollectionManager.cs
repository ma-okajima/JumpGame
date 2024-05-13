using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI collectionName;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image defaultSprite;
    Sprite coverSprite;
    string coverName;
    private void Start()
    {
        coverName = collectionName.text;
        coverSprite = defaultSprite.sprite;
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ShowTargetCollection(CollectionSO collectionSO)
    {
        if (collectionSO.Condition == true)
        {
            defaultSprite.sprite = collectionSO.Sprite;
            collectionName.text = collectionSO.Name;
            descriptionText.text = collectionSO.Text;
        }else if (collectionSO.Condition == false)
        {
            defaultSprite.sprite = coverSprite;
            collectionName.text = coverName;
            descriptionText.text = collectionSO.Keyword;
        }

        
    }
}
