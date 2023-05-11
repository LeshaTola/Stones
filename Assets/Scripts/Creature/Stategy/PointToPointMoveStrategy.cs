using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPointMoveStrategy : IMovable
{
	private readonly Transform[] points;
	private readonly Movement movement;
	private int targetPointIterator = 0;

	public PointToPointMoveStrategy(Transform[] points, Movement movement) {
		this.points = points;
		this.movement = movement;
		movement.OnMovedToLastPosition += Movement_OnMovedToLastPosition;
	}

	private void Movement_OnMovedToLastPosition(object sender, System.EventArgs e) {
		if (targetPointIterator == points.Length - 1) {
			targetPointIterator = 0;
		}
		else {
			targetPointIterator++;
		}
	}

	public void Move() {
		movement.SetNextPositionToTarget(points[targetPointIterator]);
	}
}
