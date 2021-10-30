using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject AudioPrefab;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Audio").Length < 2)
        {
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void PlayAudio(AudioClip Audio, bool isLooping)
    {
        GameObject audioClip = Instantiate(AudioPrefab, transform);
        audioClip.transform.SetParent(transform);  
        audioClip.GetComponent<AudioPlay>().PlayAudio(Audio, isLooping);
    }
}
