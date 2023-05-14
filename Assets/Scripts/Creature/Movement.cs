using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour {

	public event EventHandler OnReadyToMove;

	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] protected float moveCooldown;

	private WorldController worldController;

	private Vector2Int targetPosition;
	private Vector2Int currentPosition;
	private Vector3 targetRotation;

	private float moveTimer;

	private void Awake() {
		worldController = FindObjectOfType<WorldController>();

		if (worldController == null) {
			throw new Exception($"{gameObject.name} can not find world controller");
		}
	}

	public void SetStartProperties(float startRotation, Vector2Int startPosition) {
		SetTargetRotation(startRotation);
		SetTargetPosition(startPosition);
	}

	public void MoveAndRotateToPosition(Vector2Int? position) {
		RotateToPosition(position);
		TryMoveToTargetPosition(position);
	}

	public bool TryMoveAndRotateToPosition(Vector2Int? position) {
		RotateToPosition(position);
		return TryMoveToTargetPosition(position);
	}

	public bool TryMoveToTargetPosition(Vector2Int? position) {
		if (position == null) { 
			return false;
		}
		if (moveTimer > 0 || worldController.IsPositionAvailable((Vector2Int)position) == false 
			|| IsRotating()) {
			return false;
		}
		SetTargetPosition((Vector2Int) position);
		return true;
	}

	public void RotateToPosition(Vector2Int? position) {
		if (position == null) {
			return;
		}
		if (IsResting()) {
			var positionToRotate = new Vector3(position.Value.x, transform.position.y, position.Value.y);
			Quaternion rotationTarget = Quaternion.LookRotation(positionToRotate - transform.position);

			SetTargetRotation(rotationTarget.eulerAngles.y);
		}
	}

	private void SetTargetPosition(Vector2Int position) {
		moveTimer = moveCooldown;
		targetPosition = position;
		worldController.ChangeOccupiedState(currentPosition, targetPosition);
		currentPosition = targetPosition;
	}

	private void SetTargetRotation(float angle) => targetRotation.y = angle;

	private bool IsResting() {
		return !IsMoving() && !IsRotating();
	}

	private bool IsMoving() {
		if (targetPosition == Vector2Int.zero) {
			return false;
		}
		return new Vector3(targetPosition.x, transform.position.y, targetPosition.y) != transform.position;
	}

	private bool IsRotating() {
		return targetRotation != transform.eulerAngles;
	}

	private void Move() {
		var worldPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.y);
		if (targetRotation.y < 0) targetRotation.y = 270f;
		if (targetRotation.y >= 360) targetRotation.y = 0;

		transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, worldPosition, Time.deltaTime * moveSpeed),
			Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationSpeed));
	}

	private void Update() {
		moveTimer -= Time.deltaTime;
		if (moveTimer <= 0) {
			OnReadyToMove?.Invoke(this, EventArgs.Empty);
		}
	}

	private void FixedUpdate() {
		Move();
	}
}
