using System.Collections;
using System.Collections.Generic;
using VRMGames;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    public const int DEFAULT_MOVESPEED = 5;

    //Movement
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 lastMovedVector;

    public bool canDash = true; // Indica si se puede realizar un _dash.
    public bool isDashing = false; // Indica si el jugador est� realizando un _dash.
    public float dashTime = 0.25f; // Duraci�n del _dash.
    public float dashCD = 3f; // Tiempo de reutilizaci�n del _dash.
    public float timer = 0f; // Temporizador para el _dash.
    public float dashPower = 1f; // Fuerza aplicada durante el _dash.

    //References
    Rigidbody2D rb;
    PlayerStats player;

    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); //If we don't do this and game starts up and don't move, the projectile weapon will have no momentum
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        if (canDash && !isDashing && Input.GetKeyDown(KeyCode.Space)) StartCoroutine(Dash());
        Move();
    }

    void InputManagement()
    {
        if(GameManager.instance.isGameOver)
        {
            return;
        }

        float moveX, moveY;
        if (VirtualJoystick.CountActiveInstances() > 0)
        {
            moveX = VirtualJoystick.GetAxisRaw("Horizontal");
            moveY = VirtualJoystick.GetAxisRaw("Vertical");
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }
        

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);    //Last moved X
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);  //Last moved Y
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);    //While moving
        }
    }

    void Move()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        rb.linearVelocity = DEFAULT_MOVESPEED * player.Stats.moveSpeed * moveDir * dashPower;
    }

    private IEnumerator Dash() 
    {
        canDash = false;
        isDashing = true;
        dashPower = 2.5f;
        yield return new WaitForSeconds(dashTime);
        dashPower = 1f;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }
}
