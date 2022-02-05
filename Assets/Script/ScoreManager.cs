using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text textScore;
    [SerializeField] TMP_Text highScore;
    [SerializeField] TMP_Text highScoreEnd;
    [SerializeField] TMP_Text textScoreFinal;


    GameManager gameManager;


    int score;


    void Start()
    {
        score = 0;
        textScore.text = score.ToString();
        highScore.text = $"Highscore : {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
        highScoreEnd.text = $"Highscore : {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
    }



    internal void ScoreUp()
    {
        score++;
        textScore.text = score.ToString();
    }

    internal void SetHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = $"Highscore : {score}";
            highScoreEnd.text = $"Highscore : {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
        }
    }

    internal void SetFinalScore()
    {
        textScoreFinal.text = score.ToString();
    }

    internal void ResetScore()
    {
        score = 0;
        textScore.text = score.ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScore.text = $"Highscore : {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
        highScoreEnd.text = $"Highscore : {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
    }
}
