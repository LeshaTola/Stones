using UnityEngine;

public class Player : Creature {
	[Space(20)]
	[SerializeField] private GameInput gameInput;

	private float timer;

	private void Start() {

		targetPosition = transform.position;

		gameInput.OnTurnLeft += GameInput_TurnLeft;
		gameInput.OnTurnRight += GameInput_TurnRight;
		gameInput.OnAttack += GameInput_Attack;
	}

	private void GameInput_Attack(object sender, System.EventArgs e) {
		//Если перезарядка атаки закончена
		if (true) {
			Attack();
			//Запускаем перезарядку атаки
		}
	}

	private void Attack() {
		//Анимация
		Debug.Log("Attack");
	}

	private void Update() {
		if (timer <= 0) {
			if (gameInput.OnMoveForward()) {
				SetTargetPosition(transform.forward);
			}
			else if (gameInput.OnMoveBack()) {
				SetTargetPosition(-transform.forward);
			}
			else if (gameInput.OnMoveLeft()) {
				SetTargetPosition(-transform.right);
			}
			else if (gameInput.OnMoveRight()) {
				SetTargetPosition(transform.right);
			}
		}
		else {
			timer = timer - Time.deltaTime;
		}
		Debug.Log(targetPosition != transform.position);
	}

	private void FixedUpdate() {
		Move();
	}

	private void SetTargetPosition(Vector3 offset) {
		if (!IsMoving() && !IsRotating()) {
			if (IsPositionEmpty(transform.position + offset)) {
				targetPosition = transform.position + offset;
				timer = timeBetweenMoves;
			}
		}
	}

	private bool IsMoving() {
		return targetPosition != transform.position;
	}

	private bool IsRotating() {
		return targetRotation != transform.eulerAngles;
	}

	private void GameInput_TurnRight(object sender, System.EventArgs e) {
		if (!IsMoving() && !IsRotating()) {
			targetRotation += Vector3.up * 90f;
		}
	}

	private void GameInput_TurnLeft(object sender, System.EventArgs e) {
		if (!IsMoving() && !IsRotating()) {
			targetRotation -= Vector3.up * 90f;
		}
	}
}