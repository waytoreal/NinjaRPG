using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private PlayerAnimations palyerAnimations;
    public bool PlayerHasHealth;

    private void Awake()
    {
        palyerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (stats.Health <=0f)
        {
            PlayerDead();
        }
    }

    public void TakeDamage(float amount)
    {
        if (stats.Health <= 0f) return;
        stats.Health -= amount;
        DamageManager.Instance.ShowDamageText(amount, this.transform);
        if (stats.Health <= 0)
        {
            PlayerDead();
            stats.Health = 0f;
        }
    }

    public void RestoreHealth(float amount)
    {
        stats.Health += amount;
        if (stats.Health > stats.MaxHealth)
        {
            stats.Health = stats.MaxHealth;
        }
    }

    public bool CanRestoreHealth()
    {
        return stats.Health > 0f && stats.Health < stats.MaxHealth;
    }

    private void PlayerDead()
    {
        palyerAnimations.SetDeadAnimation();
    }
}
