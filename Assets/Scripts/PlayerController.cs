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
    AudioManager audioManager;

    public GameObject pickupPoint;
    public AnimationClip runAnim;
    public AnimationClip idleAnim;
    public AnimationClip jumpAnim;
    public AnimationClip landAnim;
    public AnimationClip grabAnim;
    public AudioClip walkSound;

    Vector2 movementInput;
    Vector2 smoothedMovementInput;
    Vector2 movementInputSmoothVelocity;

    public float speed = 2f;
    public float jumpForce = 125f;
    public float gravityModifier = 0.2f;
    public bool isGrounded = true;
    bool notJumping = true;
    bool facingRight = true;
    public bool grabbed = false;

    bool isLanding = false;
    float landingTimer = 0f;
    float landingDuration = 0.2f;


    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        mainCamera = FindFirstObjectByType<CameraController>();
        flashLight = GetComponentInChildren<FlashLight>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = FindFirstObjectByType<AudioManager>();

        facingRight = transform.localScale.x >= 0;
    }

    void Start()
    {

    }

    void Update()
    {
        //notJumping = rb.linearVelocity.y <= 0.1f;

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
        if (grabbed)
        {
            animator.Play(grabAnim.name);
            return; // Prevent other animations while grabbed
        }

        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rb.linearVelocity = new Vector2(smoothedMovementInput.x * speed, rb.linearVelocity.y);

        if (smoothedMovementInput.x > 0.01f && !facingRight) Flip();
        else if (smoothedMovementInput.x < -0.01f && facingRight) Flip();

        float targetZ = 0f;
        float currentZ = rb.rotation;
        float correctedZ = Mathf.LerpAngle(currentZ, targetZ, 10f * Time.deltaTime);
        rb.MoveRotation(correctedZ);

        bool isInput = Mathf.Abs(movementInput.x) > 0.01f;

        if (isLanding)
        {
            animator.Play(landAnim.name);
            
            landingTimer -= Time.fixedDeltaTime;
            if (landingTimer <= 0f)
            {
                isLanding = false;
            }
            return; // Prevent other animations while landing
        }

        if (!notJumping)
        {
            animator.Play(jumpAnim.name);
            
        }
        else if (isInput)
        {
            animator.Play(runAnim.name);
        }
        else
        {
            animator.Play(idleAnim.name);
        }
    }

    private void OnMove(InputValue value)
    {
        if (grabbed) return;
        print("OnMove called");
        movementInput = value.Get<Vector2>();


    }

    void OnJump()
    {
        if (grabbed) return;
        if (isGrounded && notJumping)
        {
            
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            notJumping = false;
            
        }
    }

    void OnAttack()
    {
        if (grabbed)
        {
            flashLight.TurnOff();
            return;
        }
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
            isLanding = true;
            landingTimer = landingDuration;
            isGrounded = true;
            rb.gravityScale = 1f;
            notJumping = true;
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

    public void PlayerGrabbed()
    {
        flashLight.TurnOff();
        grabbed = true;
        transform.SetParent(pickupPoint.transform);
        transform.localPosition = Vector3.zero;

        if (mainCamera != null)
        {
            mainCamera.enabled = false;
        }

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameManager.GameOver();

        //this.enabled = false;
    }


}
