using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwordOfExcelsior : MonoBehaviour
{
    public bool isSwordActive;
    public float buffDuration = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickSword();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            transform.Find("Particles").gameObject.SetActive(false);
        }
    }

    private void PickSword()
    {
        if (!isSwordActive)
        {
            isSwordActive = true;
            StartCoroutine(PickSwordRoutine());
        }
    }

    private IEnumerator PickSwordRoutine()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject swordFill = GameObject.Find("Sword-Fill");
        GameObject swordText = GameObject.Find("SwordText");

        SwordSpawner.instance.SwordUsed();

        SoundManager.instance.PlaySfx(SoundManager.instance.swordPickUp);

        swordFill.GetComponentInParent<CanvasGroup>().DOFade(1, 0.25f);
        swordFill.GetComponent<Image>().fillAmount = 1;
        swordFill.GetComponent<Image>().DOFillAmount(0, buffDuration).SetEase(Ease.Linear);

        swordText.GetComponent<CanvasGroup>().alpha = 1;

        HammerEnergy.instance.StopDrain();
        HammerEnergy.instance.hammerEnergy = 100;
        HammerEnergy.instance.UpdateUI();

        player.transform.Find("Visual").GetComponent<SpriteRenderer>().material.SetFloat("_IsActive", 1);
        player.transform.Find("Visual").GetComponent<Animator>().SetFloat("excelsior", 2);
        player.transform.Find("Visual").GetComponent<PlayerAttack>().attackRate = 3;
        player.transform.Find("Visual").Find("SwordParticles").GetComponent<ParticleSystem>().Play();

        player.GetComponent<PlatformerMotor2D>().groundSpeed *= 1.5f;
        player.GetComponent<PlatformerMotor2D>().airSpeed *= 1.5f;

        yield return new WaitForSeconds(buffDuration);

        swordFill.GetComponentInParent<CanvasGroup>().DOFade(0, 0.25f);

        swordText.GetComponent<CanvasGroup>().alpha = 0;

        HammerEnergy.instance.ResumeDrain();

        player.transform.Find("Visual").GetComponent<SpriteRenderer>().material.SetFloat("_IsActive", 0);
        player.transform.Find("Visual").GetComponent<Animator>().SetFloat("excelsior", 1);
        player.transform.Find("Visual").GetComponent<PlayerAttack>().attackRate = 1;
        player.transform.Find("Visual").Find("SwordParticles").GetComponent<ParticleSystem>().Stop();

        player.GetComponent<PlatformerMotor2D>().groundSpeed /= 1.5f;
        player.GetComponent<PlatformerMotor2D>().airSpeed /= 1.5f;
    }
}
