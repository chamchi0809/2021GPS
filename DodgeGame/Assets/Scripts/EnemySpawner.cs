using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;
    public GameObject enemyGroup;
    bool isPlaying = true;

    public void OffSpawner()
    {
        isPlaying = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            int spawnPer = Random.Range(0, 1001);
            if (spawnPer < 50)
            {
                Enemy e = Instantiate(enemy);

                float posY = Random.Range(-5f, 5f);
                int isLeftInstantiate = Random.Range(0, 2);

                if (isLeftInstantiate == 0)
                {
                    e.transform.position = new Vector3(-9.7f, posY);
                    e.transform.localScale = new Vector2(5, 5);
                    e.SetDirectionVector(1);
                }
                else
                {
                    e.transform.position = new Vector3(9.7f, posY);
                    e.transform.localScale = new Vector2(-5, 5);
                    e.SetDirectionVector(-1);
                }
                e.transform.parent = enemyGroup.transform;
            }
        }
    }
}
