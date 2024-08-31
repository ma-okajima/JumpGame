using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItemManager : MonoBehaviour
{
    [SerializeField] CollectionSO collectionSO;
    [SerializeField] CollectionManager collectionManager;
    [SerializeField] GameObject coverimageBox;
    [SerializeField] GameObject itemImageBox;
    [SerializeField] Image itemImage;
    [SerializeField] Image coverImage;

    private void Start()
    {
        
        int key = PlayerPrefs.GetInt(collectionSO.Name, 0);
        itemImage.sprite = collectionSO.Sprite;
        coverImage.sprite = collectionSO.Coversprite;
        if(key == 0)
        {
            collectionSO.Condition = false;
            itemImageBox.SetActive(false);
            coverimageBox.SetActive(true);
        }else if (key == 1)
        {
            collectionSO.Condition = true;
            itemImageBox.SetActive(true);
            coverimageBox.SetActive(false);
            
        }

    }

    public void TargetCollection()
    {
        collectionManager.ShowTargetCollection(collectionSO);
    }
}
