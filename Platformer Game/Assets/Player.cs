using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    public float drag;
    public float turnOffset;

    public Transform groundChecker;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    bool isGround = true;


    Rigidbody2D rb;
    float moveInput;

    Animator anim;
    bool attacking;
    public float attackDuration;    
    public float attackOffset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isGround = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayer);
        Attack();
        Jump();
    }
    void FixedUpdate()
    {
        Move();
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, drag), rb.velocity.y);
    }
    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGround)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        } 
    }
    void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal");        
        if (moveInput != 0)
        {            
            rb.AddForce(moveInput * moveSpeed * Vector2.right);
            if (moveInput < 0 && transform.localScale.x > 0)
            {
                transform.position += (Vector3)(Vector2.right * turnOffset);
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            if (moveInput > 0 && transform.localScale.x < 0)
            {
                transform.position -= (Vector3)(Vector2.right * turnOffset);
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }
        anim.SetBool("Moving", moveInput != 0);
    }
    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(groundChecker.position, groundCheckRadius);
    //    Gizmos.DrawSphere(transform.position + (Vector3)(Mathf.Sign(transform.localScale.x) * Vector2.right * attackOffset), .5f);
    //}
    void Attack()
    {
        if(Input.GetMouseButton(0))
            StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine()
    {
        if (!attacking)
        {
            float attackTimer = attackDuration;
            attacking = true;            
            anim.SetBool("Attacking", attacking);
            SoundManager.instance.AttackSound();
            while (attackTimer > 0)
            {
                Collider2D col = Physics2D.OverlapCircle(transform.position + (Vector3)(Mathf.Sign(transform.localScale.x) * Vector2.right * attackOffset), .5f, (1 << LayerMask.NameToLayer("Enemy"))); //비트쉬프트 연산자

                if (col != null)
                {
                    col.GetComponent<Enemy>().Die();
                }

                attackTimer -= Time.deltaTime;
                yield return null;
            }            
            attacking = false;            
            anim.SetBool("Attacking", attacking);
        }
    }
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground"))
    //    {
    //        isGround = true;
    //    }
    //}
    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground"))
    //    {
    //        isGround = true;
    //    }
    //}
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground"))
    //    {
    //        isGround = false;
    //    }
    //}
}
