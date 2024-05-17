using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JgManager : MonoBehaviour
{
    //GameManagerにて一括管理
    float moveSpeed;


    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
        
    }
    private void Update()
    {
        //画面外にでたら削除、boolにて新たにJumpGroundを生成
        if (transform.position.x <= -21)
        {
            Destroy(this.gameObject);
            GameManager.instance.isJgCreared = true;
        }
        //ジャンプ中実行される処理
        if (GameManager.instance.isMoved || GameManager.instance.isMoved2)
        {
            MoveAction();

        }
        
        //JumpGroundの流れていくスピード管理
         void MoveAction()
        {

            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
   
        }
        
    }
}




