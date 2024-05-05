using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    [SerializeField] GameObject jumpPanel;
    [SerializeField] GameObject jumpGround;
    [SerializeField]  List<GameObject> createPrefabs = new List<GameObject>{};
    
    
    Vector2 spawnPointJG = new Vector2(42, 0);
    Vector2 spawnPointTrap = new Vector2(12, -0.5f);
    Vector2 spawnPointTrap2 = new Vector2(15, -0.5f);

    int createNum;
    int itemCount = 0;
    bool isTraped = false;
    bool isItemed = false;

    private void Update()
    {
     
        if (GameManager.instance.isJgCreared == true)
        {
            JGSpawn();
            GameManager.instance.isJgCreared = false;
        }
    }

    //createNum:[0~2 Trap] [8~9 Item]
    public void Panelspawn()
    {
        CreateObject();
        if(createNum <= 2 ||createNum>=8)
        {
            GameObject newOb = Instantiate(createPrefabs[createNum], spawnPointTrap,transform.rotation);
            
        }
        
        
    }
    public void Panelspawn2()
    {
        CreateObject();
        if (createNum <= 2 || createNum >= 8)
        {
            GameObject newOb = Instantiate(createPrefabs[createNum], spawnPointTrap2, transform.rotation);
            
        }
       
    }
 
    public void JGSpawn()
    {
        GameObject newPanel = Instantiate(jumpGround, spawnPointJG, Quaternion.identity);
    }

    void CreateObject()
    {
        if (itemCount >= 3)
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
                }
        }
        else if (isItemed == false && isTraped == true)
        {
                createNum = Random.Range(3, createPrefabs.Count);
            if (createNum >= 8)
            {
                    isTraped = false;
                    isItemed = true;
            }
            else
            {
                isTraped = false;
            }
        }

        else if (isItemed == true && isTraped == false)
        {
            createNum = Random.Range(0, 8);
            if (createNum <= 2)
            {
                isTraped = true;
            }
            itemCount++;
        }
        else if (isItemed == true && isTraped == true)
        {
            {
                createNum = Random.Range(3, 8);
                isTraped = false;
                itemCount++;
            }

        }

    }    
}
