using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        score = 0;
        
    }

    public void AddScore(int count)
    {
        score += count;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
