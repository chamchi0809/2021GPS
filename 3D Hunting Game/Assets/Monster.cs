using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    int hp = 100;

    public SkinnedMeshRenderer mr;
    Material mt;
    bool dead;
    // Start is called before the first frame update
    void Start()
    {        
        mt = mr.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        if (!dead)
            StartCoroutine(DieRutine());
    }
    IEnumerator DieRutine()
    {
        dead = true;
        float dieTimer = .5f;
        while(dieTimer > 0)
        {
            mt.color -= new Color(0, 0, 0, 2*Time.deltaTime);
            dieTimer -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
