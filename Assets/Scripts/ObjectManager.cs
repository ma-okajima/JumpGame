using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
   //GameManagerにて一括管理
    float moveSpeed;
    

    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {

        //各ステージのJumpPanelに対応したアイテム、障害物以外が生成された場合は削除
        //現状ステージの切り替え時、非対応オブジェクトが生成される可能性がある為
        if (col.gameObject.tag + "Trap(Clone)" == this.gameObject.name || col.gameObject.tag + "Item(Clone)" == this.gameObject.name)
        {
            transform.position = new Vector2(col.gameObject.transform.position.x, transform.position.y);
            transform.SetParent(col.transform);
        }
        else
        if (col.gameObject.tag == "Player")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    

    private void FixedUpdate()
    {
        //画面外に出たら削除
        if (transform.position.x <= -12)
        {
            Destroy(this.gameObject);

        }



    }
    
}
