using System;
using Unity.VisualScripting;
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
    public bool isKnockedBack = false;

    [SerializeField] GameObject[] hearts;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (_grounded && movementX != 0 && !isKnockedBack)
        {
            if (!SoundManager.instance.GetComponent<AudioSource>().isPlaying && !GameManager.instance.inMenu)
            {
                SoundManager.instance.walkingSound();
            }
        }
        else
        {
            SoundManager.instance.StopWalkingSound();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocityY);
        }

        _grounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, groundLayer);
        animator.SetBool("Jump", !_grounded);
        
        if (_grounded)
        {
            _doubleJump = true;
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
        if (_grounded && !isKnockedBack)
        {
            Jump();
            SoundManager.instance.jumpSound();
            _doubleJump = true;
        }
        else if (_doubleJump && !isKnockedBack)
        {
            Jump();
            SoundManager.instance.doubleJumpSound();
            _doubleJump = false;
        }
    }
    private void Jump()
    {
        animator.SetBool("Jump", true);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed * 1.5f);
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
            SoundManager.instance.loseSound();
            GameManager.instance.ToggleGameMenu();
            Time.timeScale = 0;
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }
    }
}
