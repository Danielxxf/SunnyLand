using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundMananger instance;
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, cherryAudio,deathAudio;

    private void Awake()
    {
        instance = this;
    }

    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }

    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }

    public void CherryAudio()
    {
        audioSource.clip = cherryAudio;
        audioSource.Play();
    }

    public void DeathAudio()
    {
        audioSource.clip = deathAudio;
        audioSource.Play();
    }

    void AudioPlay()
    {
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
