using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteractMe;

    public void Interact(Transform _sender)
    {
        Debug.Log(_sender.name + "is interacting with" + gameObject.name);
        if (_sender.CompareTag("Player"))
        {
            GameManager.instance.SetMissionState(true);
            OnInteractMe?.Invoke();
        }
    }
}
