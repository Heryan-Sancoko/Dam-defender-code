using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public float GameRadius = 10;

    //    PlayerHealth playerHealth;

    public VoidEvent onGameOver;

    void Start () {
      //  playerHealth = GetComponentInChildren<PlayerHealth>();
    }
	

public    void GameOver()
    {
        onGameOver.Invoke();
    }
    

public    void Restart()
    {
        Debug.Log("restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//        gameOverUI.SetActive(false);
  //      enemyManager.EnemiesDestroy();
    //    playerHealth.SetHealth(100);
     //   cycleManager.Restart();
    }
}
