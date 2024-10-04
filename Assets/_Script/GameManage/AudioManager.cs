using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip coin;
    public AudioClip clickSound;
    public AudioSource backGroundMusic;
    public AudioSource soundEffect;
    public AudioDataBase audioDataBase;   
    public float volume = 1.0f; 
    public  bool isMuted = false;
    public int setMusic;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        else
        {          
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Start()
    {
       

        backGroundMusic = gameObject.AddComponent<AudioSource>();
        soundEffect = gameObject.AddComponent<AudioSource>();
        foreach (var audio in audioDataBase.audioDataBases)
        {
            if (audio.AudioID == setMusic)
            {
                PlayBackgoundMusic(audio.Sound);
                break;
            }
        }
    }
    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffect.PlayOneShot(clip);
        Debug.Log($"PlayAudio: {clip}");
    }
   
    public void PlayBackgoundMusic(AudioClip clip)
    {
        backGroundMusic.clip = clip;
        backGroundMusic.Play();
        backGroundMusic.loop = true;
    }
    public void ChangeBackGroundMusic(int clipID)
    {
        if (backGroundMusic.isPlaying)
        {         
            backGroundMusic.Stop();
        }
        

        foreach (var audio in audioDataBase.audioDataBases)
        {
            if (audio.AudioID == clipID)
            {
                PlayBackgoundMusic(audio.Sound);
                break;
            }
        }
    }
    
    public void OnButtonClickAudio()
    {
        soundEffect.PlayOneShot(clickSound);
    }
    public void CointEffect()
    {
        soundEffect.PlayOneShot(coin);
    }
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0f, 1f); 
        Debug.Log($"Volume set to: {volume}");
    }

    public void Mute()
    {
        isMuted = true;
        backGroundMusic.volume = 0f;
        Debug.Log("Music Muted");
    }

    public void Unmute()
    {
        isMuted = false;
        backGroundMusic.volume = volume; 
        Debug.Log("Music Unmuted");
    }

}
