using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boat : MonoBehaviour, IInteractable
{

    public UnityEvent OnInteractMe;
    
    public void Interact(Transform _sender)
    {
        Debug.Log(_sender.name + "is interacting with" + gameObject.name);
        if (GameManager.instance.MissionComplete == true)
        {
            GameManager.instance.SetGameState(GameState.GameOver);            
            GameManager.instance.SetGameEndType(GameEndType.MissionCompleteBoat);
            GameManager.instance.GameEnd();
        } else {
            if (Player.instance.PlayerAirTank.currentAir < (Player.instance.PlayerAirTank.maxAir/2))
            {
                Player.instance.PlayerAirTank.ResetAirTank();
            }
        }
    }
}
