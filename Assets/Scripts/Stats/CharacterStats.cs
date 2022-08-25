using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDestructible
{
    public Stat maxHealth;
    public Stat CurrentHealth { get; private set; }

    public Stat Damage;
    public Stat Armor;

    [HideInInspector]
    public float attackCooldown;

    public bool isInvincible = false;
    public bool isDead = false;

    public event Action OnHealthValueChanged;

    protected virtual void Start()
    {
        CurrentHealth = new Stat();
        CurrentHealth.SetValue(maxHealth.GetValue());
    }

    protected virtual void Update()
    {

    }

    public void TakeDamage(GameObject attacker, float damage)
    {
        if (damage <= 0f)
            return;
        CurrentHealth.SetValue(CurrentHealth.GetValue() - damage);

        OnHealthValueChanged?.Invoke();
    }

    public void OnDestruction(GameObject destroyer)
    {
        isDead = true;
    }
}