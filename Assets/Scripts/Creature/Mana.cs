using UnityEngine;

public class Mana : MonoBehaviour
{
	[Header("Mana params")]
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

	public void SpendMana(float spendManaValue)
	{
		value -= spendManaValue;
	}

	public bool IsEnoughMana(float compareValue)
	{
		return value >= compareValue;
	}
}
