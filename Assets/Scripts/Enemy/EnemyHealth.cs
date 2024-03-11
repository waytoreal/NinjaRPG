using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event UnityAction OnEnemyDeadEvent;

    [Header("Config")]
    [SerializeField] private float health;

    public float CurrentHealth { get; private set; }

    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
    }

    private void Start()
    {
        CurrentHealth = health;   
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            animator.SetTrigger("Dead");
            enemyBrain.enabled = false;
            enemySelector.NoSelectionCallback();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            OnEnemyDeadEvent?.Invoke();
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }
}
