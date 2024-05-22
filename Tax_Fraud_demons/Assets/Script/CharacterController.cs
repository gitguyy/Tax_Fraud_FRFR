using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    protected Vector2 velocity;
    [SerializeField]
    protected float maxSpeed,damping;
    protected float speed;
    
    //private utilty utils;
    protected float direction;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //utils = FindAnyObjectByType<utilty>().GetComponent<utilty>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    

 
    public void Move(Vector2 _velocity)
    {
        rb.velocity = _velocity;
    }

    public void Move()
    {
        rb.velocity = new Vector2(direction * maxSpeed, rb.velocity.y);
        UpdateAnimator();
    }
    
    public void dampenedMove()
    {
        

        if (direction != 0)
        {
            
            // Lerping speed from 0 to maxSpeed
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * damping);
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            
           
        }
       

        if (direction == 0)
        {
            
            speed = Mathf.Lerp(rb.velocity.x, 0, Time.deltaTime * damping);
            rb.velocity = new Vector2( speed, rb.velocity.y);
        }
        
    }
    private void UpdateAnimator()
    {
        bool isWalking = rb.velocity.x != 0;
        animator.SetBool("Walking", isWalking);
    }
}
