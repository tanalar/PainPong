using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform playerCenter;
    [SerializeField] private Vector3 centralPoint;
    private Vector3 ball;
    private Vector3 randomPoint;
    private Vector3 dodgePoint;
    private bool ballIsNear = false;
    private bool pause = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomPoint());
    }

    private void OnEnable()
    {
        Menu.onPause += Pause;
    }
    private void OnDisable()
    {
        Menu.onPause -= Pause;
    }

    private IEnumerator RandomPoint()
    {
        randomPoint = new Vector3(centralPoint.x + Random.Range(-1.75f, 1.75f), centralPoint.y + Random.Range(-1.75f, 1.75f), 0);
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(RandomPoint());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            ballIsNear = true;
            ball = collision.transform.position;

            if (playerCenter.position.x < ball.x)
            {
                if (playerCenter.position.y < ball.y)
                {
                    dodgePoint = new Vector2(transform.position.x - 2.5f, transform.position.y - 2.5f);
                }
                else
                {
                    dodgePoint = new Vector2(transform.position.x - 2.5f, transform.position.y + 2.5f);
                }
            }

            else
            {
                if (playerCenter.position.y < ball.y)
                {
                    dodgePoint = new Vector2(transform.position.x + 2.5f, transform.position.y - 2.5f);
                }
                else
                {
                    dodgePoint = new Vector2(transform.position.x + 2.5f, transform.position.y + 2.5f);
                }
            }
        }

        if (collision.gameObject.tag == "LeftWall")
        {
            if (playerCenter.position.y < ball.y)
            {
                dodgePoint = new Vector2(transform.position.x + 4, transform.position.y - 1);
            }
            else
            {
                dodgePoint = new Vector2(transform.position.x + 4, transform.position.y + 1);
            }
        }
        if (collision.gameObject.tag == "RightWall")
        {
            if (playerCenter.position.y < ball.y)
            {
                dodgePoint = new Vector2(transform.position.x - 4, transform.position.y - 1);
            }
            else
            {
                dodgePoint = new Vector2(transform.position.x - 4, transform.position.y + 1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            ballIsNear = false;
        }
    }

    private void FixedUpdate()
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
        if (!ballIsNear)
        {
            Vector3 pos = randomPoint;
            rb.velocity = (pos - transform.position) * 250 * Time.deltaTime;
        }
        else
        {
            Vector3 pos = dodgePoint;
            rb.velocity = (pos - transform.position) * 1000 * Time.deltaTime;
        }
    }

    private void Pause(bool pause)
    {
        this.pause = pause;
    }
}
