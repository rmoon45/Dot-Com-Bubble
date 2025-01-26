using System.Collections;
using UnityEngine;

public class AudioMan : MonoBehaviour
{
    [SerializeField] private AudioSource osBootup;
    [SerializeField] private AudioSource clickOn;
    [SerializeField] private AudioSource loopingFan;
    [SerializeField] private AudioSource music;
    
    [SerializeField] private float musicFadeTime;

    [SerializeField] private float osBootupDelaySeconds;
    [SerializeField] private float musicDelaySeconds;

    void Awake()
    {
        clickOn.Play();

        float len = clickOn.clip.length;
        
        osBootup.PlayDelayed(osBootupDelaySeconds);
        loopingFan.PlayDelayed(len);
        
        StartCoroutine(PlayMusic(music, musicFadeTime));
    }
    
    IEnumerator PlayMusic(AudioSource audioSource, float fadeTime)
    {
        yield return new WaitForSeconds(musicDelaySeconds);
        audioSource.Play();

        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, 0.3f, t / fadeTime);
            yield return null;
        }
    }
}
