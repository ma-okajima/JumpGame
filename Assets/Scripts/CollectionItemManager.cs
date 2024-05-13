using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItemManager : MonoBehaviour
{
    [SerializeField] CollectionSO collectionSO;
    [SerializeField] CollectionManager collectionManager;
    [SerializeField] GameObject coverImage;
  

    private void Start()
    {
        
        int key = PlayerPrefs.GetInt(collectionSO.name, 0);
        if(key == 0)
        {
            collectionSO.Condition = false;
            coverImage.SetActive(true);
        }else if (key == 1)
        {
            collectionSO.Condition = true;
            coverImage.SetActive(false);
        }

    }

    public void TargetCollection()
    {
        collectionManager.ShowTargetCollection(collectionSO);
    }
}
