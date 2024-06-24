using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip _walkSound, _jumpSound, takeDmgSound, deathSound, rollSound, landSound;

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlaywalkSound()
    {
        PlaySound(_walkSound);
    }
    public void PlayJumpAudio()
    {
        PlaySound(_jumpSound);
    }
    public void PlayDmgAudio()
    {
        PlaySound(takeDmgSound);
    }
    public void PlayDeathAudio()
    {
        PlaySound(deathSound);
    }
    public void playRollSound()
    {
        PlaySound(rollSound);
    }
    public void PlayLandSound()
    {
        PlaySound(landSound);
    }
}
