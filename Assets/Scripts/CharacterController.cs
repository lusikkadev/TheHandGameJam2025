using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 movementInput;
    Vector2 smoothedMovementInput;
    Vector2 movementInputSmoothVelocity;

    public float speed = 2f;
    public float jumpForce = 125f;
    public bool isGrounded = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (!isGrounded)
        {
            rb.gravityScale += 0.2f * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartThisScene();
        }
    }

    private void FixedUpdate()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rb.linearVelocity = new Vector2(smoothedMovementInput.x * speed, rb.linearVelocity.y);
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * 300f);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var notJumping = rb.linearVelocity.y <= 0.1f;

        if (!isGrounded && notJumping)
        {
            isGrounded = true;
            rb.gravityScale = 1f;
        }
        else
        {
            return;
        }
    }


    private void RestartThisScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
