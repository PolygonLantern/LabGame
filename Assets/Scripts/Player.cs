using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 150;
    public  int maxDamage = 15;
    private int _currentHealth;
    private Animator _animator;  
    public Transform attackPoint;
    public float range = .5f;
    [SerializeField] private LayerMask enemyLayers;

    private void Start()
    {
        _currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void DealDamage(int damage)
    {
        //collision detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
      
        //loop through the enemies
        foreach (var enemy in hitEnemies)
        {
            //deal the damage
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        //animation
        _animator.SetTrigger("takeDamage");
        //check if we are dead
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, 0f);
    
    }
}
