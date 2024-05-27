using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public float jumpPower = 10f;

    //Trap接触時の待機時間管理の為のbool値
    bool isStoped =false;

    //Trap接触時の待機時間
    [SerializeField] float waitCount;

    [SerializeField] UIManager uiManager;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        //キーボード入力
        if(Input.GetKeyDown(KeyCode.F))
        {
            OneJumpAction();
        }else if (Input.GetKeyDown(KeyCode.J))
        {
            TwoJumpAction();
        }
    }


    public void OneJumpAction()
    {
        
        //TimeUpで操作無効
        if (GameManager.instance.isFinished == true)
        {
            return;
        }
        //カウントダウン開始
        uiManager.isStarted = true;
        if (GameManager.instance.isTouched && !GameManager.instance.isMoved &&!isStoped)
        {
            GameManager.instance.isMoved = true;
            animator.SetTrigger("Jump");
            //1マスジャンプ　jumpPowerとgravityScaleで調節
            jumpPower = 12f;
            rb.gravityScale = 12f;
            Jump();
            uiManager.AddPoint(1);

        }
       
        
       
    }
    public void TwoJumpAction()
    {
        //TimeUpで操作無効
        if (GameManager.instance.isFinished == true)
        {
            return;
        }
        //カウントダウン開始　
        uiManager.isStarted = true;
        
        if (GameManager.instance.isTouched&&!GameManager.instance.isMoved2&&!isStoped)
        {
            GameManager.instance.isMoved2 = true;
            animator.SetTrigger("Jump2");
            //2マスジャンプ　jumpPowerとgravityScaleで調節
            jumpPower = 22f;
            rb.gravityScale = 11f;
            Jump();
            uiManager.AddPoint(2);
        }
        
    }
    void Jump()
    {
        rb.velocity = Vector2.up * jumpPower;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.instance.isFinished)
        {
            return;
        }
        
        if (col.CompareTag("Item1"))
        {
            uiManager.AddTimeCount();
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("Trap"))
        {
            isStoped = true;
            Destroy(col.gameObject.GetComponent<Collider2D>());
            animator.SetTrigger("Hurt");
            jumpPower = 5;
            Jump();
            StartCoroutine(WaitTimeAction());
        }
        else if (col.CompareTag("Stage_2"))
        {
            GameManager.instance.Stage2();      
        }
        else if (col.CompareTag("Stage_3"))
        {
            GameManager.instance.Stage3();
        }
        else if (col.CompareTag("Stage_4"))
        {
            GameManager.instance.Stage4();
        }
        else if (col.CompareTag("Stage_5"))
        {
            GameManager.instance.Stage5();
        }

    }
    //ジャンプ中キー入力(ボタン入力不可）
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("JumpPanel"))
        {
            GameManager.instance.isTouched = false;
        }
        
    }
    //Trap接触時の待機時間
    IEnumerator WaitTimeAction()
    {
        yield return new WaitForSeconds(waitCount);
        isStoped = false;
    }

    

    
}
