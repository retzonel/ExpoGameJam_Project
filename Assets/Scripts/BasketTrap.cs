using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketTrap : MonoBehaviour, IInteractable
{
    public bool caugthObjective = false;

    public void Interact(Transform interactor)
    {
        if (caugthObjective == true)
        {
            Debug.Log("Mission complete!");
            GameManager.instance.SetMissionState(true);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Objective"))
        {
            GameObject objectiveGO = other.gameObject;
            caugthObjective = true;
            Destroy(objectiveGO);
        }

        // if (other.transform.CompareTag("Enemy"))
        // {
        //     GameObject enemyGO = other.gameObject;
        //     Destroy(enemyGO);
        // }
    }
}
