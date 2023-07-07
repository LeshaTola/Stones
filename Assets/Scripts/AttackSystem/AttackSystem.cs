using System;
using System.Collections.Generic;
using UnityEngine;



public class AttackSystem : MonoBehaviour
{
	public event Action OnAttack;

	[SerializeField] private SpellSO defaultMeleeAttack;

	private float attackTimer;
	private SpellSO activeSpell;

	private void Awake()
	{
		activeSpell = defaultMeleeAttack;
	}

	private void Update()
	{
		attackTimer -= Time.deltaTime;
	}

	public void Attack(Player player)
	{
		float realDamage;

		if (attackTimer <= 0)
		{
			if (player.Mana.IsEnoughMana(activeSpell.ManaCost))
			{
				realDamage = CalculateRealDamage(player) * activeSpell.AttackNumber;

				List<Tile> tilesToAttack = GetTilesToAttack(player);
				foreach (Tile tile in tilesToAttack)
				{
					tile.DamageOcupant(player, realDamage);
				}

				OnAttack?.Invoke();
				player.Mana.SpendMana(activeSpell.ManaCost);
				attackTimer = activeSpell.Cooldown;

			}
		}
	}

	private float CalculateRealDamage(Player player)
	{
		float stat = player.Weapon.WeaponStat.AttackType switch
		{
			AttackTypeEnum.Melee => player.Strength,
			AttackTypeEnum.Range => player.Agility,
			AttackTypeEnum.Magic => player.Intelligence,
			_ => throw new System.Exception("Couldn`t get attack type!"),
		};

		return player.Weapon.WeaponStat.AttackType switch
		{
			AttackTypeEnum.Melee or AttackTypeEnum.Range => ((player.Weapon.WeaponStat.Damage + 1) * activeSpell.DamagePercent / 100) + (stat * (float)player.Weapon.WeaponStat.Rarity / 100),
			AttackTypeEnum.Magic => (player.Weapon.WeaponStat.Intelligence * activeSpell.DamagePercent / 100) + (stat * (float)player.Weapon.WeaponStat.Rarity / 100),
			_ => throw new System.Exception("Couldn`t calculate damage!"),
		};
	}

	private List<Tile> GetTilesToAttack(Player player)
	{
		Vector3 curentAttacPosition = transform.position + transform.forward;

		List<Tile> tilesToAttack = new();

		for (int i = 0; i < player.Weapon.WeaponStat.RangeAttack; i++)
		{
			tilesToAttack.Add(player.World.GetTileFromPosition(curentAttacPosition));
			curentAttacPosition += transform.forward;
		}

		return tilesToAttack;
	}

}
