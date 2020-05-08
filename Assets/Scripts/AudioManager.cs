using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

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
    [SerializeField] Sound[] fileSounds;
    Dictionary<string, Sound> sound = new Dictionary<string, Sound>();

    private void Start()
    {
        for (int i = 0; i < fileSounds.Length; i++)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            fileSounds[i].SetAudioSource(src);
            sound.Add(fileSounds[i].name, fileSounds[i]);
        }
        
    }
    public void PlaySound(string name)
    {
        if(sound.ContainsKey(name))
        {
            sound[name].Play();
        }
    }
}
