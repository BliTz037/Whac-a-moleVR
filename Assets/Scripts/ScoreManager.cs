using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreGameOverText;
    public TextMeshProUGUI lifeText;

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        scoreGameOverText.text = score.ToString();
    }

    public void UpdateLife(int life)
    {
        lifeText.text = life.ToString();
    }
}
