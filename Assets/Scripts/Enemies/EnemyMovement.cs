using Pathfinder;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Pathfinder.Pathfinder), typeof(Movement))]
public class EnemyMovement : MonoBehaviour
{
	public event EventHandler OnMovedToLastPosition;

	private Movement movement;
	private Pathfinder.Pathfinder pathfinder;
	private WorldController worldController;

	private List<Node> lastPath;

	private void Awake()
	{
		pathfinder = GetComponent<Pathfinder.Pathfinder>();
		movement = GetComponent<Movement>();

		worldController = FindObjectOfType<WorldController>();

		if (worldController == null)
		{
			throw new Exception($"{gameObject.name} can not find world controller");
		}
	}

	public void MoveToNextPosition()
	{
		Vector2Int? Vec2IntDirection = GetNextPosition();
		if (Vec2IntDirection == null)
		{
			return;
		}
		Vector3 direction = new(Vec2IntDirection.Value.x, transform.position.y, Vec2IntDirection.Value.y);

		movement.RotateToPosition(Vec2IntDirection);

		if (worldController.IsPositionAvailable(direction) == true)
		{
			if (movement.TryMoveToTargetPosition(Vec2IntDirection))
			{
				worldController.ChangeOccupiedState(transform.position, direction, GetComponent<Creature>());
				_ = lastPath.Remove(lastPath.Last());
			}
		}
	}

	public void MoveToTarget(Transform target)
	{
		FindPath(target);
		MoveToNextPosition();
	}

	private void FindPath(Transform target)
	{
		List<Node> path = pathfinder.GetPath(target);
		if (path != null)
		{
			lastPath = path;
		}
	}

	private Vector2Int? GetNextPosition()
	{
		if (lastPath == null)
		{
			return null;
		}

		if (lastPath.Count > 0)
		{
			Node last = lastPath.Last();
			return last.CurrentPosition;
		}
		else
		{
			OnMovedToLastPosition?.Invoke(this, EventArgs.Empty);
			return null;
		}
	}
}
