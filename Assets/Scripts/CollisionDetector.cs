using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] TriggerEvent onTriggerEnter2D = new TriggerEvent();

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter2D.Invoke(other);
    }


    [Serializable]
    public class TriggerEvent : UnityEvent<Collider2D>
    {
    }
}
