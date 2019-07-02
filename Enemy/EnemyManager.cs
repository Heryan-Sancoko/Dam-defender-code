using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    public GameObject bearPrefab;
    public GameObject cobraPrefab;
    public GameObject lynxPrefab;

    public List<EnemyWaveInfo> enemyWaveInfo;

    List<GameObject> enemies;
    

    void Start () {
        enemies = new List<GameObject>();
	}

    public void EnemiesEnterStage(int waveCount)
    {
        if (gameObject.activeInHierarchy == false)
            return;
        waveCount = waveCount < enemyWaveInfo.Count - 1 ? waveCount : enemyWaveInfo.Count - 1;
        var waveInfo = enemyWaveInfo[waveCount];
        for (int i = 0; i < waveInfo.bearCount; i++)
        {
            enemies.Add(GameObject.Instantiate(bearPrefab,transform));
        }
        for (int i = 0; i < waveInfo.cobraCount; i++)
        {
            enemies.Add(GameObject.Instantiate(cobraPrefab, transform));
        }
        for (int i = 0; i < waveInfo.lynxCount; i++)
        {
            enemies.Add(GameObject.Instantiate(lynxPrefab, transform));
        }
    }

    public void EnemiesExitStage()
    {
        if (enemies == null)
            return;
        foreach(var e in enemies)
        {
            if(e != null)
            e.GetComponentInChildren<EnemyMovement>().ExitStage();
        }
    }
	
    public void EnemiesPause()
    {
        foreach (var e in enemies)
        {
            if (e != null)
                e.GetComponentInChildren<EnemyMovement>().Pause();
        }
    }

    public void EnemiesDestroy() {
       foreach (var e in enemies)
        {
            if (e != null)
                GameObject.Destroy(e);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
