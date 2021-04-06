using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spr;
    bool dead;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {            
            spr.color = Color.Lerp(spr.color, new Color(1, 1, 1, 0), .05f);
        }

    }
    public void Die()
    {
        if (!dead)
        {
            SoundManager.instance.HitSound();
            anim.SetTrigger("Die");
            Destroy(gameObject, 2);
            dead = true;
        }
    }
}
