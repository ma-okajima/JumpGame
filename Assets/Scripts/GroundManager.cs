using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    //GameManagerにて一括管理
    float moveSpeed;
    string myTagName;

    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
        myTagName = this.gameObject.tag;
        
        
    }
    private void Update()
    {
        
        if(myTagName == "Stage_5")
        {
            if (transform.position.x <= -600)
            {
                transform.position = new Vector2(300, 0);
            }
        }
        else
        {
            if(transform.position.x <= -600)
            {
                Destroy(gameObject);
            }
        }


        //ジャンプ中実行される処理
        if (GameManager.instance.isMoved || GameManager.instance.isMoved2)
        {
            MoveAction();

        }
        
    }
    //JumpGroundの流れていくスピード管理
    void MoveAction()
    {

        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }
}




