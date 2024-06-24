using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound, _attackSound,_dmgSound, _deathSound;
    // Start is called before the first frame update
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayAttackSound()
    {
        PlaySound(_attackSound);
    }
    public void PlayWalkSound()
    {
        PlaySound(walkSound);
    }
    public void PlayDeathSound()
    {
        PlaySound(_deathSound);
    }
    public void PlayDMGSound()
    {
        PlaySound(_dmgSound);
    }
}
