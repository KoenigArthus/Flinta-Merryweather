using UnityEngine.Audio;
using System.Collections;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float fadeTime;
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.audioSource =gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    private void Start()
    {

    }
    

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("The Sound called" + name + "wasn't found !");
            return;
        }
        s.audioSource.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("The Sound called" + name + "wasn't found !");
            return;
        }
        s.audioSource.Pause();
    }

    public IEnumerator FadeTrack(Controller pcon)
    {
        float timeToFade = fadeTime;
        float timeElapsed = 0f;
        Debug.Log(pcon.currentScene);
        Debug.Log(pcon.sceneInfo.previousScene);
        Play(pcon.currentScene);

        while (timeElapsed < timeToFade)
        {
            Array.Find(sounds, sound => sound.name == pcon.currentScene).volume = 
                Mathf.Lerp(0, 0.8f, timeElapsed / timeToFade);
            Array.Find(sounds, sound => sound.name == pcon.sceneInfo.previousScene).volume =
                Mathf.Lerp(0.8f, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        Stop(pcon.sceneInfo.previousScene);
        StopCoroutine(FadeTrack(pcon));
    }

}
