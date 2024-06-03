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
    
    //BG、パネル、オブジェクトをtrueの時動かす
    public bool isMoved = false;
    public bool isMoved2 = false;
    public bool isFinished = false;
    public bool isPaused = false;
    public bool isTouched ;
   

    //全体のスクロールスピード
    [SerializeField]float moveSpeed ;
    

    public float MoveSpeed { get => moveSpeed; }


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
        Init();
        
    }

  
    public void RestartScene()
    {
        Init();
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
    
    void Init()
    {
        isMoved = false;
        isMoved2 = false;
        isFinished = false;
        isPaused = false;
        isTouched = true;
       
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

