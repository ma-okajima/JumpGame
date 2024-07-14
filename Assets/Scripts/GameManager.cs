using System;
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
    [SerializeField] BGMController bgmController;
    [SerializeField] AdMobReward adMobReward;

    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip hurtSE;
    [SerializeField] AudioClip startSE;
    [SerializeField] AudioClip systemSE;
    [SerializeField] AudioClip itemSE;
    [SerializeField] AudioClip timeupSE;
    [SerializeField] AudioClip highscoreSE;
    [SerializeField] AudioClip collectionSE;
    
    

    AudioSource audioSource;

    //BG、パネル、オブジェクトをtrueの時動かす
    public bool isMoved = false;
    public bool isMoved2 = false;
    public bool isFinished = false;
    public bool isPaused = false;
    public bool isTouched ;
    public bool isBGMMuted =false;
    public bool isSoundMuted = false;


    //全体のスクロールスピード
    [SerializeField]float moveSpeed ;

    //各ステージでアイテムを取得した回数を保管
    int[] itemCounts = new int[5];
    // 各ステージでTrapに当たった回数を保管
    int[] trapCounts = new int[5];

    // 各ステージでTrap2に当たった回数を保管
    int[] trap2Counts = new int[5];

    //CheckTrapCount()の達成時に獲得できるコレクションNo.10〜14
    int trapCollectionNum;

    //現在のステージ　1〜5
    int stageNum;

    //今までコレクションした数
    int collectionCount;

    int i;

    public float MoveSpeed { get => moveSpeed; }


    public CollectionSO[] collections;

    public enum STAGETYPE
    {
        STAGE_1,
        STAGE_2,
        STAGE_3,
        STAGE_4,
        STAGE_5,
    }

    public STAGETYPE stageTYPE = STAGETYPE.STAGE_1;

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
        string bgm = PlayerPrefs.GetString("BGM", "OFF");
        string sound = PlayerPrefs.GetString("SOUND", "OFF");
        if(bgm == "OFF")
        {
            isBGMMuted = false;
        }else if(bgm == "ON")
        {
            isBGMMuted = true;
        }

        if(sound == "OFF")
        {
            isSoundMuted = false;
        }else if(sound == "ON")
        {
            isSoundMuted = true;
        }
        audioSource = GetComponent<AudioSource>();
        //テスト用　消す
        i = 1;
    }

    void Init()
    {
        isMoved = false;
        isMoved2 = false;
        isFinished = false;
        isPaused = false;
        isTouched = true;
        stageTYPE = STAGETYPE.STAGE_1;
        Array.Clear(itemCounts, 0, itemCounts.Length);
        Array.Clear(trapCounts, 0, trapCounts.Length);
        Array.Clear(trap2Counts, 0, trap2Counts.Length);
    }
    //リワード広告をみらたその状態からリスタート
    public void RewardRestart()
    {
        isMoved = false;
        isMoved2 = false;
        isFinished = false;
        isPaused = false;
        isTouched = true;
        uiManager.RewardRestart();
    }

    private void Update()
    {
        //UIManagerにて進んだマス数にてStageを管理
        switch (stageTYPE)
        {
            case STAGETYPE.STAGE_1:
                stageNum = 0;
                trapCollectionNum = 10;
                break;
            case STAGETYPE.STAGE_2:
                stageNum = 1;
                trapCollectionNum = 11;
                break;
            case STAGETYPE.STAGE_3:
                stageNum = 2;
                trapCollectionNum = 12;
                break;
            case STAGETYPE.STAGE_4:
                stageNum = 3;
                trapCollectionNum = 13;
                break;
            case STAGETYPE.STAGE_5:
                stageNum = 4;
                trapCollectionNum = 14;
                break;
        }

        if (isBGMMuted)
        {
            bgmController.BGMMuted();
        }
   
        //if (isSoundMuted)
        //{

        //}
        //else
        //{

        //}

        //テスト用　消す
        if (Input.GetKeyDown(KeyCode.O))
        {
             
            GetCollection(i);
            i ++;
            Debug.Log(collectionCount);
        }
        Debug.Log(stageNum);
        Debug.Log(itemCounts[stageNum]);
    }

    //初めてアイテムを取得したらコレクション
    public void GetFirstItem()
    {
        itemCounts[stageNum] += 1;
        if(stageTYPE == STAGETYPE.STAGE_1)
        {
            GetCollection(5);
        }
        else if(stageTYPE == STAGETYPE.STAGE_2)
        {
            GetCollection(6);
        }
        else if (stageTYPE == STAGETYPE.STAGE_3)
        {
            GetCollection(7);
        }
        else if (stageTYPE == STAGETYPE.STAGE_4)
        {
            GetCollection(8);
        }
        else if (stageTYPE == STAGETYPE.STAGE_5)
        {
            GetCollection(9);
        }
    }

    public void CollisionTrap(string tagName)
    {
        if (tagName == "Trap")
        {
            AddTrapCount();
        }else if(tagName == "Trap2")
        {
            AddTrap2Count();
        }
        CheckTrapCount();
    }

    public void AddTrapCount()
    {
        trapCounts[stageNum] += 1;
    }
    public void AddTrap2Count()
    {
        trap2Counts[stageNum] += 1;
    }
    //各ステージ規定回数障害物にあたったらコレクション獲得
    void CheckTrapCount()
    {
        if (trapCounts[stageNum] >= 1 && trap2Counts[stageNum] >= 1)
        {
            GetCollection(trapCollectionNum);
        }
        else if(stageTYPE ==STAGETYPE.STAGE_5&& trapCounts[stageNum] ==0 && trap2Counts[stageNum] == 0)
        {
            GetCollection(25);
        }
        //同じ障害物に3回あたる
        //if (trapCounts[stageNum] >= 3||trap2Counts[stageNum] >= 3)
        //{
        //    GetCollection(16);
        //}

    }

    public void RestartScene()
    {
        StartCoroutine(OnRestart());
    }
    public void TitleScene()
    {
        StartCoroutine(OnTitle());
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
        GetCollection(3);
    }
    public void Stage5()
    {
        stageManager.backgroundType = StageManager.BACKGROUNDTYPE.BG_5;
        GetCollection(4);
    }
    
   
    

    public void GetCollection(int getNum)
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
                    collectionCount = PlayerPrefs.GetInt("COLLECTIONCOUNT", 0);
                    collectionCount += 1;
                    PlayerPrefs.SetInt("COLLECTIONCOUNT", collectionCount);
                    PlayerPrefs.SetInt(collection.Name, 1);
                    PlayerPrefs.Save();
                    uiManager.GetCollection(collection.Name);
                    OnCollectionSE();
                    //全てのコレクション獲得で最後のコレクションを獲得
                    if(collectionCount >= 25)
                    {
                        StartCoroutine(AllCollection());
                    }
                }

            }

        }
       
        
    }
    IEnumerator AllCollection()
    {
        yield return new WaitForSeconds(3f);
        GetCollection(26);
    }

    public void OnJumpSE()
    {
        audioSource.PlayOneShot(jumpSE);
    }
    public void OnHurtSE()
    {
        audioSource.PlayOneShot(hurtSE);
    }
    public void OnStartSE()
    {
        audioSource.PlayOneShot(startSE);
    }
    public void OnSystemSE()
    {
        audioSource.PlayOneShot(systemSE);
    }
    public void OnItemSE()
    {
        audioSource.PlayOneShot(itemSE);
    }
    public void OnTimeUpSE()
    {
        audioSource.PlayOneShot(timeupSE);
    }
    //コレクションSEは他のSEとかぶってしまい現状聞こえない
    public void OnCollectionSE()
    {
        audioSource.PlayOneShot(collectionSE);
    }
    IEnumerator OnRestart()
    {
        OnStartSE();
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainScene");
    }
    IEnumerator OnTitle()
    {
        OnSystemSE();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("TitleScene");
    }
    public IEnumerator OnHighScoreSE()
    {
        //AudioClip bgm = audioSource.clip;
        audioSource.Stop();
        ///audioSource.clip = highscoreSE;
        audioSource.PlayOneShot(highscoreSE);
        yield return new WaitForSeconds(5f);
        audioSource.Play();
    }

    public void AdMobReward()
    {
        adMobReward.ShowAdMobReward();
    }

    public void BGM_1()
    {
        audioSource.volume = 0;
    }

}


