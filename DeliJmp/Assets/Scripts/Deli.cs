using System;
using UnityEngine;

public class Deli : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody;
    public static Deli instance;
    public GameObject BulletPrefab;

    public SpriteRenderer spriteRenderer;

    public float maxMagnitude;

    private bool dead;
    public static Action DeadAction;

    public AudioSource jump;
    
    void Start()
    {
        dead = false;
        if(instance == null)
        {
            instance = this;
        }
    }
    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            DieMove();
        }
        if(rigidbody.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(rigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;    
        }
        if (rigidbody.velocity.y > 0)
        {
            animator.SetBool("JumpingDown", false);
        }
        else if(rigidbody.velocity.y < 0)
        {
            animator.SetBool("JumpingDown", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "DeadZone")
        {
            Die();
        }
        if (collision.name == "Left")
        {
            transform.position = new Vector3(2f, transform.position.y, transform.position.z);
        }
        if (collision.name == "Right")
        { 
            transform.position = new Vector3(-2f, transform.position.y, transform.position.z);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            dead = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Line")
        {
            jump.Play();
        }
    }
    private void Die()
    {
        DeadAction?.Invoke();
    }
    private void DieMove()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(0, -5), ForceMode2D.Impulse);
        animator.SetTrigger("Fall");
    }
    public void Pew()
    {
        //pew.Play();
        Instantiate(BulletPrefab, transform.position, Quaternion.identity);
    }
}
