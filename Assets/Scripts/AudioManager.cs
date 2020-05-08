using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]

public class Sound
{
    public string name;
    public AudioClip clip;
    public bool playOnAwake = false;
    public bool loop = false;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0f, 3f)] public float pitch = 1f;
    private AudioSource source;
    public void SetAudioSource(AudioSource src)
    {
        source = src;
        source.clip = clip;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.volume = volume;
        source.pitch = pitch;
    }
    public void Play()
    {
        source.Play();
    }
 }

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sound;
    private void Start()
    {
        for(int i=0;i<sound.Length;i++)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            sound[i].SetAudioSource(src);
        }
        
    }
    public void PlaySound(string name)
    {
        for(int i=0;i<sound.Length;i++)
        {
            if(sound[i].name == name)
            {
                sound[i].Play();
                break;
            }    
        }    
    }    


}
