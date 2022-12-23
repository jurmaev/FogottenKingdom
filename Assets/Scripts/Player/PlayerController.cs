using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public MagicWand magicWand;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float movementSpeed;

    [SerializeField] private float invincibleTime;
    [SerializeField] private bool isInvincible;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillImage;

    private Vector2 moveDirection;
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public UnityAction<Enemy> OnEnemyCollision;
    public int Coins { get; private set; }

    void Start()
    {
        InitializeElements();
        EventManager.OnCoinAmountChanged.AddListener(TryChangeCoins);
    }

    void Update()
    {
        CheckInput();
    }

    public void IncreaseMaximumHealth(float amountOfHealth)
    {
        maxHealth += amountOfHealth;
        currentHealth += amountOfHealth;
    }

    public void IncreaseSpeed(float amountSpeed)
    {
        movementSpeed += amountSpeed;
    }

    public void LearnNewMagic(GameObject magic)
    {
        magicWand.availableMagic.Add(magic);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InitializeElements()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void TryChangeCoins(int changeAmount)
    {
        Coins = Coins + changeAmount >= 0 ? Coins + changeAmount : 0;
        EventManager.SendCoinPicked(Coins);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Enemy enemy))
            GetDamage(enemy.Damage);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
            GetDamage(enemy.Damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
            GetDamage(projectile.Damage);
        if (other.gameObject.TryGetComponent(out Artifact artifact)) TryChangeCoins(-artifact.Price);
    }

    private void CheckInput()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        UpdateAnimation();
    }


    private void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
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
            UpdateHealthbar();
            StartCoroutine(nameof(BecomeInvincible));
        }
    }

    private void UpdateHealthbar()
    {
        healthBar.value = currentHealth / maxHealth;
        var healthColor = Color.Lerp(Color.red, Color.green, currentHealth / maxHealth);
        fillImage.color = healthColor;
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        Blink();
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Blink()
    {
        ColorController colorController = GetComponent<ColorController>();
        colorController.MakeBlink(gameObject, 160, invincibleTime, 0.2f);
    }


    private void Die()
    {
        EventManager.SendPlayerDeath();
        Destroy(gameObject);
    }
}