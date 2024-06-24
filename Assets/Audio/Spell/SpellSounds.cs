using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSound : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip onStartSound;
    // Start is called before the first frame update
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayNormalSound()
    {
        PlaySound(onStartSound);
    }
    
}
