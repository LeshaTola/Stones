using UnityEngine;

public class Mana : MonoBehaviour
{
	//[Header("Health params")]
	[SerializeField] private float value;
	//[SerializeField] private float regenerationValue;
	[SerializeField] private float regenerationCooldown;

	private readonly float regenerationTime;

	public void Regenerate(float regenerationValue)
	{
		value += regenerationValue;
	}

	public void SpendMana(float spendManaValue)
	{
		value -= spendManaValue;
	}

	public bool IsEnoughMana(float compareValue)
	{
		return value > compareValue;
	}
}
