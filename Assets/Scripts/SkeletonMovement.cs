using System.Collections;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool movingRight = false;
    private Rigidbody2D rb;
    public GameObject flipPoint1;
    public GameObject flipPoint2;
    public float pushForce;
    private bool justCollidedWith = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!justCollidedWith)
        {
            rb.linearVelocity = new Vector2(movingRight ? speed : -speed, rb.linearVelocity.y);
        }

        if ((transform.position.x <= flipPoint1.transform.position.x && !movingRight)  || (transform.position.x >= flipPoint2.transform.position.x && movingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage();
            }
            StartCoroutine(HandleCollide(collision));
        }
    }

    private IEnumerator HandleCollide(Collision2D collision)
    {
        justCollidedWith = true;
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            
            Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            pushDirection.y = 0.5f;

            playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(1f);
        justCollidedWith = false;
    }
}
