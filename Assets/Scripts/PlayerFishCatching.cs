using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerFishCatching : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] public Transform shootPointHolder;
    [SerializeField] GameObject bulletNetPrefab;
    [SerializeField] int netAmmoCount = 3;
    [SerializeField] float netShootSpeed = 5;

    Camera camz;
    Vector2 dir;


    [Header("For Detecting Objective")]
    [SerializeField] private LayerMask objectiveLayer;
    [SerializeField] private float detectObjRadius;

    [Space]
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

        if (Input.GetMouseButtonDown(0))
        {
            ShootNet();
        }

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

    void ShootNet()
    {
        if (CheckForObjectiveNear() == true)
        {
            if (netAmmoCount > 0)
            {
                Vector2 myDir = dir;
                GameObject projectile = Instantiate(bulletNetPrefab, shootPoint.position, shootPoint.rotation);
                projectile.GetComponent<Rigidbody2D>().AddForce(dir * netShootSpeed, ForceMode2D.Impulse);
                netAmmoCount--;
            }
        }
    }

    public bool CheckForObjectiveNear()
    {
        return Physics2D.OverlapCircle(transform.position, detectObjRadius, objectiveLayer);
    }





}
