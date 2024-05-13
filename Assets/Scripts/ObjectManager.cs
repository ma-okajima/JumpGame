using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
   
    float moveSpeed;
    

    private void Start()
    {
        moveSpeed = GameManager.instance.MoveSpeed;
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        

        if (col.gameObject.tag + "Trap(Clone)" == this.gameObject.name || col.gameObject.tag + "Item(Clone)" == this.gameObject.name)
        {
            transform.position = new Vector2(col.gameObject.transform.position.x, transform.position.y);
            transform.SetParent(col.transform);
        }else if (col.gameObject.tag == "Player")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
       
        
    }

    

    private void FixedUpdate()
    {
        if (transform.position.x <= -12)
        {
            Destroy(this.gameObject);

        }

       
        
    }

}
