using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    //GameManagerにて一括管理
    float moveSpeed;
    

    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
        
        //moveSpeed = 15f;
        
    }
    private void Update()
    {
        
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




