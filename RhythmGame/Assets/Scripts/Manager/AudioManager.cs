using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private static AudioManager Instance;

    public static AudioManager instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = FindObjectOfType<AudioManager>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newobj = new GameObject().AddComponent<AudioManager>();
                    Instance = newobj;
                }
            }
            return Instance;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<AudioManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public float CheckProgress()
    {
        return bgmPlayer.time / bgmPlayer.clip.length;
    }

    public bool IsBGMPlaying()
    {
        return bgmPlayer.isPlaying;
    }

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName.Equals(bgm[i].name))
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void ReplayBGM()
    {
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName.Equals(sfx[i].name))
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfx[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
            }
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmPlayer.volume = volume;
    }

    public void SetSfXVolume(float volume)
    {
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayer[i].volume = volume;
        }
    }
}
