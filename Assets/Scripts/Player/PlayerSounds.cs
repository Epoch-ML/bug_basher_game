using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlatformerMotor2D _motor;
    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<PlatformerMotor2D>();

        _motor.onJump += OnJump;
        _motor.onLanded += OnLanded;

    }

    private void OnJump()
    {
        SoundManager.instance.PlayRandomFromList(SoundManager.instance.jumpingSounds);
    }

    private void OnLanded()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.land);
    }
}
