using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

	[SerializeField] protected float Damage;
	[SerializeField] protected float timeBetweenAttacks;

	public abstract float Attack();


}