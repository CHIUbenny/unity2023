using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class XXmain : MonoBehaviour
{
    public static XXmain instance;
    public AudioMixer audioMixer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetMusicVolume(float v)
    {
        audioMixer.SetFloat("MusicV", v);
    }
    public void SetAudioVolume(float v)
    {
        audioMixer.SetFloat("AudioV", v);
    }
    public float GetMusicVolume()
    {
        float ov = 1.0f;
        audioMixer.GetFloat("MusicV", out ov);
        return ov; 
    }
    public float GetAudioVolume()
    {
        float ov = 1.0f;
        audioMixer.GetFloat("AudioV", out ov);
        return ov;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
