using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour {


    [Range(0.25f, 2f)]
    public float pitchMin = 0.8f;
    [Range(0.25f, 2f)]

    public float pitchMax = 1.4f;



    public List<AudioClip> audioClips;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomClip()
    {
        int clipIndex = Random.Range(0, audioClips.Count - 1);
        float pitch = Random.Range(pitchMin, pitchMax);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClips[clipIndex]);

    }

    private void OnValidate()
    {
        pitchMin = Mathf.Min(pitchMin, pitchMax);
        pitchMax = Mathf.Max(pitchMin, pitchMax);

    }

    

	void Update () {
		
	}
}
