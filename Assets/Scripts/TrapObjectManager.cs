using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObjectManager : MonoBehaviour
{
    [SerializeField] Sprite newSprite;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        string tagName = col.gameObject.tag;
        Debug.Log(tagName);
        //各ステージのJumpPanelに対応したアイテム、障害物以外が生成された場合は削除
        //現状ステージの切り替え時、非対応オブジェクトが生成される可能性がある為
        if (tagName + "Trap(Clone)" == this.gameObject.name || tagName + "Trap2(Clone)" == this.gameObject.name)
        {
            transform.position = new Vector2(col.gameObject.transform.position.x, transform.position.y);
            transform.SetParent(col.transform);
        }
        else
        if (tagName == "Player")
        {
            if (newSprite != null)
            {
                
                spriteRenderer.sprite = newSprite;
            }
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