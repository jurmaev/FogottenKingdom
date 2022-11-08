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
    [SerializeField] private GameObject currentMagic;
    [SerializeField] private List<GameObject> availableMagic;
    [SerializeField] private float movementSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        currentMagic = availableMagic[0];
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            SwitchCurrentMagic(true);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            SwitchCurrentMagic(false);
        if (Input.GetButtonUp("Fire1"))
            magicWand.Shoot(currentMagic);
    }

    private void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
    }

    private void SwitchCurrentMagic(bool isNextMagic)
    {
        var currentMagicIndex = availableMagic.FindIndex(x => x);
        currentMagic = isNextMagic
            ? availableMagic[Mathf.Min(currentMagicIndex + 1, availableMagic.Count - 1)]
            : availableMagic[Mathf.Max(currentMagicIndex - 1, 0)];
    }
}