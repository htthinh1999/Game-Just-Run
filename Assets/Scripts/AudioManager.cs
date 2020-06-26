using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool playOnAwake = false;
    public bool loop = false;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0f, 3f)] public float pitch = 1f;
    [HideInInspector] public AudioSource source;
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
    public static AudioManager Instance;
    [Header("UI")]
    [SerializeField] Image volumeImage;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;

    [Header("Other")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] Sound[] fileSounds;
    [SerializeField] float animalsSoundDelayMin = 2;
    [SerializeField] float animalsSoundDelayMax = 5;

    List<AudioSource> audioSources = new List<AudioSource>();

    Dictionary<string, Sound> sound = new Dictionary<string, Sound>();
    string[] strAnimals = new string[] { "eagle", "tiger" };
    
    float volume = 1;
    bool audioEnabled = true;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume", 1);
        if (SceneManager.GetActiveScene().name == "UIScene")
        {
            volumeSlider.value = volume;
            SetVolume();
        }
        for (int i = 0; i < fileSounds.Length; i++)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            audioSources.Add(src);
            fileSounds[i].SetAudioSource(src);
            sound.Add(fileSounds[i].name, fileSounds[i]);
        }
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].volume = volume;
        }
        PlaySound("bgm" + UnityEngine.Random.Range(1, 3));
        if (SceneManager.GetActiveScene().name != "UIScene")
        {
            PlayAnimalSounds();
        }
    }

    public void PlaySound(string name)
    {
        if(sound.ContainsKey(name))
        {
            sound[name].Play();
        }
    }

    void PlayAnimalSounds()
    {
        StartCoroutine(_PlayAnimalSounds());
    }

    IEnumerator _PlayAnimalSounds()
    {
        while (!GameManager.Instance.GameOver)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(animalsSoundDelayMin, animalsSoundDelayMax));
            PlaySound(strAnimals[UnityEngine.Random.Range(0, strAnimals.Length)]);
        }
    }

    public void SetVolume()
    {
        volume = volumeSlider.value;
        for(int i=0; i<audioSources.Count; i++)
        {
            audioSources[i].volume = volume;
        }
        if(volume == volumeSlider.minValue)
        {
            audioEnabled = false;
            volumeImage.sprite = musicOff;
        }
        else
        {
            audioEnabled = true;
            volumeImage.sprite = musicOn;
        }
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void TurnOnOffVolume()
    {
        if (audioEnabled)
        {
            volume = 0;
            audioEnabled = false;
            volumeImage.sprite = musicOff;
        }
        else
        {
            volume = 1;
            audioEnabled = true;
            volumeImage.sprite = musicOn;
        }
        volumeSlider.value = volume;
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].volume = volume;
        }
        PlayerPrefs.SetFloat("Volume", volume);
    }

}
