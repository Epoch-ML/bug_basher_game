using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class TriggerEvent : UnityEvent { }

public class UniversalTimer : MonoBehaviour
{
    public bool canTrigger;
    public bool triggerOnStart = false;
    public TriggerEvent triggerEvent;

    [Header("Timer Settings")]
    public float timerMinSeconds;
    public float timerMaxSeconds;
    public float nextTriggerTime;
    public float currentTime;

    void Start()
    {
        if (triggerOnStart)
        {
            Trigger();
        }
        nextTriggerTime = RandomTriggerTime();
    }

    void Update()
    {
        if (canTrigger)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= nextTriggerTime)
            {
                Trigger();
                currentTime = 0;
                nextTriggerTime = RandomTriggerTime();
            }
        }
    }

    public void Trigger()
    {
        triggerEvent.Invoke();
    }

    public float RandomTriggerTime()
    {
        float random = Random.Range(timerMinSeconds, timerMaxSeconds);
        return random;
    }
}