using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EasyTransition;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    public TransitionSettings transition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.PressToStart);
    }

    private void Update()
    {
        if (state == GameState.PressToStart)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                UpdateGameState(GameState.Game);
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.PressToStart:
                HammerEnergy.instance.isGameOver = false;
                SoundManager.instance.ChangeMusic(SoundManager.instance.gameMusic);
                GameObject.Find("Score").GetComponent<CanvasGroup>().alpha = 0;
                break;
            case GameState.Game:
                SoundManager.instance.PlaySfx(SoundManager.instance.gameStart);
                GameObject.Find("Score").GetComponent<CanvasGroup>().DOFade(1, 1);
                GameObject.Find("ClickToStart").GetComponent<Animator>().SetTrigger("fadeout");
                GameObject.Find("Player").GetComponent<PlayerController2D>().enabled = true;
                GameObject.Find("Player").GetComponentInChildren<PlayerAttack>().enabled = true;
                break;
            case GameState.End:
                GameObject.Find("Player").GetComponentInChildren<PlayerAttack>().LockPlayer();
                GameObject.Find("Player").GetComponentInChildren<PlayerAttack>().enabled = false;
                GameObject.Find("Player").GetComponentInChildren<Animator>().enabled = false;

                // Time.timeScale = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void GameOver()
    {
        UpdateGameState(GameState.End);

        SoundManager.instance.PlaySfx(SoundManager.instance.gameEnd);

        GameObject gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverScreen.GetComponent<CanvasGroup>().interactable = true;
        gameOverScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameOverScreen.GetComponent<CanvasGroup>().DOFade(1, 1.5f);

        GameObject.Find("YouScored").GetComponent<TextMeshProUGUI>().text = "you scored: " + ScoreManager.instance.score.ToString();
    }
}

public enum GameState
{
    PressToStart,
    Game,
    End
}
