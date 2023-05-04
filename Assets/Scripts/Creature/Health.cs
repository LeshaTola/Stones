using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField] private float value;
	//[SerializeField] private float regenerationValue;
	[SerializeField] private float regenerationCooldown;

	public void Regenerate(float regenerationValue) {
		value += regenerationValue;
	}

	public void ApplyDamage(float damageValue) {
		value -= damageValue;
	}

	public bool IsAlive => value > 0;

}