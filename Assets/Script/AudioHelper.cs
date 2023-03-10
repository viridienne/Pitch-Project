using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Audio
{
    public string key;
    // public AudioClip clip;
    public AudioSource source;
}
public class AudioHelper : MonoBehaviour
{
    [TableList] public List<Audio> clips;
    public static AudioHelper Instance;

    private void Awake()
    {
        Instance = this;

        clips = new List<Audio>();
        foreach (Transform child in transform)
        {
            Audio a = new Audio()
            {
                key = child.name,
                source = child.GetComponent<AudioSource>(),
            };
            clips.Add(a);
        }
    }

    public AudioSource GetAudio(string _key)
    {
        var audio = clips.Find(a => a.key.Equals(_key));
        if (audio != null) return audio.source;
        return null;
    }
}
