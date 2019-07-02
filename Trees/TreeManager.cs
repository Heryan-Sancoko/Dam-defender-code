using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour {

    public GameObject treePrefab;
    public int treesToSpawn = 10;
    public Transform myTreeGroup;


    GameObject[] trees;

    private void Start()
    {
        SpawnTrees();
    }


    public void SpawnTrees()
    {
        /*
        float radius = GetComponentInParent<GameManager>().GameRadius;
        trees = new GameObject[treesToSpawn];
        for(int i = 0; i < treesToSpawn; i++)
        {
            var pos2d = Random.insideUnitCircle * radius;
            var pos = new Vector3(pos2d.x, 0, pos2d.y);
            trees[i] = GameObject.Instantiate(treePrefab,pos,Quaternion.identity, transform);
        }
        */
        foreach (Transform child in myTreeGroup)
        {
            child.gameObject.tag = "Collectable";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
