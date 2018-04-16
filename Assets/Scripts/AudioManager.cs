using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    //Global Instance
    public static AudioManager Instance = null;

    public AudioClip[] m_audioData;

    private Dictionary<string, AudioSource> m_audioSrcDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            //IDK Reinitialize?
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        m_audioSrcDictionary = new Dictionary<string, AudioSource>();
        GameObject audioSrcObject = new GameObject("AudioSource");
        audioSrcObject.transform.parent = transform;
        foreach (AudioClip ad in m_audioData)
        {
            AudioSource audioSource = audioSrcObject.AddComponent<AudioSource>();
            audioSource.clip = ad;
            audioSource.playOnAwake = false;
            m_audioSrcDictionary[ad.name] = audioSource;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Plays the sound.
    /// </summary>
    /// <param name="sound">The sound.</param>
    public void PlaySound(string sound)
    {
        try
        {
            m_audioSrcDictionary[sound].Play();
        }
        catch (System.Exception e) {
            Debug.LogWarning("Audio Clip not found");
        }
    }
}
