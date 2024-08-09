using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerGetObjective : MonoBehaviour
{
    [SerializeField] public Transform shootPointHolder;

    Camera camz;
    Vector2 dir;

    [Header("For Interacting")]
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float detectInteractableRadius;

    void Start()
    {
        camz = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = camz.ScreenToWorldPoint(Input.mousePosition);
        dir = (mousePos - (Vector2)shootPointHolder.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        shootPointHolder.eulerAngles = new Vector3(0, 0, angle);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, detectInteractableRadius, interactLayer);
        foreach (Collider2D col in collider2Ds)
        {
            if (col.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(transform);
            }
        }
    }


}
