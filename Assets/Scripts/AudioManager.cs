using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip soundEffectButton;
    public AudioClip soundEffectCorrect;
    public AudioClip soundEffectWrong;

    private AudioSource backgroundMusicSource;
    private AudioSource soundEffectButtonSource;
    private AudioSource soundEffectCorrectSource;
    private AudioSource soundEffectWrongSource;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        // Create AudioSources for background music and sound effects
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectButtonSource = gameObject.AddComponent<AudioSource>();
        soundEffectCorrectSource = gameObject.AddComponent<AudioSource>();
        soundEffectWrongSource = gameObject.AddComponent<AudioSource>();

        backgroundMusicSource.volume = 0.5f;

        // Set the audio clips
        backgroundMusicSource.clip = backgroundMusic;
        soundEffectButtonSource.clip = soundEffectButton;
        soundEffectCorrectSource.clip = soundEffectCorrect;
        soundEffectWrongSource.clip = soundEffectWrong;

        // Set the background music to loop
        backgroundMusicSource.loop = true;

        // Play the background music
        backgroundMusicSource.Play();
    }

    // Example method to play a sound effect
    public void PlaySoundEffectButton()
    {
        soundEffectButtonSource.Play();
    }

    public void PlaySoundEffectCorrect()
    {
        soundEffectCorrectSource.Play();
    }

    public void PlaySoundEffectWrong()
    {
        soundEffectWrongSource.Play();
    }

}
