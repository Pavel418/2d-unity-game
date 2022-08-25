using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private CharacterStats stats;

    void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        stats.TakeDamage(attacker, attack.Damage);

        if (stats.CurrentHealth.GetValue() <= 0)
        {
            IDestructible[] destructibles = GetComponents<IDestructible>();
            foreach (IDestructible destructible in destructibles)
            {
                destructible.OnDestruction(attacker);
            }
        }
    }
}