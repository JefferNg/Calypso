using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX;
    [SerializeField] float speed;
    public Animator animator;
    private bool facingRight = true;
    public int lives = 3;
    private bool _grounded = true;
    private bool _doubleJump = true;

    [SerializeField] private GameObject[] hearts;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _grounded = true;
            _doubleJump = true;
        }
    }

    void OnXMove(InputValue inputValue)
    {
        movementX = inputValue.Get<float>();
        animator.SetFloat("Speed", Mathf.Abs(movementX));
    }

    void OnJump()
    {
        if (_grounded)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            _grounded = false;
        }
        else if (_doubleJump)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            _doubleJump = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDamage()
    {
        if (lives > 0)
        {
            lives--;
            UpdateHearts();

        }
        if (lives <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= lives)
            {
                hearts[i].SetActive(false);
            } else
            {
                hearts[i].SetActive(true);
            }
        }
    }
}
