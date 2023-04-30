using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

	[SerializeField] private Projectile projectile;

	public override float Attack() {
		throw new System.NotImplementedException();
	}
}