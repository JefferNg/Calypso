using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX;
    [SerializeField] float speed;
    public Animator animator;
    private bool facingRight = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocityY);
        if (rb.linearVelocityY == 0)
        {
            animator.SetBool("Jump", false);
        }

        if (movementX > 0 && !facingRight)
        {
            Flip();
        }
        else if (movementX < 0 && facingRight)
        {
            Flip();
        }
    }

    void OnXMove(InputValue inputValue)
    {
        movementX = inputValue.Get<float>();
        animator.SetFloat("Speed", Mathf.Abs(movementX));
    }

    void OnJump()
    {
        animator.SetBool("Jump", true);
        rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
