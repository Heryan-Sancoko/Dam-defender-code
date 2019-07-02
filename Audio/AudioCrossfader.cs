using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCrossfader : MonoBehaviour {



    public List<AudioSource> groupA;
    public List<AudioSource> groupB;

    public float fadeDuration = 2;


    public List<float> defaultLevelsA;
    public List<float> defaultLevelsB;

    public bool fadeAToB;
    public bool fadeBToA;

    bool isFadingAToB;
    bool isFadingBToA;

    public float startDelay = 4;

    private void OnValidate()
    {
        if (fadeAToB)
        {
            FadeAToB();
            fadeAToB = false;
        }
        if (fadeBToA)
        {
            FadeBToA();
            fadeBToA = false;
        }
    }


    private void Start()
    {
        groupA.ForEach(s => defaultLevelsA.Add(s.volume));
        groupB.ForEach(s => defaultLevelsB.Add(s.volume));

        groupB.ForEach(s => s.volume = 0);

    }


    float fadeStart;
    public void FadeAToB()
    {
        fadeStart = Time.time;
        isFadingAToB = true;
        isFadingBToA = false;

    }

    public void FadeBToA()
    {

        fadeStart = Time.time;
        isFadingBToA = true;
        isFadingAToB = false;
    }

    

    private void Update()
    {
        if (Time.timeSinceLevelLoad < startDelay)
            return;
//        Debug.Log("a to b:" + isFadingAToB);
  //      Debug.Log("b to a:" + isFadingBToA);
        if (isFadingAToB)
        {
            float elapsedTime = Time.time - fadeStart;
            float t = elapsedTime / fadeDuration;
            if (t > 1)
            {
                isFadingAToB = false;

                return;
            }
            for (int i = 0; i < groupA.Count; i++)
            {
                groupA[i].volume =Mathf.Lerp(defaultLevelsA[i], 0, t);
                groupB[i].volume = Mathf.Lerp(0, defaultLevelsB[i], t);
            }
        }else if (isFadingBToA)
        {
            float elapsedTime = Time.time - fadeStart;
            float t = elapsedTime / fadeDuration;
            if (t > 1)
            {
                isFadingBToA = false;
                return;
            }
            for (int i = 0; i < groupA.Count; i++)
            {
                groupB[i].volume = Mathf.Lerp(defaultLevelsB[i], 0, t);
                groupA[i].volume = Mathf.Lerp(0, defaultLevelsA[i], t);
            }
        }
    }
}
