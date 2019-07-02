using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFloatUpAndDissapear : MonoBehaviour {

    // Use this for initialization
    RectTransform rectTransform;
    public float speed = 40;
    public float time = 2;

    float startTime;
    public Vector2 offset;
     public   Vector3 startPos;
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
       var timeAlive = Time.time - startTime;
        UpdatePositionRoot();
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + speed * timeAlive);
        if (timeAlive > time)
            Destroy(gameObject);
    }


    void UpdatePositionRoot()
    {
        var canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;

        Vector3 viewPortPos = Camera.main.WorldToViewportPoint(startPos);

        Vector2 screenPos = viewPortPos * canvasSize;

        rectTransform.anchoredPosition = screenPos + offset;
    }
}
