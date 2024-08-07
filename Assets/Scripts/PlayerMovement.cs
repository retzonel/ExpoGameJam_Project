using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    Rigidbody2D RB;

    [Header("Player_Data")]
    [SerializeField] private float maxSpeed = 2;
    [SerializeField] private float acceleration = 50;
    [SerializeField] private float deceleration = 100;
    [SerializeField] private float rotationSpeed = 5;

    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    private float targetAngle;
    private float initialY;

    Player player;

    [SerializeField] private float bobbingFreq;
    [SerializeField] private float boobingAmp;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        initialY = RB.position.y;
    }

    void Update()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (movementInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg - 90f;
        }
        else
        {
            targetAngle = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!player.playerAirTank.IsInWater && movementInput.y > 0)
        {
            movementInput.y = 0;
        }
        
        if (movementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = movementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * maxSpeed * Time.deltaTime;
            BobbingEffect();
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        RB.velocity = oldMovementInput * currentSpeed;

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
        initialY = RB.position.y;
    }

    void BobbingEffect()
    {
        float newY = initialY + Mathf.Sin(Time.time * bobbingFreq) * boobingAmp;
        RB.position = new Vector2(RB.position.x, newY);
    }
}
