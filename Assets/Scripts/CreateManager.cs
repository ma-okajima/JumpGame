using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CreateManager : MonoBehaviour
{
    [SerializeField] GameObject jumpPanel;
    [SerializeField] List<GameObject> jumpPrefabs = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_1 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_2 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_3 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_4 = new List<GameObject> { };
    [SerializeField] List<GameObject> createPrefabs_5 = new List<GameObject> { };
    Vector2 spawnPointJG = new Vector2(42, 0);
    Vector2 spawnPointTrap = new Vector2(15, -0.5f); 
    Vector2 spawnPointTrap2 = new Vector2(18,-0.5f);

    int createNum;
    int itemCount = 0;
    int jumpCount = 0;
    int stageNum = 0;
    public bool isTraped = false;
    bool isItemed = false;
    List<GameObject> createPrefabs;

    

    private void Start()
    {
        jumpCount = 2;


    }
    private void Update()
    {
     
        if (GameManager.instance.isJgCreared == true)
        {
            jumpCount++;
            if (jumpCount > 2)
            {
                stageNum++;
                jumpCount = 0;
            }
            JGSpawn();
            GameManager.instance.isJgCreared = false;
        }
        if (GameManager.instance.stageType == GameManager.STAGETYPE.STAGE1)
        {
            createPrefabs = createPrefabs_1;
        }else if(GameManager.instance.stageType == GameManager.STAGETYPE.STAGE2)
        {
            createPrefabs = createPrefabs_2;
        }
        else if (GameManager.instance.stageType == GameManager.STAGETYPE.STAGE3)
        {
            createPrefabs = createPrefabs_3;
        }
        else if (GameManager.instance.stageType == GameManager.STAGETYPE.STAGE4)
        {
            createPrefabs = createPrefabs_4;
        }
        else if (GameManager.instance.stageType == GameManager.STAGETYPE.STAGE5)
        {
            createPrefabs = createPrefabs_5;
        }
    }

    //createNum:[0~2 Trap] [8~9 Item]
    public void Panelspawn()
    {
        CreateObject(spawnPointTrap2);
     
    }
    public void Panelspawn2(UnityAction nextJump)
    {
        CreateObject(spawnPointTrap,nextJump);
      
    }
 
    public void JGSpawn()
    {
        GameObject newPanel = Instantiate(jumpPrefabs[stageNum], spawnPointJG, Quaternion.identity);
    }

    void CreateObject(Vector2 spawnPoint,UnityAction nextJump=null)
    {

        if (itemCount >= 22)
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
}
