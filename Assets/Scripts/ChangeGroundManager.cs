using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGroundManager : MonoBehaviour
{
    string myTagName;
    
    void Start()
    {
        myTagName = this.gameObject.tag;
    }

    
    void Update()
    {
        if (myTagName == "Stage_5")
        {
            if (transform.position.x <= -600)
            {
                transform.position = new Vector2(300, -0.3f);
            }
        }
        else
        {
            if (transform.position.x <= -600)
            {
                Destroy(gameObject);
            }
        }
    }
}
