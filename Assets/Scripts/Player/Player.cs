using UnityEngine;

public class Player : Creature {
	[Space(20)]
	[SerializeField] private GameInput gameInput;

	private void Start() {

		targetPosition = transform.position;

		gameInput.TurnLeft += GameInput_TurnLeft;
		gameInput.TurnRight += GameInput_TurnRight;
		gameInput.MoveForward += GameInput_MoveForward;
		gameInput.MoveBack += GameInput_MoveBack;
		gameInput.MoveLeft += GameInput_MoveLeft;
		gameInput.MoveRight += GameInput_MoveRight;
		gameInput.Attack += GameInput_Attack;
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

	public bool AtRest {
		get {
			if ((Vector3.Distance(transform.position, targetPosition) < 0.05f)
				&& (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f)) {
				return true;
			}
			else {
				return false;
			}
		}
	}

	private void GameInput_MoveRight(object sender, System.EventArgs e) {
		if (AtRest) {
			if (IsPositionEmpty(transform.position + transform.right)) {
				targetPosition = transform.position + transform.right;
			}
		}
	}

	private void GameInput_MoveLeft(object sender, System.EventArgs e) {
		if (AtRest) {
			if (IsPositionEmpty(transform.position - transform.right)) {
				targetPosition = transform.position - transform.right;
			}
		}
	}

	private void GameInput_MoveBack(object sender, System.EventArgs e) {
		if (AtRest) {
			if (IsPositionEmpty(transform.position - transform.forward)) {
				targetPosition = transform.position - transform.forward;
			}
		}
	}

	private void GameInput_MoveForward(object sender, System.EventArgs e) {
		if (AtRest) {
			if (IsPositionEmpty(transform.position + transform.forward)) {
				targetPosition = transform.forward + transform.position;
			}
		}
	}

	private void FixedUpdate() {
		Move();
	}

	private void GameInput_TurnRight(object sender, System.EventArgs e) {
		targetRotation += Vector3.up * 90f;
	}
	private void GameInput_TurnLeft(object sender, System.EventArgs e) {
		targetRotation -= Vector3.up * 90f;
	}
}