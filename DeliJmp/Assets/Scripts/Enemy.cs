using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Enemy - instance
    /// Int - count + points
    /// </summary>
    public static Action<Enemy, int> Die;
    [SerializeField] int price;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "DeadZone")
        {
            Die?.Invoke(this, 0);
            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Die?.Invoke(this, price);
            Destroy(gameObject);
        }
    }
}
