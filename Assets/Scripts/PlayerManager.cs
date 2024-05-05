using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public float jumpPower = 10f;
    int guradCount;
    bool isTouched = false;
    bool isStoped =false;
    [SerializeField] UIManager uiManager;
    [SerializeField] CreateManager createManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        guradCount = 0;
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
        if (isTouched && !GameManager.instance.isMoved &&!isStoped)
        {
            GameManager.instance.isMoved = true;
            createManager.Panelspawn();
            animator.SetTrigger("Jump");
            //1マジャンプ　jumpPowerとgravityScaleで調節
            jumpPower = 7.3f;
            rb.gravityScale = 3f;
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
        
        if (isTouched&&!GameManager.instance.isMoved2&&!isStoped)
        {
            GameManager.instance.isMoved2 = true;
            createManager.Panelspawn();
            createManager.Panelspawn2();
            animator.SetTrigger("Jump2");
            //2回マスジャンプ　jumpPowerとgravityScaleで調節
            jumpPower = 13f;
            rb.gravityScale = 2.7f;
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
        if (col.CompareTag("JumpPanel"))
        {
            isTouched = true;
           
        }
        else if (col.CompareTag("Item1"))
        {
            uiManager.AddTimeCount();
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("Item2"))
        {
            guradCount++;
            uiManager.GuradOn();
            if(guradCount >= 1)
            {
                guradCount = 1;
            }
            
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("Trap") && guradCount == 0)
        {
            isStoped = true;
            Destroy(col.gameObject.GetComponent<Collider2D>());
            animator.SetTrigger("Hurt");
            jumpPower = 5;
            Jump();
            StartCoroutine(WaitTimeAction());
        }
        else if (col.CompareTag("Trap") && guradCount != 0)
        {
            Destroy(col.gameObject.GetComponent<Collider2D>());
            guradCount = 0;
            uiManager.GuradOff();
        }




    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("JumpPanel"))
        {
            isTouched = false;
        }
        
    }

    IEnumerator WaitTimeAction()
    {
        yield return new WaitForSeconds(2f);
        isStoped = false;
    }
    
}
