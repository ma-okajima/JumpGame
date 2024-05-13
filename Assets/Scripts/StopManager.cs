using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopManager : MonoBehaviour
{

    [SerializeField] CreateManager createManager;

    int count = 0;
   
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (GameManager.instance.isMoved == true)
        {
            GameManager.instance.isMoved = false;
            //GameManager.instance.CreateOnJumpPanel();
            createManager.Panelspawn();
        }
        else if (GameManager.instance.isMoved2 == true)
        {
            count++;
            if (count >= 2)
            {
                GameManager.instance.isMoved2 = false;
                //GameManager.instance.CreateOnJumpPanel_2();
                createManager.Panelspawn2();
                count = 0;
                createManager.Panelspawn();
            }
        }
    }
}
