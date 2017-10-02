﻿using UnityEngine;

[System.Serializable]

public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Range(0f, 1f)]
    public float pitch = 1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}
public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one SoundManager in scene");
        }
        else
        {
            instance = this;

        }

    }

    private void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject _gameobject = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(_gameobject.AddComponent<AudioSource>());
        }
    }

    public void PlaySound (string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not found, " + _name);
    }

}
