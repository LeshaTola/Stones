using Pathfinder;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour {

	public event EventHandler OnReadyToMove;
	public event EventHandler OnMovedToLastPosition;

	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] protected float moveCooldown;

	private Pathfinder.Pathfinder pathfinder;
	private WorldController worldController;

	private Vector2Int targetPosition;
	private Vector2Int currentPosition;
	private Vector3 targetRotation;

	private float moveTimer;
	private List<Node> lastPath;

	private void Awake() {
		worldController = FindObjectOfType<WorldController>();
		pathfinder = GetComponent<Pathfinder.Pathfinder>();
	}

	public void FindPath(Transform target) {
		var path = pathfinder.GetPath(target);
		if (path != null) {
			lastPath = path;
		}
	}
	public Vector2Int? GetNextPosition() {
		if (lastPath == null ) {
			return null;
		}

		if (lastPath.Count > 0) {
			var last = lastPath.Last();
			lastPath.Remove(lastPath.Last());
			return last.CurrentPosition;
		}
		else {
			OnMovedToLastPosition?.Invoke(this, EventArgs.Empty);
			return null;
		}

	}
	public void SetNextPositionToTarget(Transform target) {
		FindPath(target);
		SetNextPosition(GetNextPosition());
	}

	public void SetNextPosition(Vector2Int? nextPosition) {
		if (nextPosition == null) {
			//Debug.Log("Position Is Null");
			return;
		}
		RotateToPosition((Vector2Int)nextPosition); //ToFix
		SetTargetPosition((Vector2Int)nextPosition);
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

	public void AddAngleToTargetRotation(float rotationAngle) {
		if (!IsMoving()) {
			targetRotation += Vector3.up * rotationAngle;
		}
	}

	public void RotateToPosition(Vector2Int rotation) {
		var positionToRotate = new Vector3(rotation.x, transform.position.y, rotation.y);
		Quaternion rotationTarget = Quaternion.LookRotation(positionToRotate - transform.position);

		SetTargetRotation(rotationTarget.eulerAngles.y);
	}

	private void SetTargetRotation(float angle) => targetRotation.y = angle;

	private bool IsMoving() {
		if (targetPosition == Vector2Int.zero) {
			return false;
		}
		return new Vector3(targetPosition.x, transform.position.y, targetPosition.y) != transform.position;
	}
	private bool IsRotating() {
		return targetRotation != transform.eulerAngles;
	}

	public bool IsResting() {
		return !IsMoving() && !IsRotating();
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
