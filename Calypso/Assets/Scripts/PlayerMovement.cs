using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX;
    [SerializeField] float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocityY);
    }

    void OnXMove(InputValue inputValue)
    {
        movementX = inputValue.Get<float>();
    }

    void OnJump()
    {
        Debug.Log("Hello!");
    }
}
