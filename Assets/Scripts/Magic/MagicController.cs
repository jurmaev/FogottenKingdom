using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    private Dictionary<GameObject, CooldownTimer> magicCooldown;

    void Start()
    {
        magicCooldown = new Dictionary<GameObject, CooldownTimer>();
    }

    public void InstantiateMagic(GameObject magic, int magicIndex, Vector3 startPosition, Quaternion rotation)
    {
        if (!magicCooldown[magic].IsActive)
        {
            Instantiate(magic, startPosition, rotation);
            magicCooldown[magic].StartCountdown();
            EventManager.SendMagicStartCooldown(magicIndex, magic.GetComponent<Magic>().CooldownTime);
        }
    }

    public void SetMagic(List<GameObject> availableMagic)
    {
        foreach (var magic in availableMagic)
            magicCooldown.Add(magic, new CooldownTimer(magic.GetComponent<Magic>().CooldownTime));
    }

    public class CooldownTimer
    {
        public bool IsActive
        {
            get
            {
                if (startTime == 0)
                    return false;
                return Time.time < startTime + cooldownTime;
            }
        }

        private readonly float cooldownTime;
        private float startTime;

        public CooldownTimer(float cooldownTime)
        {
            this.cooldownTime = cooldownTime;
        }

        public void StartCountdown()
        {
            startTime = Time.time;
        }
    }
}