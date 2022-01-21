using UnityEngine;

public class TextureScrolling : MonoBehaviour
{
    Material material;
    Vector2 offset;
    float startTime;
    float tempBgSpeed;
    [SerializeField] float backgroundSpeed;
    [SerializeField] GameManager gameManager;
    string state;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        startTime = Time.deltaTime;
        tempBgSpeed = backgroundSpeed;
    }

    void Update()
    {
        state = gameManager.getState();
        if (state == "GameOver")
        {
            backgroundSpeed = 0f;
        }
        else
        {
            backgroundSpeed = tempBgSpeed;
        }
        offset = new Vector2(startTime * backgroundSpeed / 10, 0);
        material.mainTextureOffset += offset;
        if (material.mainTextureOffset == new Vector2(1, 0))
        {
            material.mainTextureOffset = new Vector2(0, 0);
        }
    }
}
