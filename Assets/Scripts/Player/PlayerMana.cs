using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public float CurrentMana {  get; private set; }

    private void Start()
    {
        ResetMana();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(1f);
        }
    }

    public void UseMana(float amount)
    {
        
        stats.Mana = Mathf.Max(stats.Mana -= amount, 0f);
        CurrentMana = stats.Mana;
        
    }

    public void RecoverMana(float amount)
    {
        stats.Mana += amount;
        if (stats.Mana > stats.MaxMana)
        {
            stats.Mana = stats.MaxMana;
        }
    }

    public bool CanRecoverMana()
    {
        return stats.Mana > 0f && stats.Mana < stats.MaxMana;
    }

    public void ResetMana()
    {
        CurrentMana = stats.MaxMana;
    }
}
