using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    FlashLight flashLight;
    Animator animator;
    CameraController mainCamera;
    GameManager gameManager;

    public GameObject pickupPoint;
    public AnimationClip runAnim;
    public AnimationClip idleAnim;


    Vector2 movementInput;
    Vector2 smoothedMovementInput;
    Vector2 movementInputSmoothVelocity;

    public float speed = 2f;
    public float jumpForce = 125f;
    public float gravityModifier = 0.2f;
    public bool isGrounded = true;
    bool notJumping = true;
    bool facingRight = true;


    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        mainCamera = FindFirstObjectByType<CameraController>();
        flashLight = GetComponentInChildren<FlashLight>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        facingRight = transform.localScale.x >= 0;
    }

    void Start()
    {

    }

    void Update()
    {
        notJumping = rb.linearVelocity.y <= 0.1f;

        if (!isGrounded)
        {
            rb.gravityScale += gravityModifier * Time.deltaTime;
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

        if (smoothedMovementInput.x > 0.01f && !facingRight) Flip();
        else if (smoothedMovementInput.x < -0.01f && facingRight) Flip();

        float targetZ = 0f;
        float currentZ = rb.rotation;
        float correctedZ = Mathf.LerpAngle(currentZ, targetZ, 10f * Time.deltaTime);
        rb.MoveRotation(correctedZ);
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

    void OnAttack()
    {
        if (flashLight.isOn)
        {
            flashLight.TurnOff();
        }
        else
        {
            flashLight.TurnOn();
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


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x *= -1f;
        transform.localScale = s;
    }


    private void RestartThisScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void PlayerGrabbed(Transform handTransform)
    {
        transform.SetParent(pickupPoint.transform);

        if (mainCamera != null)
        {
            mainCamera.enabled = false;
        }

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameManager.GameOver();
        this.enabled = false;
    }
}
