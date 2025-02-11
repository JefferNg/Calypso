using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool movingRight = false;
    private Rigidbody2D rb;
    public GameObject flipPoint1;
    public GameObject flipPoint2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the skeleton
        rb.linearVelocity = new Vector2(movingRight ? speed : -speed, rb.linearVelocity.y);

        // Check for ground ahead

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
}
