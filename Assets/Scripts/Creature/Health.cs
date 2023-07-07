using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	public event Action OnCreatureDeath;

	[Header("Health params")]
	[SerializeField] private float value;
	[SerializeField] private float maxValue;
	[SerializeField] private float regenerationValue;
	[SerializeField] private float regenerationCooldown;

	private float regenerationTimer;

	private void Update()
	{
		if (regenerationTimer <= 0)
		{
			Regenerate(regenerationValue);
			regenerationTimer = regenerationCooldown;
		}
		regenerationTimer -= Time.deltaTime;
	}

	public void Regenerate(float regenerationValue)
	{
		value += regenerationValue;
		if (value > maxValue)
		{
			value = maxValue;
		}
	}

	public void ApplyDamage(float damageValue)
	{
		value -= damageValue;
		if (value <= 0)
		{
			OnCreatureDeath?.Invoke();
		}
	}

	public bool IsAlive => value > 0;

}