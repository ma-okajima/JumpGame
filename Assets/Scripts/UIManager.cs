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
    [SerializeField] GameObject hiscoreChara;
    [SerializeField] Button pauseButton;
    [SerializeField] Color disableColor;
    Color buttonColor;


    int scorePoint;
    int hiScore;
    int stagePoint;
    float timeCount;
    public bool isStarted = false;

    private void Start()
    {
        scorePoint = 0;
        timeCount = 10;
        pointText.text = ("SCORE:" + scorePoint.ToString() + "m");
        timeText.text = ("Time:" + timeCount.ToString() + "sec");
        hiScore = PlayerPrefs.GetInt("HISCORE", 0);
        hiScoreText.text = ("HISCORE:"+hiScore.ToString() + "m");
        buttonColor = pauseButton.GetComponent<Image>().color;
        Time.timeScale = 1;

    }

    private void Update()
    {
        if (isStarted)
        {
            TimeStart();
        }
        

    }
    public void AddPoint(int point)
    {
        scorePoint += point;
        pointText.text = ("SCORE:" + scorePoint.ToString() + "m");
        if(scorePoint >= 12 && scorePoint<33)
        {
            GameManager.instance.stageType = GameManager.STAGETYPE.STAGE2;
        }else if(scorePoint >= 33 &&scorePoint<54)
        {
            GameManager.instance.stageType = GameManager.STAGETYPE.STAGE3;
        }
        else if (scorePoint >= 54 && scorePoint < 75)
        {
            GameManager.instance.stageType = GameManager.STAGETYPE.STAGE4;
        }
        else if (scorePoint >= 75 && scorePoint < 96)
        {
            GameManager.instance.stageType = GameManager.STAGETYPE.STAGE5;
        }

        stagePoint += point;

    }

    void TimeStart()
    {
        timeCount -= Time.deltaTime;
        if (timeCount <= 0)
        {
            timeCount = 0;
            GameManager.instance.isFinished = true;
            TimeUp();
        }
        timeText.text = ("Time:" + timeCount.ToString("F1") + "sec");
        
    }
    void TimeUp()
    {
        if (scorePoint > hiScore)
        {
            hiScore = scorePoint;
            PlayerPrefs.SetInt("HISCORE", hiScore);
            hiScoreText.text = ("HISCORE:"+hiScore.ToString() + "m");
            PlayerPrefs.Save();
            hiscoreChara.SetActive(true);
            
        }
        timeupPanel.SetActive(true);
        StartCoroutine(Result());
    }
    IEnumerator　Result()
    {
        resultScore.text = (scorePoint.ToString() + "m");
       
        yield return new WaitForSeconds(2);
        resultPanel.SetActive(true);
    }

    public void RestartButton()
    {
        GameManager.instance.RestartScene();
    }
    public void AddTimeCount()
    {
        timeCount += 5f;
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
        pausePanel.SetActive(true);
        pauseButton.interactable = false;
        pauseButton.GetComponent<Image>().color = disableColor;
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        Time.timeScale = GameManager.instance.isPaused ? 0 : 1;
        
    }
    public void PauseBackButton()
    {
        pausePanel.SetActive(false);
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        Time.timeScale = GameManager.instance.isPaused ? 0 : 1;
        Invoke("EnablePauseButton", 5f);
        
    }
    void EnablePauseButton()
    {
        pauseButton.interactable = true;
        pauseButton.GetComponent<Image>().color = buttonColor;
    }
    public void GetCollection(string getcollectionName)
    {
        getcollectionText.text = (getcollectionName + "GET!!");
        //仮で追加
        getcollectionBox.SetActive(true);
        //StartCoroutine(OnGetCollection());
    }
    //IEnumerator OnGetCollection()
    //{
    //    getcollectionBox.SetActive(true);
    //    yield return new WaitForSeconds(2f);
    //    getcollectionBox.SetActive(false);
    //}
}
