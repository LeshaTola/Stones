using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public event EventHandler OnReadyToMove;
	public event Action OnMoveEnd;
	public event Action OnRotationEnd;

	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] protected float moveCooldown;

	private Vector2Int targetPosition;
	private Vector3 targetRotation;

	private float moveTimer;

	private enum MovementState
	{
		Stay,
		Move
	}

	private enum RotationState
	{
		Stay,
		Rotate
	}

	private RotationState rotationState;
	private MovementState movementState;

	public void SetStartProperties(float startRotation, Vector2Int startPosition)
	{
		SetTargetRotation(startRotation);
		SetTargetPosition(startPosition);
	}

	public void MoveAndRotateToPosition(Vector2Int? position)
	{
		RotateToPosition(position);
		_ = TryMoveToTargetPosition(position);
	}

	public bool TryMoveAndRotateToPosition(Vector2Int? position)
	{
		RotateToPosition(position);
		return TryMoveToTargetPosition(position);
	}

	public bool TryMoveToTargetPosition(Vector2Int? position)
	{
		if (position == null)
		{
			return false;
		}
		if (moveTimer > 0 || IsRotating())
		{
			return false;
		}
		SetTargetPosition((Vector2Int)position);
		return true;
	}

	public void RotateToPosition(Vector2Int? position)
	{
		if (position == null)
		{
			return;
		}
		if (IsResting())
		{
			Vector3 positionToRotate = new(position.Value.x, transform.position.y, position.Value.y);
			Quaternion rotationTarget = Quaternion.LookRotation(positionToRotate - transform.position);

			SetTargetRotation(rotationTarget.eulerAngles.y);
		}
	}

	private void SetTargetPosition(Vector2Int position)
	{
		movementState = MovementState.Move;
		moveTimer = moveCooldown;
		targetPosition = position;
	}

	private void SetTargetRotation(float angle)
	{
		rotationState = RotationState.Rotate;
		targetRotation.y = angle;
	}

	private bool IsResting()
	{
		return !IsMoving() && !IsRotating();
	}

	private bool IsMoving()
	{
		return targetPosition != Vector2Int.zero && new Vector3(targetPosition.x, transform.position.y, targetPosition.y) != transform.position;
	}

	private bool IsRotating()
	{
		return targetRotation != transform.eulerAngles;
	}

	private void Move()
	{
		Vector3 worldPosition = new(targetPosition.x, transform.position.y, targetPosition.y);
		if (targetRotation.y < 0)
		{
			targetRotation.y = 270f;
		}

		if (targetRotation.y >= 360)
		{
			targetRotation.y = 0;
		}

		transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, worldPosition, Time.deltaTime * moveSpeed),
			Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationSpeed));

		switch (rotationState)
		{
			case RotationState.Stay:
				break;
			case RotationState.Rotate:
				if (!IsRotating())
				{
					rotationState = RotationState.Stay;
					OnRotationEnd?.Invoke();
				}
				break;
			default:
				throw new NotImplementedException();
		}

		switch (movementState)
		{
			case MovementState.Stay:
				break;
			case MovementState.Move:
				if (!IsMoving())
				{
					movementState = MovementState.Stay;
					OnMoveEnd?.Invoke();
				}
				break;
			default:
				throw new NotImplementedException();
		}
	}

	private void Update()
	{
		moveTimer -= Time.deltaTime;
		if (moveTimer <= 0)
		{
			OnReadyToMove?.Invoke(this, EventArgs.Empty);
		}
	}

	private void FixedUpdate()
	{
		Move();
	}
}
