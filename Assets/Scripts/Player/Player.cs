using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    [Header("Test")]
    public ItemHealthPotion HealthPotion;
    public ItemManaPotion ManaPotion;

    public PlayerStats Stats => stats;
    public PlayerMana PlayerMana { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }

    public PlayerAttack PlayerAttack { get; private set; }

    PlayerAnimations animations;

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
        PlayerMana = GetComponent<PlayerMana>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (HealthPotion.UseItem())
            {
                Debug.Log("ü�������� ���Ҵ�.");
            }

            if (ManaPotion.UseItem())
            {
                Debug.Log("���������� ���Ҵ�.");
            }
        }
        
    }

    public void ResetPlayer()
    {
        stats.ResetPlayer();
        // Reset Animation
        animations.ResetPlayer();
        PlayerMana.ResetMana();
    }
}
