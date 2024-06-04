using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    int count = 0;
    float moveSpeed;

    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
    }
    public void Tap()
    {
        count++;
        if (count >= 3)
        {
            GameManager.instance.GetCollection(20);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance.isMoved || GameManager.instance.isMoved2)
        {
            MoveAction();

        }
    }


    void MoveAction()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }


}
