using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] float pipeSpeed = 1f;
    [SerializeField] GameManager gameManager;
    float leftEdge;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    void Update()
    {
        if (gameManager.getState() == "Playing")
        {
            transform.position += Vector3.left * pipeSpeed * Time.deltaTime;
            if (transform.position.x < leftEdge)
            {
                Destroy(gameObject);
            }
        }
    }
}
