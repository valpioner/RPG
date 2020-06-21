using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();

        // Add ragtoll effect / death animation

        Destroy(gameObject);

        // Add loot
    }
}
