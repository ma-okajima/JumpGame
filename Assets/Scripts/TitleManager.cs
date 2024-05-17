using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    
    [SerializeField] GameObject optionPanel;

    
   

    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnCollectionButton()
    {
        SceneManager.LoadScene("CollectionScene");
    }
    public void OnOptionButton()
    {
        optionPanel.SetActive(true);
    }
    public void OptionBackButton()
    {
        optionPanel.SetActive(false);
    }

    public void ResetButton()
    {
        //PlayerPrefs.DeleteKey("HISCORE");
        PlayerPrefs.DeleteAll();
    }
    
}
