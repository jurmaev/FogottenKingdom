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
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float movementSpeed;

    [SerializeField] private GameObject currentMagic;
    [SerializeField] private List<GameObject> availableMagic;

    [SerializeField] private float invincibleTime;
    [SerializeField] private bool isInvincible;

    private Vector2 moveDirection;
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;


    void Start()
    {
        InitializeElements();
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InitializeElements()
    {
        currentMagic = availableMagic[0];
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Enemy enemy))
            GetDamage(enemy.Damage);
    }

    private void CheckInput()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        UpdateAnimation();
        if (Input.GetButtonUp("Fire1"))
            magicWand.Shoot(currentMagic);
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            SwitchCurrentMagic(true);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            SwitchCurrentMagic(false);
    }

    private void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
    }

    private void SwitchCurrentMagic(bool isNextMagic)
    {
        var currentMagicIndex =
            availableMagic.FindIndex(magic => magic.GetType().ToString() == currentMagic.GetType().ToString());
        currentMagic = isNextMagic
            ? availableMagic[Mathf.Min(currentMagicIndex + 1, availableMagic.Count - 1)]
            : availableMagic[Mathf.Max(currentMagicIndex - 1, 0)];
    }

    private void UpdateAnimation()
    {
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            animator.SetBool("Moving", true);
            if (moveDirection.x > 0)
            {
                spriteRenderer.flipX = false;
                magicWand.MoveToHand(Hand.Right);
            }

            if (moveDirection.x < 0)
            {
                spriteRenderer.flipX = true;
                magicWand.MoveToHand(Hand.Left);
            }
        }
        else
            animator.SetBool("Moving", false);
    }

    private void GetDamage(float amountOfDamage)
    {
        if (!isInvincible)
        {
            currentHealth -= amountOfDamage;
            if (currentHealth <= 0)
                Die();
            StartCoroutine(nameof(BecomeInvincible));
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}