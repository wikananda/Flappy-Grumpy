using UnityEngine;

public class Grumpy : MonoBehaviour
{
    [SerializeField] float jumpForce = 0f;
    [SerializeField] GameObject pipeSpawnerPrefab;
    [SerializeField] AudioClip scoreSFX;
    [SerializeField] AudioClip gameOverSFX;
    AudioSource audioSource;

    Rigidbody2D rigid;
    ScoreManager scoreManager;
    GameManager gameManager;
    GameObject pipeSpawner;
    string state;

    private void Start()
    {
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        state = gameManager.getState();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void Update()
    {
        state = gameManager.getState();
        switch (state)
        {
            case "GameStart":
                rigid.bodyType = RigidbodyType2D.Static;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigid.bodyType = RigidbodyType2D.Dynamic;
                    Flap();
                    gameManager.changeState(1);
                    pipeSpawner = Instantiate(pipeSpawnerPrefab, new Vector3(10, 0, -12), Quaternion.identity);
                }
                break;
            case "Playing":
                PlayerControl();
                if (rigid.velocity.y < 0)
                {
                    RotateGrumpy(-35, Mathf.Abs(rigid.velocity.y));
                }
                else if (rigid.velocity.y == 0)
                {
                    RotateGrumpy(0, 50);
                }
                break;
            case "GameOver":
                if (transform.position.y < -5)
                {
                    Destroy(gameObject);
                }
                break;
        }

    }



    void PlayerControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }
    }

    void Flap()
    {
        RotateGrumpy(35, 1000);
        rigid.velocity = new Vector2(0, 0);
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void RotateGrumpy(float targetAngle, float turnSpeed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);
    }
    void KillGrumpy()
    {
        gameManager.changeState(2);
        Destroy(pipeSpawner);
        audioSource.PlayOneShot(gameOverSFX);
        GetComponent<BoxCollider2D>().enabled = false;
    }





    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            KillGrumpy();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Scoring")
        {
            scoreManager.ScoreUp();
            audioSource.PlayOneShot(scoreSFX);
        }
    }

}
