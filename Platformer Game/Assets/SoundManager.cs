using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    private AudioSource audioSource;

    public AudioClip bgm;
    public AudioClip hitSound;
    public AudioClip attackSound;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm;

        audioSource.volume = 1f;
        audioSource.loop = true;
        audioSource.mute = false;

        audioSource.Play();               
    }

    public void AttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
    public void HitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}
