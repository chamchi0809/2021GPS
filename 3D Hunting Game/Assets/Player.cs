using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    CharacterController cc;

    public float gravity = -9.8f;
    float velocity;
    public float jumpPower = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();        
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir = new Vector3(dir.x, 0, dir.z).normalized;

        if(cc.collisionFlags == CollisionFlags.Below)
        {
            velocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                velocity = jumpPower;
        }
        velocity += gravity * Time.deltaTime;
        dir.y = velocity;
        cc.Move(dir * moveSpeed * Time.deltaTime);
        
    }
}
