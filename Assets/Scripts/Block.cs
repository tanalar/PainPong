using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public static Action<Vector2, Color> onDestroy;
    public static Action onDestroySound;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        onDestroySound?.Invoke();
        onDestroy?.Invoke(transform.position, spriteRenderer.color);
        Destroy(gameObject);
    }
}
