using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; }
    [SerializeField] protected float currentHealth;
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] protected Slider healthBar;
    protected Rigidbody2D enemyRigidbody;
    private DebuffController debuffController;

    protected virtual void Start()
    {
        InitializeElements();
    }

    public virtual void GetDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        if (currentHealth < 0)
            Die();
        healthBar.value = GetSliderFill();
    }

    private float GetSliderFill()
    {
        return currentHealth / MaxHealth;
    }

    public virtual void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public virtual void SetDamage(float damage)
    {
        Damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Magic magic))
            CollideWithMagic(magic);
    }

    protected virtual void CollideWithMagic(Magic magic)
    {
        GetDamage(magic.Damage);
        ApplyDebuffFromMagic(magic);
    }

    protected virtual void InitializeElements()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        debuffController = GameObject.FindWithTag("DebuffController").GetComponent<DebuffController>();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private void ApplyDebuffFromMagic(Magic magic)
    {
        if (transform.childCount != 0)
        {
            Debug.Log("Зашёл");
            if (debuffController.TryMixDebuffs(transform.GetChild(0).gameObject, magic.SuperimposedDebuff,
                    out GameObject mixedDebuff))
            {
                Debug.Log("Сочетание произошло");
                Destroy(transform.GetChild(0).gameObject);
                mixedDebuff.transform.SetParent(gameObject.transform);
                mixedDebuff.GetComponent<Debuff>().Activate(this);
            }
        }
        else
        {
            var imposedDebuff = Instantiate(magic.SuperimposedDebuff, transform.position, Quaternion.identity);
            imposedDebuff.transform.SetParent(gameObject.transform);
            imposedDebuff.GetComponent<Debuff>().Activate(this);
        }
    }
}