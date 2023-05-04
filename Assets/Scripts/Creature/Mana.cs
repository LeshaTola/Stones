using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
	//[Header("Health params")]
	[SerializeField] private float value;
	//[SerializeField] private float regenerationValue;
	[SerializeField] private float regenerationCooldown;

	private float regenerationTime;

	public void Regenerate(float regenerationValue) {
		value += regenerationValue;
	}

	public void SpendMana(float spendManaValue) {
		value -= spendManaValue;
	}

	public bool IsEnoughMana(float compareValue)=> value > compareValue;

}
