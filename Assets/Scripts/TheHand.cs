using UnityEngine;
using UnityEngine.UIElements;

public class TheHand : MonoBehaviour
{
    public Animator handAnim;
    public AnimationClip grabAnim;
    public AnimationClip idleAnim;

    private bool triggered = false;
    public bool grabbing = false;

    public float normalSpeed = 0.2f;
    public float speed = 0.2f;
    public float verticalSpeed = 5f;
    public float retreatSpeed = 5f;

    public GameObject player;
    PlayerController playerController;

    Vector2 playerPos;
    Vector2 startPos;

    bool ended = false;
    public void OnGameEnd() 
    {
        ended = true;
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
    }
    private void Update()
    {
        if (ended) return;
        
        playerPos = player.transform.position;
        float targetX = playerPos.x;
        float currentX = transform.position.x;



        Debug.Log($"T {triggered}, G {grabbing}");
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
            if(handAnim)
                handAnim.Play(grabAnim.name);
        }
        else
        {
            if (handAnim)
                handAnim.Play(idleAnim.name);
        }

    }



    public void StopDescending()
    {
        triggered = false;
        Debug.LogWarning("NotTriggered");
    }
    public void StartDescending()
    {
        triggered = true;
        Debug.LogWarning("Triggered");
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