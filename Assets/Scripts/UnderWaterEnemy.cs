using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class UnderWaterEnemy : MonoBehaviour
{
    public EnemyState state;
    public EnemyType enemyType;
    IAstarAI ai;

    [Space]
    [SerializeField] float roamRange;
    [SerializeField] float targetRange = 5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float damage;


    Vector3 roamingPosition;
    Vector3 startPosition;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] BoxCollider2D waterCol;

    [Space]
    [SerializeField] GameObject deathParticleFx;
    [SerializeField] float knockBackStrength = 20;

    Rigidbody2D rb2D;

    int roamerLife = 3;
    [SerializeField] ParticleSystem roamerAttackFx;

    bool isAttackFxPlaying = false;

    private void Awake()
    {

        waterCol = GameObject.Find("Water").GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        state = EnemyState.Roaming;
        startPosition = transform.position;
        ai = GetComponent<IAstarAI>();
        roamingPosition = GetRoamingPosition();
        ai.destination = roamingPosition;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.IsGameOver() == true || GameManager.instance.IsGamePaused() == true)
        {
            return;
        }
        switch (state)
        {
            case EnemyState.Roaming:
                Roam();
                if (Vector3.Distance(transform.position, Player.instance.transform.position) < targetRange)
                {
                    state = EnemyState.Chasing;
                }
                break;

            case EnemyState.Chasing:
                Chase();
                if (Vector3.Distance(transform.position, Player.instance.transform.position) > targetRange)
                {
                    state = EnemyState.Roaming;
                }
                break;
        }


    }

    Vector3 GetRoamingPosition()
    {
        const int maxAttempts = 100;
        int attempts = 0;
        Vector3 genPos;

        do
        {
            genPos = startPosition + Utilities.GetRandomDir() * UnityEngine.Random.Range(-roamRange, roamRange);
            attempts++;

            // Debugging statements
            // Debug.Log($"Attempt {attempts}: genPos = {genPos}, inCollider = {IsWithinWaterCollider(genPos)}");

            if (attempts >= maxAttempts)
            {
                // Debug.LogWarning("Failed to find a valid roaming position within 100 attempts.");
                break;
            }
        } while (!IsWithinWaterCollider(genPos));

        return genPos;
    }

    private void Attack()
    {
        ai.isStopped = true;
        Collider2D col = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (col.CompareTag("Player"))
        {
            Player.instance.ReduceHealthBy(damage);
            if (enemyType == EnemyType.Kamizaze)
            {
                Die();
            }
            else
            {
                if (!isAttackFxPlaying)
                {
                    roamerAttackFx.Play();
                    isAttackFxPlaying = true;
                }
            }
        }
    }

    private void Chase()
    {
        ai.isStopped = false;
        ai.destination = Player.instance.transform.position;
        ai.SearchPath();
        if (Vector3.Distance(transform.position, Player.instance.transform.position) < attackRange)
        {
            Attack();
        }
        else
        {
            if (enemyType == EnemyType.Roamer)
            {
                roamerAttackFx.Stop();
                isAttackFxPlaying = false;

            }
        }
    }

    private void Roam()
    {
        ai.isStopped = false;
        ai.destination = roamingPosition;
        ai.SearchPath();

        if (ai.reachedDestination)
        {
            roamingPosition = GetRoamingPosition();
        }
        else
        {
            ai.SearchPath();
        }
    }

    bool IsWithinWaterCollider(Vector3 position)
    {
        return waterCol.OverlapPoint(position);
    }

    public void OnHit(GameObject sender)
    {
        if (enemyType == EnemyType.Kamizaze)
        {
            Die();
        }
        else
        {
            if (roamerLife <= 0)
            {
                Die();
            }
            else
            {
                StopAllCoroutines();
                Vector2 dir = (transform.position - sender.transform.position).normalized;
                rb2D.AddForce(-dir * knockBackStrength, ForceMode2D.Impulse);
                StartCoroutine(ResetKnockback());
                roamerLife--;
            }
        }

    }

    IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.15f);
        rb2D.linearVelocity = Vector3.zero;
    }

    public void Die()
    {
        GameObject _ = Instantiate(deathParticleFx, transform.position, Quaternion.identity);
        Destroy(_, 2);
        Destroy(gameObject);
    }
}

public enum EnemyState { Roaming, Chasing }
public enum EnemyType { Roamer, Kamizaze }
//Roamer walks around and is defaeated by knocking it backj
//kamikaze attcks you once and deducts health on hit
