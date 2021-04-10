using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public LayerMask layer;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.root.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }    
    }
    void Shoot()
    {
        Ray ray = new Ray(Camera.main.transform.position,
            Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, 100, layer))
        {
            if (hit.collider.GetComponent<Monster>() != null)
                hit.collider.GetComponent<Monster>().Die();
        }
        anim.SetTrigger("Shoot");
    }
}
