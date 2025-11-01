using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    FlashLight flashLight;
    Animator animator;
    public AnimationClip runAnim;
    public AnimationClip idleAnim;


    Vector2 movementInput;
    Vector2 smoothedMovementInput;
    Vector2 movementInputSmoothVelocity;

    public float speed = 2f;
    public float jumpForce = 125f;
    public bool isGrounded = true;
    bool notJumping = true;


    private void Awake()
    {
        flashLight = GetComponentInChildren<FlashLight>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        notJumping = rb.linearVelocity.y <= 0.1f;

        if (!isGrounded)
        {
            rb.gravityScale += 0.2f * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartThisScene();
        }

        if (Input.GetKeyDown(KeyCode.F) && !flashLight.isOn)
        {
            flashLight.TurnOn();
        }
        else if (Input.GetKeyDown(KeyCode.F) && flashLight.isOn)
        {
            flashLight.TurnOff();
        }
    }

    private void FixedUpdate()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rb.linearVelocity = new Vector2(smoothedMovementInput.x * speed, rb.linearVelocity.y);

        float targetZ = 0f;
        float currentZ = rb.rotation;
        float correctedZ = Mathf.LerpAngle(currentZ, targetZ, 10f * Time.deltaTime);
        rb.MoveRotation(correctedZ);

        if (movementInput.x != 0)
            transform.localScale = new Vector2(movementInput.x, transform.localScale.y);
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        animator.Play(movementInput.x != 0 ? runAnim.name : idleAnim.name);

    }

    void OnJump()
    {
        if (isGrounded && notJumping)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!isGrounded)
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
