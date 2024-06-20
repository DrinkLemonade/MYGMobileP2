using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIElements;
using System;
public class EnemyScript : MonoBehaviour
{
    public float health;
    public bool isDead = false;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0) isDead = true;
    }

}
