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

    private PlayerMovement playerMovement;

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
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.TakeDamage();
                StartCoroutine(HandleCollide(collision));
            }
        }
    }

    private IEnumerator HandleCollide(Collision2D collision)
    {
        justCollidedWith = true;
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            SoundManager.instance.screamSound();

            playerMovement = playerRb.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.isKnockedBack = true;
            }

            // Determine push direction
            Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            pushDirection.y = 0.1f;  
            pushDirection.x = Mathf.Sign(pushDirection.x); 

            // Reset player's velocity before applying push
            playerRb.linearVelocity = Vector2.zero;

            // Apply instant pushback
            playerRb.linearVelocity = new Vector2(pushDirection.x * pushForce, 0f);

            print("Push Direction: " + pushDirection);
            print("Player Velocity after push: " + playerRb.linearVelocity);

            yield return new WaitForSeconds(0.5f);

            if (playerMovement != null)
            {
                playerMovement.isKnockedBack = false;
            }
           
        }

        yield return new WaitForSeconds(1f);
        justCollidedWith = false;
    }

    private void OnDestroy()
    {
        if (playerMovement != null)
        {
            playerMovement.isKnockedBack = false;
        }
    }
}
