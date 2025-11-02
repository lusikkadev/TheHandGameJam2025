using UnityEngine;

public class TheHand : MonoBehaviour
{
    public Animator handAnim;
    AnimationClip handIdle;
    AnimationClip handGrab;

    public bool triggered = false;

    public float normalSpeed = 0.2f;
    public float speed = 0.2f;
    public float retreatSpeed = 5f;

    public GameObject player;
    Vector2 playerPos;
    Vector2 startPos;


    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        var posX = transform.position.x;
        playerPos = player.transform.position;
        posX = player.transform.position.x;


        if (triggered)
        {
           
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPos.x, playerPos.y + 1f), speed * Time.deltaTime);
            transform.position = new Vector2(posX, transform.position.y);

        }

        else if (!triggered)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, retreatSpeed * Time.deltaTime);
            transform.position = new Vector2(posX, transform.position.y);
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
            //handAnim.Play("Hand_Grab");
            StopDescending();

            var playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.PlayerGrabbed(transform);
            }
        }
    }
}
