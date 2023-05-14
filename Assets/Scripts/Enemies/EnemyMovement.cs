using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinder;
using System.Linq;
using System;

[RequireComponent(typeof(Pathfinder.Pathfinder), typeof(Movement))]
public class EnemyMovement : MonoBehaviour
{
	public event EventHandler OnMovedToLastPosition;

	private Movement movement;
	private Pathfinder.Pathfinder pathfinder;

	private List<Node> lastPath;

	private void Awake() {
		pathfinder = GetComponent<Pathfinder.Pathfinder>();
		movement= GetComponent<Movement>();	
	}

	private void FindPath(Transform target) {
		var path = pathfinder.GetPath(target);
		if (path != null) {
			lastPath = path;
		}
	}

	private Vector2Int? GetNextPosition() {
		if (lastPath == null) {
			return null;
		}

		if (lastPath.Count > 0) {
			var last = lastPath.Last();
			return last.CurrentPosition;
		}
		else {
			OnMovedToLastPosition?.Invoke(this, EventArgs.Empty);
			return null;
		}
	}

	public void MoveToNextPosition() {
		if(movement.TryMoveAndRotateToPosition(GetNextPosition()) == true) {
			lastPath.Remove(lastPath.Last());
		}
	}

	public void MoveToTarget(Transform target) {
		FindPath(target);
		MoveToNextPosition();
	}
}
