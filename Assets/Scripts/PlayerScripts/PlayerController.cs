using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
    public MagicWand magicWand;
    [SerializeField] private float movementSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D playerRb;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckInput()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if(Input.GetButtonUp("Fire1"))
            magicWand.Shoot();
    }
    
    private void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
    }
}