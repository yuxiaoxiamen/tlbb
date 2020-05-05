using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectControl : MonoBehaviour
{
    public static SoundEffectControl instance;
    private AudioSource audioEffect;
    public List<AudioClip> clips;
    // Start is called before the first frame update
    void Start()
    {
        audioEffect = GetComponent<AudioSource>();
        instance = this;
    }

    public void PlaySoundEffect(int i)
    {
        audioEffect.clip = clips[i];
        audioEffect.Play();
    }
}
