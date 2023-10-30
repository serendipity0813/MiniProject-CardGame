using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bgm;

    private void Start()
    {
        this.audioSource.clip = this.bgm;
        this.audioSource.Play();
    }
}
