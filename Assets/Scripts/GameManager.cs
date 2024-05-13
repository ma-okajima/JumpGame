using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] StageManager stageManager;
    [SerializeField] CreateManager createManager;
    public bool isJgCreared = false;
    //BG、パネル、オブジェクトをtrueの時動かす
    public bool isMoved = false;
    public bool isMoved2 = false;
    public bool isFinished = false;
    public bool isPaused = false;
    float moveSpeed = 6f;
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
    public void Stage2()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_2;
        Collection_1();
    }
    public void Stage3()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_3;
    }
    public void Stage4()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_4;
    }
    public void Stage5()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_5;
    }

    //public void CreateOnJumpPanel()
    //{
    //    createManager.Panelspawn();
    //}
    //public void CreateOnJumpPanel_2()
    //{
    //    createManager.Panelspawn();
    //    createManager.Panelspawn2();
    //}

    void Collection_1()
    {
        PlayerPrefs.SetInt("Trophy", 1);
        PlayerPrefs.Save();
    }

    
}

