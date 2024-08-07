using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour, IInteractable
{
    public void Interact(Transform interactor)
    {
        if (GameManager.instance.MissionComplete)
        {
            GameManager.instance.SetGameState(GameState.GameOver);
        } else {
            Debug.Log("msiiosn not complete cannot come to boat");
            if (Player.instance.playerAirTank.currentAir < (Player.instance.playerAirTank.maxAir/2))
            {
                Debug.Log("refilled tank and suplies");
            }
        }
    }
}
