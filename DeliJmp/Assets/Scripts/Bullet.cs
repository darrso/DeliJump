using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Enemy target;
    Rigidbody2D rigidbody;

    float speed;
    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
        speed = 10;
        GetNearEnemy();
    }
    void GetNearEnemy()
    {
        target = GameManager.instance.findMinDistPlayer();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            float x = Random.Range(0, 1);
            rigidbody.velocity = new Vector2(x, 1) * speed;
        }
        else
        {
            Vector2 toTarget = (target.transform.position - transform.position);
            toTarget.Normalize();
            Debug.Log(toTarget);
            rigidbody.velocity = toTarget * 10f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.name == "BulletUpDeadZone" || collision.name == "Left" || collision.name == "Right" || collision.name == "DeadZone" || collision.tag == "Enemy" || collision.tag == "Line")
        {
            Destroy(gameObject);
        }
    }
}
