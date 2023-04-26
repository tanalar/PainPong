using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private new ParticleSystem particleSystem;
    private ParticleSystem.MainModule particleSystemMain;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMain = particleSystem.main;
    }
    private void OnEnable()
    {
        Block.onDestroy += Explosion;
    }
    private void OnDisable()
    {
        Block.onDestroy -= Explosion;
    }

    private void Explosion(Vector2 position, Color color)
    {
        particleSystem.transform.position = position;
        particleSystemMain.startColor = color;
        particleSystem.Play();
    }
}
