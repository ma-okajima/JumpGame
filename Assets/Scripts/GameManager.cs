using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] StageManager stageManager;
    [SerializeField] CreateManager createManager;
    [SerializeField] UIManager uiManager;
    public bool isJgCreared = false;
    //BG、パネル、オブジェクトをtrueの時動かす
    public bool isMoved = false;
    public bool isMoved2 = false;
    public bool isFinished = false;
    public bool isPaused = false;
    public bool isTouched ;
    float moveSpeed = 15f;
    int stageNum = 1;

    public float MoveSpeed { get => moveSpeed; }

    public enum STAGETYPE
    {
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
        STAGE5,
    }

    public STAGETYPE stageType = STAGETYPE.STAGE1;


    public CollectionSO[] collections;


    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isPaused = false;
        isTouched = true;
        
    }

    private void Update()
    {

        switch (stageType)
        {
            case STAGETYPE.STAGE1:
                stageNum = 1;
                break;
            case STAGETYPE.STAGE2:
                stageNum = 2;
                break;
            case STAGETYPE.STAGE3:
                stageNum = 3;
                break;
            case STAGETYPE.STAGE4:
                stageNum = 4;
                break;
            case STAGETYPE.STAGE5:
                stageNum = 5;
                break;

        }
    }

    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void CollectionScene()
    {
        SceneManager.LoadScene("CollectionScene");
    }

    public void Stage2()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_2;
        GetCollection(1);
    }
    public void Stage3()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_3;
        GetCollection(2);
    }
    public void Stage4()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_4;
    }
    public void Stage5()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_5;
    }

    

    void GetCollection(int getNum)
    {
        foreach(var collection in collections)
        {
            if (collection.Num == getNum)
            {
                int keyNum = PlayerPrefs.GetInt(collection.Name, 0);
                if(keyNum == 1)
                {
                    return;
                }
                else if(keyNum == 0)
                {
                    PlayerPrefs.SetInt(collection.Name, 1);
                    PlayerPrefs.Save();
                    uiManager.GetCollection(collection.Name);
                }

            }
        }
       
        
    }

    
}

