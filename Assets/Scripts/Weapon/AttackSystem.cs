using System;
using UnityEngine;



public class AttackSystem : MonoBehaviour
{

    public event Action OnAttack;

    [SerializeField] private float attackCooldown;
    [SerializeField] private SpellSO defaultMeleeAttack;


    private float attackTimer;
    private void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    

    public void Attack(Player player)
    {
        float realDamage;

        if (attackTimer <= 0)
        {
            realDamage = CalculateRealDamage(player);
            OnAttack?.Invoke();
            attackTimer = attackCooldown + defaultMeleeAttack.Cooldown;
        }
    }
    private float CalculateRealDamage(Player player)
    {
        float stat;
        switch (player.Weapon.WeaponStat.AttackType)
        {
            case AttackTypeEnum.Melee:
                stat = player.Strength;
                break;
            case AttackTypeEnum.Range:
                stat = player.Agility;
                break;
            case AttackTypeEnum.Magic:
                stat = player.Intelligence;
                break;
            default:
                throw new System.Exception("Couldn`t get attack type!");
        }


        switch (player.Weapon.WeaponStat.AttackType)
        {
            case AttackTypeEnum.Melee:
            case AttackTypeEnum.Range:
                return (player.Weapon.WeaponStat.Damage + 1) * defaultMeleeAttack.DamagePercent + stat * (float)player.Weapon.WeaponStat.Rarity/100; 
            case AttackTypeEnum.Magic:
                return (player.Weapon.WeaponStat.Intelligence) * defaultMeleeAttack.DamagePercent + stat * (float)player.Weapon.WeaponStat.Rarity / 100;
            default:
                throw new System.Exception("Couldn`t calculate damage!");
        }
    }
}
