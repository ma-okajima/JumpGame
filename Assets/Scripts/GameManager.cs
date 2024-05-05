using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isJgCreared = false;
    //BG、パネル、オブジェクトをtrueの時動かす
    public bool isMoved = false;
    public bool isMoved2 = false;
    public bool isFinished = false;
    float moveSpeed = 6f;

    public float MoveSpeed { get => moveSpeed; }

    private void Update()
    {
       
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
   public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
   
}

