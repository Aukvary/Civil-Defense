using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStep : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void SoundStepEvent()
    {
        _audioSource.Play();
    }
}
