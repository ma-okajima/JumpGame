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
    Color buttonColor;

    [SerializeField]Transform spawnPoint;
    int scorePoint;
    int hiScore;
    int playCount;
    int danceNum;
    int clearDanceCount = 0;
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
        }
        else if(scorePoint >220 && scorePoint <= 360)
        {
            GameManager.instance.stageTYPE = GameManager.STAGETYPE.STAGE_3;
        }
        else if (scorePoint > 360 && scorePoint <= 520)
        {
            GameManager.instance.stageTYPE = GameManager.STAGETYPE.STAGE_4;
        }
        else if (scorePoint > 520 )
        {
            GameManager.instance.stageTYPE = GameManager.STAGETYPE.STAGE_5;
        }

    }
    
    public void AddPoint(int point)
    {
        scorePoint += point;
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
        pausePanel.SetActive(false);
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        Time.timeScale = GameManager.instance.isPaused ? 0 : 1;
        Invoke("EnablePauseButton", pauseStanbyTime);
        
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
}
