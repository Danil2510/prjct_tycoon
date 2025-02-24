using UnityEngine;

public class PlayRandomMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] musicClip;
    [SerializeField] AudioSource musicSource;

    public void Play()
    {
        musicSource.clip = musicClip[Random.Range(0, musicClip.Length)];
        musicSource.Play();
    }
}
