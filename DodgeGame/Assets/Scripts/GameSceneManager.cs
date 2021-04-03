using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    Player player;
    EnemySpawner enemySpawner;
    UI_Time timer;

    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        timer = FindObjectOfType<UI_Time>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetHP() <= 0)
        {
            gameOverUI.SetActive(true);
            enemySpawner.OffSpawner();
            timer.OffTimer();
        } 
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
