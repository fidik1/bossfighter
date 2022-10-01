using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private RoundController roundController;

    [SerializeField] private EntityStats entityStats;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Loot loot;

    [SerializeField] protected Sprite spriteHit;
    protected Sprite sprite;
    public float maxHealthPoint { get; protected set; }
    public float healthPoint { get; protected set; }
    public float damage { get; protected set; }
    public float attackSpeed { get; protected set; }
    public float speed { get; protected set; }
    public float jumpForce { get; protected set; }
    public float maxJumps { get; protected set; }

    public bool isAlive = true;
    public bool isGrounded;
    public bool isBoss;

    public List<ParticleSystem> particleDeath;
    private int currentParticle = 0;

    public delegate void EnemyEvent();
    public EnemyEvent ChangedHP;

    private void Awake()
    {
        ChangedHP += CheckDeath;

        sprite = entityStats.sprite;
        spriteRenderer.sprite = sprite;

        maxHealthPoint = entityStats.maxHealthPoint;
        healthPoint = maxHealthPoint;
        damage = entityStats.damage;
        attackSpeed = entityStats.attackSpeed;
        speed = entityStats.speed;
        jumpForce = entityStats.jumpForce;
        maxJumps = entityStats.maxJumps;
    }

    private void CheckDeath()
    {
        if (healthPoint <= 0)
            Death();
    }

    private void Death()
    {
        healthPoint = 0;
        isAlive = false;

        Destroy(gameObject);
        particleDeath[currentParticle].transform.position = transform.position;
        particleDeath[currentParticle].Play();
        _ = currentParticle < particleDeath.Count -1 ? currentParticle++ : currentParticle = 0;

        loot.Drop(transform.position, isBoss);
        roundController.RemoveEnemy(gameObject);
    }

    public void SpendHP(float HP)
    {
        healthPoint -= HP;
        ChangedHP?.Invoke();
    }

    public void ChangeSpeed(float movementSpeed)
    {
        speed = movementSpeed;
    }

    private void OnDestroy()
    {
        ChangedHP -= CheckDeath;
    }
}
