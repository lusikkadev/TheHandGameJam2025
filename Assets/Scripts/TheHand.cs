using UnityEngine;

public class TheHand : MonoBehaviour
{
    public Animator handAnim;
    public AnimationClip grabAnim;
    public AnimationClip idleAnim;

    public bool triggered = false;
    public bool grabbing = false;

    public float normalSpeed = 0.2f;
    public float speed = 0.2f;
    public float retreatSpeed = 5f;

    public GameObject player;
    PlayerController playerController;

    Vector2 playerPos;
    Vector2 startPos;

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
        var posX = transform.position.x;
        playerPos = player.transform.position;
        

        if (triggered && !grabbing)
        {
            posX = player.transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPos.x, playerPos.y + 1f), speed * Time.deltaTime);
            transform.position = new Vector2(posX, transform.position.y);

        }
        else
        {
            posX = transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, startPos, retreatSpeed * Time.deltaTime);
            transform.position = new Vector2(posX, transform.position.y);
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
