using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PC2D;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float currentHealth;
    public float maxHealth;

    public AnimalType animalType;

    public SpriteRenderer sr;
    public Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
    }

    public void Attack(GameObject player)
    {
        StartCoroutine(AttackRoutine(player));
    }

    private IEnumerator AttackRoutine(GameObject player)
    {
        GetComponent<SimpleAI>().enabled = false;
        GetComponent<PlatformerAnimation2D>().enabled = false;

        transform.DOMove(player.transform.position, 0.5f).SetEase(Ease.OutSine);
        animator.Play("Backstab");
        player.GetComponent<IDamageable>().Damage(1);

        yield return new WaitForSeconds(0.5f);
        GetComponent<SimpleAI>().enabled = true;
        GetComponent<PlatformerAnimation2D>().enabled = true;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            StartCoroutine(HitRoutine());
        }
    }

    private IEnumerator HitRoutine()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.hit);

        GetComponent<SimpleAI>().enabled = false;
        GetComponent<PlatformerAnimation2D>().enabled = false;
        GetComponent<PlatformerMotor2D>().frozen = true;

        animator.Play("Hit");

        yield return new WaitForSeconds(0.35f);
        GetComponent<SimpleAI>().enabled = true;
        GetComponent<PlatformerAnimation2D>().enabled = true;
        GetComponent<PlatformerMotor2D>().frozen = false;
    }

    private void Die()
    {
        animator.Play("Death");
        sr.DOFade(0, 1);
        SoundManager.instance.PlayRandomFromList(SoundManager.instance.popSounds);

        int randomScore = Random.Range(5, 15);
        ScoreManager.instance.AddScore(randomScore);

        float randomEnergy = Random.Range(10, 20);
        HammerEnergy.instance.AddEnergy(randomEnergy);

        GetComponent<SimpleAI>().enabled = false;
        GetComponent<PlatformerAnimation2D>().enabled = false;
        GetComponent<PlatformerMotor2D>().frozen = true;

        if (TryGetComponent<Collider2D>(out Collider2D collider2D))
        {
            collider2D.enabled = false;
        }


        Destroy(gameObject, 1.2f);
    }

}

public enum AnimalType
{
    Bear,
    Wolf
}