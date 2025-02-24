using UnityEngine;

public class PlayWaitPlay : MonoBehaviour
{
    [SerializeField] float f;
    float g;
    bool n = false;
    [SerializeField] AudioClip[] musicClip;
    [SerializeField] AudioSource musicSource;

    public void Play()
    {
        musicSource.clip = musicClip[0];
        musicSource.Play();
        g = f;
        n= true;
    }

    private void Update()
    {
        if (n)
        {
            g -= Time.deltaTime;
            if (g <= 0)
            {
                musicSource.clip = musicClip[1];
                musicSource.Play();
                n=false;
            }
        }
    }
}
