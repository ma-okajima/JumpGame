using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI resultScore;
    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] TextMeshProUGUI getcollectionText;
    [SerializeField] GameObject getcollectionBox;
    [SerializeField] GameObject addtimecountText;
    [SerializeField] GameObject guradCountObject;
    [SerializeField] GameObject timeupPanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] List<GameObject> danceAnimations;
    [SerializeField] Button pauseButton;
    [SerializeField] Color disableColor;
    [SerializeField] GameObject starOb;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject highScoreTextImage;
    [SerializeField] Image oneJumpButtonImage;
    [SerializeField] Image twoJumpButtonImage;
    [SerializeField] Image pauseButtonImage;

    [SerializeField] Image pauseRetryImage;
    [SerializeField] Sprite defaultPauseRetrySprite;
    [SerializeField] Sprite pauseRetrySprite;
    [SerializeField] Image pauseBackImage;
    [SerializeField] Sprite defaultPauseBackSprite;
    [SerializeField] Sprite pauseBackSprite;
    [SerializeField] Image resultRetryImage;
    [SerializeField] Sprite defaultResultRetrySprite;
    [SerializeField] Sprite ResultRetrySprite;
    [SerializeField] Image resultRewardImage;
    [SerializeField] Sprite defaultResultRewardSprite;
    [SerializeField] Sprite ResultRewardSprite;
    [SerializeField] GameObject RewardButton;

    [SerializeField] List<Sprite> oneJumpSprites;
    [SerializeField] List<Sprite> twoJumpSprites;
    [SerializeField] List<Sprite> pauseSprites;
    Color buttonColor;

    [SerializeField]Transform spawnPoint;
    int scorePoint;
    int hiScore;
    int playCount;
    int danceNum;
    int clearDanceCount = 0;
    int spriteNum = 0;
    //ゲーム開始時の所持時間
    [SerializeField]float startTime;

    //アイテム取得時の追加時間
    [SerializeField] int addTimeCount;

    //ポーズ使用後の次回使用可能までの待機時間
    [SerializeField] int pauseStanbyTime;

    public bool isStarted = false;
    //ハイスコアの時リザルトのBGM変える
    public bool isHighScored = false;

    private void Start()
    {
        scorePoint = 0;
        spriteNum = 0;
        pointText.text = ("SCORE:" + scorePoint.ToString() + "m");
        timeText.text = ("Time:" + startTime.ToString() + "sec");
        hiScore = PlayerPrefs.GetInt("HISCORE", 0);
        hiScoreText.text = ("HISCORE:"+hiScore.ToString() + "m");
        buttonColor = pauseButton.GetComponent<Image>().color;
        Time.timeScale = 1;
        danceNum = Random.Range(0, 4);
        //遊んだ回数が規定を超えたらコレクションゲット。
        playCount = PlayerPrefs.GetInt("PLAYCOUNT", 0);
        if(playCount >= 82)
        {
            GameManager.instance.GetCollection(24);
        }

    }

    private void Update()
    {
        if (GameManager.instance.isFinished)
        {
            return;
        }
        if (isStarted)
        {
            TimeStart();
        }

        if(scorePoint > 100 &&scorePoint<=220)
        {
            GameManager.instance.stageTYPE = GameManager.STAGETYPE.STAGE_2;
            spriteNum = 1;
        }
        else if(scorePoint >220 && scorePoint <= 360)
        {
            GameManager.instance.stageTYPE = GameManager.STAGETYPE.STAGE_3;
            spriteNum = 2;
        }
        else if (scorePoint > 360 && scorePoint <= 520)
        {
            GameManager.instance.stageTYPE = GameManager.STAGETYPE.STAGE_4;
            spriteNum = 3;
        }
        else if (scorePoint > 520 )
        {
            GameManager.instance.stageTYPE = GameManager.STAGETYPE.STAGE_5;
            spriteNum = 4;
        }

            oneJumpButtonImage.sprite = oneJumpSprites[spriteNum];
            twoJumpButtonImage.sprite = twoJumpSprites[spriteNum];
            pauseButtonImage.sprite = pauseSprites[spriteNum];

        if (GameManager.instance.isRewarded)
        {
            RewardButton.SetActive(false);
        }
        else if (!GameManager.instance.isRewarded)
        {
            RewardButton.SetActive(true);
        }
        

    }
    
    public void AddPoint(int point)
    {
        scorePoint += point;
        if(scorePoint >= 9999)
        {
            scorePoint = 9999;
        }

        pointText.text = ("SCORE:" + scorePoint.ToString() + "m");
        //テスト
        int starNum = Random.Range(0, 2);
        //int starNum = Random.Range(0, 100);

        //1000m達成でコレクションゲット
        if (scorePoint >= 821)
        {
           GameManager.instance.GetCollection(20);
        }
        //1000m以降ランダムで星出現
        //if (scorePoint > 1000 && starNum == 1)
        //{
        //    GameObject star = Instantiate(starOb,spawnPoint);
        //    star.transform.SetParent(canvas.transform, false); Instantiate(starOb, transform.position, transform.rotation);
        //}

        //テスト
        if (scorePoint > 0 && starNum == 1)
        {
            GameObject star = Instantiate(starOb,spawnPoint);
            star.transform.SetParent(canvas.transform, false);
        }
        
    }

    void TimeStart()
    {
        startTime -= Time.deltaTime;
        if (startTime <= 0)
        {
            startTime = 0;
            GameManager.instance.isFinished = true;
            TimeUp();
        }
        timeText.text = ("Time:" + startTime.ToString("F1") + "sec");
        
    }
    void TimeUp()
    {
        GameManager.instance.OnTimeUpSE();
        if (scorePoint > hiScore)
        {
            hiScore = scorePoint;
            PlayerPrefs.SetInt("HISCORE", hiScore);
            hiScoreText.text = ("HISCORE:"+hiScore.ToString() + "m");
            PlayerPrefs.Save();
            highScoreTextImage.SetActive(true);
            isHighScored = true;
            
        }
        //遊んだ回数を記録
        playCount++;
        PlayerPrefs.SetInt(danceAnimations[danceNum].ToString(), 1);
        PlayerPrefs.SetInt("PLAYCOUNT", playCount);
        PlayerPrefs.Save();
        timeupPanel.SetActive(true);
        StartCoroutine(Result());

        
    }
    IEnumerator　Result()
    {
        resultScore.text = (scorePoint.ToString() + "m");
        danceAnimations[danceNum].SetActive(true);
       
        for (int i = 0; i < danceAnimations.Count; i++)
        {
            int clearNum = PlayerPrefs.GetInt(danceAnimations[i].ToString(), 0);
            if(clearNum == 0)
            {
                clearDanceCount++;
            }
        }
        
        yield return new WaitForSeconds(2);
        resultPanel.SetActive(true);
        if (clearDanceCount == 0)
        {
            GameManager.instance.GetCollection(22);
        }
        if (isHighScored)
        {
            StartCoroutine(GameManager.instance.OnHighScoreSE());
        }
        
    }
   
    public void AddTimeCount()
    {
        startTime += addTimeCount;
        if (startTime >= 99)
        {
            startTime = 99;
        }
        StartCoroutine(AddTimeCountAction());
    }

    IEnumerator AddTimeCountAction()
    {
        addtimecountText.SetActive(true);
        yield return new WaitForSeconds(2f);
        addtimecountText.SetActive(false);
    }
    

    public void PauseButton()
    {
        if (GameManager.instance.isFinished)
        {
            return;
        }
        GameManager.instance.OnSystemSE();
        pausePanel.SetActive(true);
        pauseButton.interactable = false;
        pauseButton.GetComponent<Image>().color = disableColor;
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        Time.timeScale = GameManager.instance.isPaused ? 0 : 1;
        
    }
    public void PauseBackButton()
    {
        GameManager.instance.OnSystemSE();
        StartCoroutine(PauseBack());
    }
    void EnablePauseButton()
    {
        pauseButton.interactable = true;
        pauseButton.GetComponent<Image>().color = buttonColor;
    }
    public void GetCollection(string getcollectionName)
    {
        getcollectionText.text = (getcollectionName + "GET");
        //仮で追加
        //getcollectionBox.SetActive(true);
        StartCoroutine(OnGetCollection());
    }
    IEnumerator OnGetCollection()
    {
        getcollectionBox.SetActive(true);
        yield return new WaitForSeconds(2f);
        getcollectionBox.SetActive(false);
    }
    public void RewardRestart()
    {
        isStarted = false;
        startTime += 10;
        resultPanel.SetActive(false);
        timeupPanel.SetActive(false);
        highScoreTextImage.SetActive(false);
        isHighScored = false;
        danceAnimations[danceNum].SetActive(false);
        timeText.text = ("Time:" + startTime.ToString() + "sec");

    }

    public void RestartScene()
    {
        StartCoroutine(PauseRetry());
    }
    public void ResultRestartButton()
    {
        StartCoroutine(ResultRestart());
    }
    public void ResultRewardButton()
    {
        StartCoroutine(ResultReward());
    }
    IEnumerator PauseBack()
    {
        pauseBackImage.sprite = pauseBackSprite;
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        Time.timeScale = GameManager.instance.isPaused ? 0 : 1;
        Invoke("EnablePauseButton", pauseStanbyTime);
        yield return new WaitForSeconds(0.1f);
        pauseBackImage.sprite = defaultPauseBackSprite;
        pausePanel.SetActive(false);
        
    }
    IEnumerator PauseRetry()
    {
        pauseRetryImage.sprite = pauseRetrySprite;
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        Time.timeScale = GameManager.instance.isPaused ? 0 : 1;
        yield return new WaitForSeconds(0.1f);
        pauseRetryImage.sprite = defaultPauseRetrySprite;
        GameManager.instance.RestartScene();

    }
    IEnumerator ResultRestart()
    {
        resultRetryImage.sprite = ResultRetrySprite;
        yield return new WaitForSeconds(0.1f);
        resultRetryImage.sprite = defaultResultRetrySprite;
        GameManager.instance.RestartScene();

    }
    IEnumerator ResultReward()
    {
        resultRewardImage.sprite = ResultRewardSprite;
        yield return new WaitForSeconds(0.1f);
        resultRewardImage.sprite = defaultResultRewardSprite;
        GameManager.instance.AdMobReward();
    }
}
