using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JgManager : MonoBehaviour
{
    float moveSpeed;


    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
        
    }
    private void Update()
    {
        if (transform.position.x <= -21)
        {
            Destroy(this.gameObject);
            GameManager.instance.isJgCreared = true;
        }

        if (GameManager.instance.isMoved || GameManager.instance.isMoved2)
        {
            MoveAction();

        }
        

         void MoveAction()
        {

            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
   
        }
        
    }
}




