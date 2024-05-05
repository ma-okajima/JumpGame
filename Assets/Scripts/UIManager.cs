using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI resultScore;
    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] GameObject addtimecountText;
    [SerializeField] GameObject guradCountObject;
    [SerializeField] GameObject timeupPanel;
    [SerializeField] GameObject resultPanel;
    
    int scorePoint;
    int hiScore;
    float timeCount;
    public bool isStarted = false;
    private void Start()
    {
        scorePoint = 0;
        timeCount = 100;
        pointText.text = ("SCORE:" + scorePoint.ToString() + "m");
        timeText.text = ("Time:" + timeCount.ToString() + "sec");
        hiScore = PlayerPrefs.GetInt("HISCORE", 0);
        hiScoreText.text = ("HISCORE:"+hiScore.ToString() + "m");
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
    public void GuradOn()
    {
        guradCountObject.SetActive(true);
    }
    public void GuradOff()
    {
        guradCountObject.SetActive(false);
    }
}
