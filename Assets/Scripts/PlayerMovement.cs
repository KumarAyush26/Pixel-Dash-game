using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX =0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumforce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle,running,jumping,falling}

    [SerializeField] private AudioSource jumpSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (dirX*moveSpeed,rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity =new Vector2(rb.velocity.x,jumforce);
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState State;
        if (dirX > 0)
        {
            State = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            State = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.idle;
        }
        if(rb.velocity.y>.1f)
        {
            State = MovementState.jumping;
        }
        else if(rb.velocity.y<-.1f)
        {
            State = MovementState.falling;
        }
        anim.SetInteger("State",(int)State);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,jumpableGround);
    }
}
