using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleSlider : MonoBehaviour {


    public Vector2 startPos;
    public Vector2 endPos;

    RectTransform rectTransform;
	void Start () {
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetPositionFromTime(float time)
    {
        if (time < 6)
            time += 24;
        time -= 6;
        float t = time / 24;
        rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

    }
}
