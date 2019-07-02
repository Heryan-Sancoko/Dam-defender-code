using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOverlayManager : MonoBehaviour {



    public GameObject healthSubtractPrefab;

    public GameObject healthAddPrefab;

    public GameObject woodAddPrefab;


    private void Start()
    {
//        InstantiatePrefabAtWorldPosition(healthSubtractPrefab, Vector3.zero);
    }


    public void DisplayAddWoodAtPosition(Vector3 pos)
    {
        InstantiatePrefabAtWorldPosition(woodAddPrefab, pos);
    }
    public void DisplaySubtractHealthAtPosition(Vector3 pos)
    {
      
        InstantiatePrefabAtWorldPosition(healthSubtractPrefab, pos);
    }

    public void DisplayAddHealthAtPosition(Vector3 pos)
    {

        InstantiatePrefabAtWorldPosition(healthAddPrefab, pos);
    }


    public void InstantiatePrefabAtWorldPosition(GameObject prefab, Vector3 pos)
    {
        var canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;


        Vector3 viewPortPos = Camera.main.WorldToViewportPoint(pos);

        Vector2 screenPos = viewPortPos * canvasSize;
        

       var go = GameObject.Instantiate(prefab, transform, false);
        go.GetComponent<UIFloatUpAndDissapear>().startPos = pos;
    }

}
