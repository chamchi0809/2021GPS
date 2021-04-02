using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public float moveForce;
    public float drag;

    int hp = 3;

    public int GetHP()
    {
        return hp;
    }

    public void SetHP(int hp)
    {
        this.hp = hp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            return;
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; 
    }
    void FixedUpdate()
    {
        if (hp > 0)
            rb.AddForce(moveInput * moveForce);
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, drag);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {            
            print(--hp);
        }
    }
}
