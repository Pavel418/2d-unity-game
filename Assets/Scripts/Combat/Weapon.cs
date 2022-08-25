using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float Cooldown;

    public float minDamage;
    public float maxDamage;
    public float criticalMultiplier;
    public float criticalChance;
    public float Range;

    public WeaponType type;

    public GameObject ProjectileToFire;
    public float ProjectileSpeed;

    public Attack CreateAttack(CharacterStats attacker, CharacterStats defender)
    {
        float baseDamage = attacker.Damage.GetValue();

        baseDamage += Random.Range(minDamage, maxDamage);

        bool isCritical = Random.value < criticalChance;
        if (isCritical)
            baseDamage *= criticalMultiplier;

        if (defender != null)
            baseDamage -= defender.Armor.GetValue();

        if (baseDamage < 0)
            baseDamage = 0;
        return new Attack(baseDamage, isCritical);
    }

    public void ExecuteAttack(GameObject attacker, Vector2 startSpot, Quaternion target)
    {
        GameObject projectile = Instantiate(ProjectileToFire, startSpot, target);
        Projectile fire = projectile.GetComponent<Projectile>();
        fire.Speed = ProjectileSpeed;
        fire.Shooter = attacker;
        fire.ProjectileCollided += OnProjectileCollided;
        projectile.SetActive(true);
    }

    private void OnProjectileCollided(GameObject attacker, GameObject defender)
    {
        if (attacker == null || defender == null)
            return;

        var attackerStats = attacker.GetComponent<CharacterStats>();
        var defenderStats = defender.GetComponent<CharacterStats>();

        if (defenderStats == null)
            return;

        var attack = CreateAttack(attackerStats, defenderStats);

        var attackables = defender.GetComponentsInChildren(typeof(IAttackable));

        foreach (IAttackable attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }

    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (defender == null)
            return;

        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > Range)
        {
            return;
        }

        OnProjectileCollided(attacker, defender);
    }
}
public enum WeaponType { LongRange, LowRange }