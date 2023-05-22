using UnityEngine;

public class MoveToTargetStrategy : IMovable
{

	private readonly Transform target;
	private readonly EnemyMovement movement;
	private readonly SearchArea searchArea;
	public MoveToTargetStrategy(Transform target, EnemyMovement movement, SearchArea searchArea)
	{
		this.target = target;
		this.movement = movement;
		this.searchArea = searchArea;
	}

	public void Move()
	{
		if (searchArea.IsInsideSearchArea(target))
		{
			movement.MoveToTarget(target);
		}
		else
		{
			movement.MoveToNextPosition();
		}
	}
}
