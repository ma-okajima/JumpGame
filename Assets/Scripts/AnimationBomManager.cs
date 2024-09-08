using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBomManager : MonoBehaviour
{
    [SerializeField] Sprite newSprite;

    Animator animator;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();
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
                Destroy(animator);
                StartCoroutine(OnBom());
            }
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator OnBom()
    {
        spriteRenderer.sprite = newSprite;
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
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