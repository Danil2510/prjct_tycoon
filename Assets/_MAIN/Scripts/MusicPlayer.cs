using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] musicClip;
    [SerializeField] AudioClip curClip;
    [SerializeField] AudioSource musicSource;
    float cliplenght;

    private void Start()
    {
        selectNewClip();
    }
    private void Update()
    {
        if (cliplenght <= 0)
        {
            selectNewClip();
        }
        else 
        {
            cliplenght -= Time.deltaTime;
        }
    }

    void selectNewClip()
    {
        curClip = musicClip[Random.Range(0, musicClip.Length)];
        musicSource.clip = curClip;
        cliplenght = curClip.length;
        musicSource.Play();
    }
}
