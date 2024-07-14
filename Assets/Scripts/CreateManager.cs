using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CreateManager : MonoBehaviour
{
    
    [SerializeField] List<GameObject> createPrefabs_1 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_2 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_3 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_4 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_5 = new List<GameObject> { };
    
    Vector2 spawnPointTrap = new Vector2(15, -0.77f); 
    Vector2 spawnPointTrap2 = new Vector2(18,-0.77f);

    int createNum;
   
    int itemCount = 0;
   

    //アイテム出現後次出現まで空ける値
    [SerializeField] int itemWaitCount;
    public bool isTraped = false;
    bool isItemed = false;
    List<GameObject> createPrefabs;


    private void Start()
    {
        createPrefabs = createPrefabs_1;
    }

    //createNum:[0~2 Trap] [8~9 Item]
    public void Panelspawn()
    {
        CreateObject(spawnPointTrap2);
    }
    public void Panelspawn2(UnityAction nextJump)
    {
        CreateObject(spawnPointTrap, nextJump);
    }


    public void CreateObject(Vector2 spawnPoint, UnityAction nextJump=null)
    {

        if (itemCount >= itemWaitCount)
        {
            isItemed = false;
            itemCount = 0;
        }

        if (isItemed == false && isTraped == false)
        {
            createNum = Random.Range(0, createPrefabs.Count);
            if (createNum <= 2)
            {
                isTraped = true;
            }
            else if (createNum >= 8)
            {
                isItemed = true;
                isTraped = false;
            }
            else
            {
                isTraped = false;
            }
        }
        else if (isItemed == false && isTraped == true)
        {
            createNum = Random.Range(3, createPrefabs.Count);

            if (createNum >= 8)
            {
                isItemed = true;
                isTraped = false;
            }
            else
            {
                isTraped = false;
            }
        }
        else if (isItemed == true && isTraped == false)
        {
            createNum = Random.Range(0, 8);
            itemCount++;
            if (createNum <= 2)
            {
                isTraped = true;
            }
            else
            {
                isTraped = false;
            }
        }
        else if (isItemed == true && isTraped == true)
        {
            createNum = Random.Range(3, 8);
            isTraped = false;
            itemCount++;
        }

        if (createNum <= 2 || createNum >= 8)
        {
            GameObject newOb = Instantiate(createPrefabs[createNum], spawnPoint, transform.rotation);//createPrefabs[createNum].transform.rotation);

        }

        nextJump?.Invoke();
       
    }


    public void OnDetectObject(Collider2D col)
    {
        
        string tagName = col.gameObject.tag;
        //Debug.Log(col.gameObject.tag);
        if(tagName == "Stage_1")
        {
            createPrefabs = createPrefabs_1;
        }
        else if (tagName == "Stage_2")
        {
            createPrefabs = createPrefabs_2;
        }
        else if (tagName == "Stage_3")
        {
            createPrefabs = createPrefabs_3;
        }
        else if (tagName == "Stage_4")
        {
            createPrefabs = createPrefabs_4;
        }
        else if (tagName == "Stage_5")
        {
            createPrefabs = createPrefabs_5;
        }
    }
}
