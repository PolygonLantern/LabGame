using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 150;
    private int currentHealth;
    public int maxDamage = 15;
    private Animator _animator;
    void Start()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void DealDamage(int damage)
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        //hurt animation
        _animator.SetTrigger("takeDamage");

        //check if we've died
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, 0f);
    }
}
