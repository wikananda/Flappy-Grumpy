using UnityEngine;

public class Grumpy : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float jumpForce = 0f;
    [SerializeField] ScoreManager scoreManager;
    string state;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject pipeSpawnerPrefab;
    GameObject pipeSpawner;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        state = gameManager.getState();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Scoring")
        {
            Debug.Log("Scoring");
            scoreManager.ScoreUp();
        }
        if (other.tag == "Obstacle")
        {
            KillGrumpy();
            Debug.Log("Hit Obstacle");
        }
        if (other.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }

    void KillGrumpy()
    {
        gameManager.changeState(2);
        Destroy(pipeSpawner);
    }

}
