using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public MagicWand magicWand;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float movementSpeed;

    [SerializeField] private int currentMagicIndex;
    private GameObject currentMagic => availableMagic[currentMagicIndex];

    [field: SerializeField] public List<GameObject> availableMagic { get; protected set; }

    [SerializeField] private float invincibleTime;
    [SerializeField] private bool isInvincible;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillImage;

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
        currentMagicIndex = 0;
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Enemy enemy))
            GetDamage(enemy.Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            Debug.Log("Попал");
            GetDamage(projectile.Damage);
        }
    }

    private void CheckInput()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        UpdateAnimation();

        CheckMagicChange();
        if (Input.GetButtonUp("Fire1"))
            magicWand.Shoot(currentMagic);
    }

    private void CheckMagicChange()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            SwitchCurrentMagic(true);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            SwitchCurrentMagic(false);
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchCurrentMagic(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) && availableMagic.Count >= 2)
            SwitchCurrentMagic(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) && availableMagic.Count >= 3)
            SwitchCurrentMagic(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4) && availableMagic.Count >= 4)
            SwitchCurrentMagic(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5) && availableMagic.Count >= 4)
            SwitchCurrentMagic(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6) && availableMagic.Count >= 4)
            SwitchCurrentMagic(5);
    }

    private void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
    }

    private void SwitchCurrentMagic(bool isNextMagic)
    {
        currentMagicIndex = isNextMagic ? currentMagicIndex + 1 : currentMagicIndex - 1;
        if (currentMagicIndex >= availableMagic.Count)
            currentMagicIndex = 0;
        else if (currentMagicIndex < 0)
            currentMagicIndex = availableMagic.Count - 1;
        EventManager.SendChangeMagic(currentMagicIndex);
    }

    private void SwitchCurrentMagic(int magicIndex)
    {
        currentMagicIndex = magicIndex;
        EventManager.SendChangeMagic(currentMagicIndex);
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
        healthBar.value = Mathf.Lerp(healthBar.value, currentHealth / maxHealth, 0.3f);
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
        Destroy(gameObject);
    }
}