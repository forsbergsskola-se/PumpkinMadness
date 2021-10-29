using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject AudioPrefab;
    
    

    public void PlayAudio(AudioClip Audio, bool isLooping)
    {
        GameObject audioClip = Instantiate(AudioPrefab, transform);
        audioClip.transform.SetParent(transform);  
        audioClip.GetComponent<AudioPlay>().PlayAudio(Audio, isLooping);
    }
}
