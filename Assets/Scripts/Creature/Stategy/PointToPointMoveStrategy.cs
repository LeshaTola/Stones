using UnityEngine;

public class PointToPointMoveStrategy : IMovable
{
	private readonly Transform[] points;
	private readonly EnemyMovement movement;
	private int targetPointIterator = 0;

	public PointToPointMoveStrategy(Transform[] points, EnemyMovement movement)
	{
		this.points = points;
		this.movement = movement;
		movement.OnMovedToLastPosition += EnemyMovement_OnMovedToLastPosition;
	}

	public void Move()
	{
		movement.MoveToTarget(points[targetPointIterator]);
	}

	private void EnemyMovement_OnMovedToLastPosition(object sender, System.EventArgs e)
	{
		if (targetPointIterator == points.Length - 1)
		{
			targetPointIterator = 0;
		}
		else
		{
			targetPointIterator++;
		}
	}
}
