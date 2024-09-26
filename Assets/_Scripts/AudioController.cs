using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Soundnames
{
    BACKGROUND,
    VICTORY
}

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{

    public static AudioController instance { get; private set; }
    private AudioSource mAudioSource;
    [SerializeField]
    private AudioClip[] soundList;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(Soundnames m_Soundname, float volume = 1)
    {
        instance.mAudioSource.PlayOneShot(instance.soundList[(int)m_Soundname], volume);
    }

}
