using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisual : MonoBehaviour {

	Animator animator;
	[SerializeField] GameInput gameInput;

	private const string ATTACK_TRIGGER = "AttackTrigger";

	private void Start() {
		gameInput.OnAttack += GameInput_Attack;
	}

	private void GameInput_Attack(object sender, System.EventArgs e) {
		animator.SetTrigger(ATTACK_TRIGGER);
	}

	private void Awake() {
		animator = GetComponent<Animator>();
	}


}