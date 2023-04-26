using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource button;
    [SerializeField] private AudioSource collision;
    [SerializeField] private AudioSource destroy;
    [SerializeField] private AudioSource music;

    private int soundValue;
    private float musicValue;

    private void Start()
    {
        soundValue = PlayerPrefs.GetInt("Sound");
        musicValue = PlayerPrefs.GetFloat("Music");

        button.Stop();
        collision.Stop();
        destroy.Stop();

        SetMusic();
    }

    private void OnEnable()
    {
        Menu.onButtonSound += ButtonSound;
        Menu.onSetSound += SetSound;
        Menu.onSetMusic += SetMusic;
        Ball.onCollisionSound += CollisionSound;
        Block.onDestroySound += DestroySound;
    }
    private void OnDisable()
    {
        Menu.onButtonSound -= ButtonSound;
        Menu.onSetSound -= SetSound;
        Menu.onSetMusic -= SetMusic;
        Ball.onCollisionSound -= CollisionSound;
        Block.onDestroySound -= DestroySound;
    }

    private void ButtonSound()
    {
        if(soundValue == 1)
        {
            button.Play();
        }
    }

    private void CollisionSound()
    {
        if(soundValue == 1)
        {
            collision.Play();
        }
    }

    private void DestroySound()
    {
        if(soundValue == 1)
        {
            destroy.Play();
        }
    }

    private void SetSound()
    {
        soundValue = PlayerPrefs.GetInt("Sound");
    }

    private void SetMusic()
    {
        musicValue = PlayerPrefs.GetFloat("Music");
        music.volume = musicValue;
    }
}
