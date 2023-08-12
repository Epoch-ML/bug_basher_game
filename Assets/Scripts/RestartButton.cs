using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class RestartButton : MonoBehaviour
{
    public TransitionSettings transition;

    public void RestartScene()
    {
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transition, 0);
        SoundManager.instance.PlaySfx(SoundManager.instance.confirm);
    }

    public void PlayButton()
    {
        TransitionManager.Instance().Transition("Game", transition, 0);
        SoundManager.instance.PlaySfx(SoundManager.instance.confirm);
    }
}
