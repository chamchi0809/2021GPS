using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    Text hpText;
    int playerHP = 3;
    private Player player;    
    // Start is called before the first frame update
    void Start()
    {
        hpText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
        playerHP = player.GetHP();
    }

    // Update is called once per frame
    void Update()
    {        
        if(player.GetHP() <= 0)
            hpText.text = "HP : " + 0;
        else
            hpText.text = "HP : " + player.GetHP();
    }
}
