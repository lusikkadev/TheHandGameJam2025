using UnityEngine;

public class TheHand : MonoBehaviour
{
    [SerializeField] Animator handAnim;
    [SerializeField] AnimationClip grabAnim;
    [SerializeField] AnimationClip idleAnim;

    [SerializeField] bool triggered = false;
    [SerializeField] bool grabbing = false;

    [SerializeField] float normalSpeed = 0.2f;
    [SerializeField] float speed = 0.2f;
    [SerializeField] float verticalSpeed = 5f;
    [SerializeField] float retreatSpeed = 5f;

    [SerializeField] GameObject player;
    PlayerController playerController;

    Vector2 playerPos;
    Vector2 startPos;
    public void OnGameEnd() 
    {
        triggered = false;
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        handAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        startPos = transform.position;
        speed = normalSpeed;
    }
    private void Update()
    {
        playerPos = player.transform.position;
        float targetX = playerPos.x;
        float currentX = transform.position.x;

        if (triggered && !grabbing)
        {

            float newX = Mathf.MoveTowards(currentX, targetX, verticalSpeed * Time.deltaTime);

            float newY = Mathf.MoveTowards(transform.position.y, playerPos.y + 1f, speed * Time.deltaTime);
            transform.position = new Vector2(newX, newY);
        }
        else
        {
            float newX = Mathf.MoveTowards(currentX, transform.position.x, retreatSpeed * Time.deltaTime);
            float newY = Mathf.MoveTowards(transform.position.y, startPos.y, retreatSpeed * Time.deltaTime);
            transform.position = new Vector2(newX, newY);
        }

        if (grabbing)
        {
            handAnim.Play(grabAnim.name);
        }
        else
        {
            handAnim.Play(idleAnim.name);
        }

    }



    public void StopDescending()
    {
        triggered = false;
    }
    public void StartDescending()
    {
        triggered = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            grabbing = true;
            StopDescending();

            if (playerController != null)
            {
                playerController.PlayerGrabbed();
            }
        }
    }
}