using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Settings")]
    public int score;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ResetScore();
    }

    public void AddScore(int amount)
    {
        score += amount;
        TextEffect();
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }

    private void TextEffect()
    {
        scoreText.transform.DOShakePosition(0.5f, 10);
    }
}
