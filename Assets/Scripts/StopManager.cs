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
            StartCoroutine(OneJumpAction());
        }
        else if (GameManager.instance.isMoved2 == true)
        {
            count++;
            if (count >= 2)
            {
                GameManager.instance.isMoved2 = false;
                count = 0;
                createManager.Panelspawn2(NextJump);
            }
        }
    }
    IEnumerator OneJumpAction()
    {
        createManager.Panelspawn();
        yield return new WaitUntil(() => GameManager.instance.isTouched = true);
    }
    void NextJump()
    {
        StartCoroutine(OneJumpAction());
    }
}

