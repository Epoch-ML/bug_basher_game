using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HammerEnergy : MonoBehaviour
{
    public static HammerEnergy instance;

    [Header("Settings")]
    public float hammerEnergy;
    public float drainRate;
    public bool canDrain = true;
    public bool isGameOver = false;

    [Header("UI")]
    public Image hammerImage;

    [Header("References")]
    public UniversalTimer drainRateIncreaseTimer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameManager.OnGameStateChanged += OnStateChanged;
        drainRateIncreaseTimer = GetComponent<UniversalTimer>();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(GameState state)
    {
        if (state == GameState.Game)
        {
            ResumeDrain();
            hammerImage.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            StopDrain();
            hammerImage.transform.parent.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (hammerEnergy <= 0)
            {
                isGameOver = true;
                GameManager.instance.GameOver();
                hammerEnergy = 0;
                canDrain = false;
            }

            if (canDrain)
            {
                Drain();
            }
        }
    }

    private void Drain()
    {
        hammerEnergy -= drainRate * Time.deltaTime;
        UpdateUI();
    }

    public void UpdateUI()
    {
        hammerImage.fillAmount = hammerEnergy / 100;

        if (hammerImage.fillAmount >= 0.75)
        {
            hammerImage.color = Color.white;
        }

        if (hammerImage.fillAmount < 0.65 && hammerImage.fillAmount >= 0.35)
        {
            hammerImage.color = Color.yellow;
        }

        if (hammerImage.fillAmount < 0.35)
        {
            hammerImage.color = Color.red;
        }
    }

    public void AddEnergy(float amount)
    {
        hammerEnergy += amount;

        if (hammerEnergy > 100)
        {
            hammerEnergy = 100;
        }

        UpdateUI();

        hammerImage.transform.parent.transform.DOShakePosition(0.25f, amount, 25);
    }

    public void IncreaseDrainRate(float amount)
    {
        drainRate += amount;
    }

    public void StopDrain()
    {
        canDrain = false;
        drainRateIncreaseTimer.canTrigger = false;
    }

    public void ResumeDrain()
    {
        canDrain = true;
        drainRateIncreaseTimer.canTrigger = true;
    }
}
