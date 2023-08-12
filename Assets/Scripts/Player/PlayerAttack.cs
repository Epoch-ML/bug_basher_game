using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerController2D playerController2D;
    private PlatformerAnimation2D platformerAnimation2D;
    private PlatformerMotor2D _motor;

    public KeyCode primaryAttackKey;
    public KeyCode alternativeAttackKey;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 1;
    private float nextAttackTime = 0;
    public LayerMask enemyLayer;

    public GameObject hitParticles;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController2D = GetComponentInParent<PlayerController2D>();
        platformerAnimation2D = GetComponentInParent<PlatformerAnimation2D>();
        _motor = GetComponentInParent<PlatformerMotor2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= nextAttackTime)
        {
            if (UnityEngine.Input.GetKeyDown(primaryAttackKey) || UnityEngine.Input.GetKeyDown(alternativeAttackKey))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void Attack()
    {

        LockPlayer();

        if (!_motor.IsGrounded())
        {
            animator.Play("AerialAttack");
        }
        else
        {
            animator.Play("Attack");
        }



        // SpawnEffect(transform.position);
        // StartCoroutine(UnlockPlayerIn(1));
    }

    public void HitFrame()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.swing);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<IDamageable>().Damage(1);
            SpawnHitParticles(enemy.transform.position);
        }
    }

    private void SpawnHitParticles(Vector3 pos)
    {
        GameObject spawnedParticle = Instantiate(hitParticles, pos, Quaternion.identity);
        Destroy(spawnedParticle, 2f);
    }

    private IEnumerator UnlockPlayerIn(float duration)
    {
        yield return new WaitForSeconds(duration);
        UnlockPlayer();
    }

    public void LockPlayer()
    {
        playerController2D.enabled = false;
        platformerAnimation2D.enabled = false;
        _motor.velocity = Vector2.zero;
        _motor.frozen = true;
    }

    public void UnlockPlayer()
    {
        playerController2D.enabled = true;
        platformerAnimation2D.enabled = true;
        animator.Play("Fall");
        _motor.frozen = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
