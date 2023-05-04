using System;
using TMPro;
using UnityEngine;

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

	public void SetTargetPosition(Vector2Int position) {
		if (moveTimer <= 0) {
			if (worldController.IsPositionAvailable(position) == false) {
				return;
			}
			if (IsRotating() == false) {
				moveTimer = moveCooldown;
				targetPosition = position;
				worldController.ChangeOccupiedState(currentPosition, targetPosition);
				currentPosition = targetPosition;
			}
		}
	}

	public void SetTargetRotation(float rotationAngle) {
		if (!IsMoving()) {
			targetRotation += Vector3.up * rotationAngle;
		}
	}

	public void SetTargetRotation(Vector2Int rotation) {
		var positionToRotate = new Vector3(rotation.x, transform.position.y, rotation.y);
/*		Vector3 direction = (targetRotation - positionToRotate).normalized;
		Quaternion rotationTarget = Quaternion.LookRotation(direction);
		Debug.Log(rotationTarget.eulerAngles.y);
		SetTargetRotation(rotationTarget.eulerAngles.y);
*/
		transform.LookAt(positionToRotate);
	}


	public void Move() {
		Vector3 worldPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.y);
		if (targetRotation.y < 0) targetRotation.y = 270f;
		if (targetRotation.y >= 360) targetRotation.y = 0;

		transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, worldPosition, Time.deltaTime * moveSpeed), 
			Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationSpeed));
	}


	private void Update() {
		moveTimer -= Time.deltaTime;
		if(moveTimer <= 0) {
			OnReadyToMove?.Invoke(this, EventArgs.Empty);
		}
	}

	private void FixedUpdate() {
		Move();
	}
}
