using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelleAttack : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] Animation _animation;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameOver() == true || GameManager.instance.IsGamePaused() == true)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        _animation.Play();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D col in collider2Ds)
        {
            if (col.TryGetComponent(out UnderWaterEnemy underWaterEnemy))
            {
                underWaterEnemy.OnHit(gameObject);
            }
        }
    }
}
