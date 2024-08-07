using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class UnderWaterEnemy : MonoBehaviour
{
    public EnemyState state;
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


    void Start()
    {
        state = EnemyState.Roaming;
        startPosition = transform.position;
        ai = GetComponent<IAstarAI>();
        roamingPosition = GetRoamingPosition();
        ai.destination = roamingPosition;
        // waterCol = GameObject.Find("Water").GetComponent<BoxCollider2D>();
        // waterCol = GameObject.FindGameObjectWithTag("Water").GetComponent<BoxCollider2D>();
    }

    void Update()
    {
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
            Destroy(gameObject);
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
}

public enum EnemyState { Roaming, Chasing }
