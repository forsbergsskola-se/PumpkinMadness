using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{

    [SerializeField] private AudioSource _audioSource;

    public void PlayAudio(AudioClip Audio, bool isLooping)
    {
        _audioSource.clip = Audio;
        _audioSource.loop = isLooping;
        _audioSource.Play();
        if (isLooping == false)
        {
            Invoke("DestroyComp", _audioSource.clip.length);
        }
        
    }

    public void DestroyComp()
    {
        Destroy(this.gameObject);
    }
}
