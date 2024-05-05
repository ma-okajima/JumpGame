using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopManager : MonoBehaviour
{
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="JumpPanel")
        {
            if (GameManager.instance.isMoved == true)
            {
                GameManager.instance.isMoved = false;
            }else if (GameManager.instance.isMoved2 == true)
            {
                count++;
                if(count >= 2)
                {
                    GameManager.instance.isMoved2 = false;
                    count = 0;
                }
            }
            
        }
    }
}
