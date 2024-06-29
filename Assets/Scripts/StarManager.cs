using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
   
    float moveSpeed =5;
    
    private void Start()
    {
        
        
    }
    public void Tap()
    { 
            GameManager.instance.GetCollection(23);
            Destroy(gameObject);      
    }

    private void Update()
    {
        MoveAction();

        if (transform.position.x < -12 || GameManager.instance.isFinished==true)
        {
            Destroy(gameObject);
        }
    }


    void MoveAction()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }
}
