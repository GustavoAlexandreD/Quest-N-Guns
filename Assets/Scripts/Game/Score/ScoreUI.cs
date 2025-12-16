using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void updateScore(ScoreController scoreController)
    {
        scoreText.text = $"Score: {scoreController.Score}";
    }
}
