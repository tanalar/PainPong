using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private bool pause = false;
    private Vector3 pos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    private void OnEnable()
    {
        Menu.onPause += Pause;
    }
    private void OnDisable()
    {
        Menu.onPause -= Pause;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!pause)
        {
            if (collision.gameObject.tag == "Touch")
            {
                pos = collision.transform.position;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = (pos - transform.position) * 50000 * Time.deltaTime;
    }

    private void Pause(bool pause)
    {
        this.pause = pause;
    }
}
