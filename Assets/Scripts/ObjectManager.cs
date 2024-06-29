using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    int count = 0;
    float moveSpeed;
    int obNum;
    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
        obNum = int.Parse(gameObject.name);
        
    }
    public void Tap()
    {
        count++;
        if (count >= 3)
        {
            GameManager.instance.GetCollection(obNum);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance.isMoved || GameManager.instance.isMoved2)
        {
            MoveAction();

        }
        if (transform.position.x < -12)
        {
            Destroy(gameObject);
        }
    }


    void MoveAction()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }


}
