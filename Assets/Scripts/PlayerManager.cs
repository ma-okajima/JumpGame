using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    //デフォルト
    [SerializeField]float jumpPower = 10f;
    //OneJump時のJumpPower
    [SerializeField] float oneJumpPower;
    //TwoJump時のJumpPower
    [SerializeField] float twoJumpPower;
    //OneJump時のグラビティPower
    [SerializeField] float oneJumpGravity;
    //TwoJump時のグラビティPower
    [SerializeField] float twoJumpGravity;
    //Trap接触時のJumpPower
    [SerializeField] float hurtJumpPower;
    //TwoJump時の高さ上限値
    [SerializeField] float maxYPos;
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
        //TwoJump時の高さ上限
        if (GameManager.instance.isMoved2)
        {
            if (transform.position.y >= maxYPos)
            {
                transform.position = new Vector2(transform.position.x, maxYPos);
            }
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
            GameManager.instance.isTouched = false;
            animator.SetTrigger("Jump");
            //1マスジャンプ　jumpPowerとgravityScaleで調節
            jumpPower = oneJumpPower;
            rb.gravityScale = oneJumpGravity;
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
            GameManager.instance.isTouched = false;
            animator.SetTrigger("Jump2");
            //2マスジャンプ　jumpPowerとgravityScaleで調節
            jumpPower = twoJumpPower;
            rb.gravityScale = twoJumpGravity;
            Jump();
            uiManager.AddPoint(2);
        }
        
    }
    


    void Jump()
    {
        rb.velocity = Vector2.up * jumpPower;
        GameManager.instance.OnJumpSE();
    }

    //Trap接触時の待機時間
    IEnumerator WaitTimeAction()
    {
        yield return new WaitForSeconds(waitCount);
        isStoped = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.instance.isFinished)
        {
            return;
        }
        
        if (col.CompareTag("Item1"))
        {
            GameManager.instance.OnItemSE();
            uiManager.AddTimeCount();
            Destroy(col.gameObject);
            GameManager.instance.GetFirstItem();
        }
        else if (col.CompareTag("Trap")|| col.CompareTag("Trap2"))
        {
            string tagName = col.gameObject.tag;
            isStoped = true;
            Destroy(col.gameObject.GetComponent<Collider2D>());
            animator.SetTrigger("Hurt");
            GameManager.instance.OnHurtSE();
            //jumpPower = hurtJumpPower;
            //Jump();
            StartCoroutine(WaitTimeAction());
            GameManager.instance.CollisionTrap(tagName);
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
        //else if (col.CompareTag("BGM_1"))
        //{
        //    GameManager.instance.BGM_1();
        //}

    }
}
