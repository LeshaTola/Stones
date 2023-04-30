using UnityEngine;

public class Player : Creature {
	[Space(20)]
	[SerializeField] private GameInput gameInput;

	private void Start() {

		SetTargetTile(World.GetTileFromPosition(GetVector2IntPositionToMove(transform.position)));


		gameInput.OnTurnLeft += GameInput_TurnLeft;
		gameInput.OnTurnRight += GameInput_TurnRight;
		gameInput.OnAttack += GameInput_Attack;
	}

	private void GameInput_Attack(object sender, System.EventArgs e) {
		if (attackTimer <= 0) {
			Attack();
		}
	}

	private void Attack() {
		//Анимация
		Debug.Log("Attack");
		attackTimer = timeBetweenAttacks;
	}

	public enum MoveDirection {
		Front,
		Right,
		Back,
		Left
	}

	private void Update() {
		attackTimer -= Time.time;
		if (moveTimer <= 0) {
			if (gameInput.OnMoveForward()) {
				SetTargetTile(World.GetTileFromPosition(GetVector2IntPositionToMove(transform.position + transform.forward)));
			}
			else if (gameInput.OnMoveBack()) {
				SetTargetTile(World.GetTileFromPosition(GetVector2IntPositionToMove(transform.position - transform.forward)));
			}
			else if (gameInput.OnMoveLeft()) {
				SetTargetTile(World.GetTileFromPosition(GetVector2IntPositionToMove(transform.position - transform.right)));
			}
			else if (gameInput.OnMoveRight()) {
				SetTargetTile(World.GetTileFromPosition(GetVector2IntPositionToMove(transform.position + transform.right)));
			}
		}
		else {
			moveTimer -= Time.deltaTime;
		}
	}

	/*	private Tile GetTileToMoveAccordingFromRotation(int rotation, int direction) {
			direction += rotation;
			int rotationAmount = 4;
			direction = direction % rotationAmount;

			switch (direction) {
				case 0:
					return currentTile.UpTile != null? currentTile.UpTile : currentTile;
				case -3:
				case 1:
					return currentTile.RightTile != null? currentTile.RightTile : currentTile;
				case -2:
				case 2:
					return currentTile.DownTile != null? currentTile.DownTile : currentTile;
				case -1:
				case 3:
					return currentTile.LeftTile != null? currentTile.LeftTile : currentTile;
				default:
					Debug.LogError("Rotation is incorect: " + rotation);
					throw new ArgumentOutOfRangeException(nameof(rotation));
			}
		}*/

	private void FixedUpdate() {
		Move();
	}

	private void GameInput_TurnRight(object sender, System.EventArgs e) {
		SetTargetRatation(90f);
	}

	private void GameInput_TurnLeft(object sender, System.EventArgs e) {
		SetTargetRatation(-90f);
	}

	public void SetTargetRatation(float rotationAngle) {
		if (!IsMoving() && !IsRotating()) {
			targetRotation += Vector3.up * rotationAngle;
		}
	}
}