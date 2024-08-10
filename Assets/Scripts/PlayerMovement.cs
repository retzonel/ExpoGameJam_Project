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
    private int roundedXMovementInput;
    private float targetAngle;
    private float initialY;
    public int FaceingDir { get; private set; }


    Player player;

    [SerializeField] private float bobbingFreq;
    [SerializeField] private float boobingAmp;

    [Space]
    [SerializeField] Transform playerGFX;

    Animator animator;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        initialY = RB.position.y;
        FaceingDir = 1;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.IsGameOver() == true || GameManager.instance.IsGamePaused() == true)
        {
            return;
        }
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (movementInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg - 90f;
        }
        else
        {
            targetAngle = 0;
        }

        if (Mathf.Abs(movementInput.magnitude) > 0) animator.SetBool("isSwimming", true);
        else animator.SetBool("isSwimming", false);

        if (Mathf.Abs(movementInput.x) > 0)
        {
            roundedXMovementInput = (int)(movementInput * Vector2.right).normalized.x;
        }
        else
        {
            roundedXMovementInput = 0;
        }

        CheckIfShouldFlip(roundedXMovementInput);
    }

    private void FixedUpdate()
    {
        if (!player.PlayerAirTank.IsInWater && movementInput.y > 0)
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
            // BobbingEffect();
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        RB.linearVelocity = oldMovementInput * currentSpeed;

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
        RB.SetRotation(newAngle);
        initialY = RB.position.y;
    }

    void BobbingEffect()
    {
        float newY = initialY + Mathf.Sin(Time.time * bobbingFreq) * boobingAmp;
        RB.position = new Vector2(RB.position.x, newY);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FaceingDir)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FaceingDir *= -1;
        playerGFX.Rotate(0, 180, 0);
    }
}
