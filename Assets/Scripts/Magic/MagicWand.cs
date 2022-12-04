using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Hand
{
    Right,
    Left
}

public class MagicWand : MonoBehaviour
{
    [SerializeField] private int currentMagicIndex;
    private GameObject currentMagic => availableMagic[currentMagicIndex];
    [field: SerializeField] public List<GameObject> availableMagic { get; protected set; }

    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform rightHandPoint;
    [SerializeField] private Transform leftHandPoint;

    private Vector2 mousePos;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
        SwitchCurrentMagic(0);
    }

    private void Update()
    {
        RotateFirepoint();
        CheckInput();
    }

    public void MoveToHand(Hand hand)
    {
        if (hand == Hand.Right)
            transform.position = rightHandPoint.position;
        else if (hand == Hand.Left)
            transform.position = leftHandPoint.position;
    }


    private void CheckInput()
    {
        CheckMagicChange();
        if (Input.GetButton("Fire1"))
            Shoot();
    }

    private void Shoot()
    {
        if (!currentMagic.GetComponent<Magic>().IsCooldown)
        {
            Instantiate(currentMagic, firePoint.position, transform.rotation);
            StartCoroutine(currentMagic.GetComponent<Magic>().StartCountdown());
            EventManager.SendMagicStartCooldown(currentMagicIndex,
                currentMagic.GetComponent<Magic>().CooldownTime);
            currentMagic.GetComponent<Magic>().Damage = 100;
        }
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

    private void RotateFirepoint()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        var lookDirection = mousePos - new Vector2(firePoint.position.x, firePoint.position.y);
        lookDirection.Normalize();
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}