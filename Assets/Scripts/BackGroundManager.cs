using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackGroundManager : MonoBehaviour
{

    public float effectSpeed;
       
    float moveSpeed;
    void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
    }
    private void FixedUpdate()
    {
        
        if (transform.position.x<=-19.2f)
        {
           
            transform.position = new Vector2(0, 0);
        }
        

        if (GameManager.instance.isMoved || GameManager.instance.isMoved2)
        {
            MoveAction();
            
        }
    }


    void MoveAction()
    {
        transform.Translate(Vector2.left * moveSpeed *effectSpeed* Time.deltaTime);

    }
}
