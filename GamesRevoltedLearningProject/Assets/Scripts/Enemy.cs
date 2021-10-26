using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemySO enemySO;
    private Color hitColor = Color.red;
    private Color baseColor = Color.black;

    float health;

    Rigidbody rigidbody;
    Renderer renderer;
    Animator animator;

    private float hitColorChangeTimer = 0.5f;
    private float enemyDeathFallTimer = 5.0f;

    public delegate void OnHealthChange();
    public static event OnHealthChange HealthDrop;

    


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        health = enemySO.health;
        renderer.material.color = baseColor;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        
       
        if (health <= 0f)
        {
            
            StartCoroutine(EnemyDeath());
            Debug.Log("Enemy died!!");
            

        }
        if(health > 0f)
        {
            StartCoroutine(ChangeTargetColorOnHit());
        }
    }

    private IEnumerator EnemyDeath()
    {
        animator.enabled = false;
        rigidbody.useGravity = true;
        yield return new WaitForSeconds(enemyDeathFallTimer);
        GameObject.Destroy(gameObject);
    }

    public IEnumerator ChangeTargetColorOnHit()
    {
        renderer.material.color = hitColor;
        yield return new WaitForSeconds(hitColorChangeTimer);
        renderer.material.color = baseColor;
    }
     
    
    
}
