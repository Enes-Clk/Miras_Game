// Puan sistemidir, UI Text ile puanı ekranda gösterir
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Skor: " + score;
    }
}