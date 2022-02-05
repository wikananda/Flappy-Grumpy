using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject grumpyPrefab;
    [SerializeField] ScoreManager scoreManager;

    public enum State
    {
        GameStart,
        Playing,
        GameOver
    }
    public State state;

    GameObject scorePanel, startPanel, gameOverPanel;
    GameObject grumpy;


    void Start()
    {
        state = State.GameStart;
        scorePanel = GameObject.Find("Score");
        startPanel = GameObject.Find("StartScreen");
        gameOverPanel = GameObject.Find("EndPanel");
        scorePanel.SetActive(false);
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        switch (state)
        {
            case State.GameStart:
                if (grumpy == null)
                {
                    grumpy = Instantiate(grumpyPrefab, new Vector3(-5, 0, -1), Quaternion.identity);
                }

                scorePanel.SetActive(false);
                startPanel.SetActive(true);
                gameOverPanel.SetActive(false);

                scoreManager.ResetScore();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
                break;
            case State.Playing:
                scorePanel.SetActive(true);
                startPanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case State.GameOver:
                scorePanel.SetActive(false);
                startPanel.SetActive(false);
                gameOverPanel.SetActive(true);

                scoreManager.SetHighScore();
                scoreManager.SetFinalScore();
                break;
        }
    }



    public string getState()
    {
        return state.ToString();
    }

    public void changeState(int state)
    {
        if (state == 0)
        {
            this.state = State.GameStart;
        }
        else if (state == 1)
        {
            this.state = State.Playing;
        }
        else if (state == 2)
        {
            this.state = State.GameOver;
        }
    }
    public void BackToMainMenu()
    {
        state = State.GameStart;
        Destroy(grumpy);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

