/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameInput gameInput;

	private void Start() {
		Init();

		gameInput.OnTurnLeft += GameInput_TurnLeft;
		gameInput.OnTurnRight += GameInput_TurnRight;
		gameInput.OnAttack += GameInput_Attack;
	}

	private void OnDisable() {
		gameInput.OnTurnLeft -= GameInput_TurnLeft;
		gameInput.OnTurnRight -= GameInput_TurnRight;
		gameInput.OnAttack -= GameInput_Attack;
	}

	private void GameInput_Attack(object sender, System.EventArgs e) {
		if (attackTimer <= 0) {
			Attack();
		}
	}

	private void Attack() {
		//Анимация
		Debug.Log("Attack");
		attackTimer = attackCooldown;
	}

	private void Update() {
		attackTimer -= Time.time;
		//Нажимаешь кнопку, получаешь направление, проверяешь наличие тайла, проверяешь можешь ли ты на него ступить
		//Дальше просто задаешь позицию на перемещение в класс movement
		Vector2Int moveDirection = Vector2Int.zero;
		if (gameInput.OnMoveForward()) {
			moveDirection = GetVector2IntPosition(transform.position + transform.forward * World.Space);
		}
		else if (gameInput.OnMoveBack()) {
			moveDirection = GetVector2IntPosition(transform.position - transform.forward * World.Space);
		}
		else if (gameInput.OnMoveLeft()) {
			moveDirection = GetVector2IntPosition(transform.position - transform.right * World.Space);
		}
		else if (gameInput.OnMoveRight()) {
			moveDirection = GetVector2IntPosition(transform.position + transform.right * World.Space);
		}

		if (moveDirection != Vector2Int.zero) {
			movement.SetTargetTile(World.GetTileFromPosition(moveDirection));
		}
	}

	private void GameInput_TurnRight(object sender, System.EventArgs e) {
		movement.SetTargetRotation(90f);
	}

	private void GameInput_TurnLeft(object sender, System.EventArgs e) {
		movement.SetTargetRotation(-90f);
	}
}*/
