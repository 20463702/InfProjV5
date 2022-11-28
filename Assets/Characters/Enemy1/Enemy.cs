using System;
using  System.Collections;
using System.Collections.Generic;
using Characters;
using Characters.PlayerCharacter;
using UnityEngine;

public class Enemy : Character
{
    public float damage;
    public PlayerHealth pHealth;
    public GameObject enemy1;
    public Transform respawn;

    private new void Start()
    {
        MaxHealth = health;
    }
    
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //        pHealth.health -= damage;
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.collider.gameObject.TryGetComponent(out Character c);
        c.health -= damage;
    }

    public void Damage(int attackDamage)
    {
        health -= damage;

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        enemy1.transform.position = respawn.position;  
        health = MaxHealth;
    }
}
