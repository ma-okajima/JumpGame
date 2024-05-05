using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject tapText;
    float count = 0;
    bool isActive = true;
    private void Update()
    {
        TapFlash();
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    void TapFlash()
    {
        count += Time.deltaTime;
        if(count > 0.7f)
        {
            isActive = !isActive;
            tapText.SetActive(isActive);
            count = 0;
        }
       
    }
}
