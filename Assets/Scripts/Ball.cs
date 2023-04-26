using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool rage;
    [SerializeField] private Rigidbody2D rb;
    private int rageCounter = 0;
    private Vector2 direction;
    private float speed = 2500;
    private bool pause = false;

    public static Action onCollisionSound;

    public void Start()
    {
        Launch();
        if (rage)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(Rage());
        }
    }

    private void OnEnable()
    {
        Menu.onPause += Pause;
    }
    private void OnDisable()
    {
        Menu.onPause -= Pause;
    }

    public void FixedUpdate()
    {
        if (!pause)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Move()
    {
        rb.velocity = direction * speed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //рикошет
        var contactPoint = collision.contacts[0].point;
        Vector2 ballLocation = transform.position;
        var inNormal = (ballLocation - contactPoint).normalized;
        direction = Vector2.Reflect(direction, inNormal);



        if (collision.gameObject.layer == 11) //звук при касании стенки
        {
            onCollisionSound?.Invoke();
        }
        if (collision.gameObject.tag == "DeathWall")
        {
            transform.position = new Vector2(0, 0);
        }
    }

    private void Launch()
    {
        float x = Random.Range(-2f, 2f) == 0 ? -1f : 1f;
        float y = Random.Range(-2f, 2f) == 0 ? -1f : 1f;
        direction = new Vector2(x, y);
    }

    private IEnumerator Rage()
    {
        yield return new WaitForSeconds(0.25f);

        if(rageCounter < 80)
        {
            if (!pause)
            {
                rageCounter++;
            }
        }
        else
        {
            if(speed < 4000)
            {
                speed = 4000;
            }
            spriteRenderer.color = new Color(Random.Range(0.2f, 0.8f), Random.Range(0.9f, 1), Random.Range(0.1f, 0.6f));
        }
        StartCoroutine(Rage());
    }

    private void Pause(bool pause)
    {
        this.pause = pause;
    }
}
