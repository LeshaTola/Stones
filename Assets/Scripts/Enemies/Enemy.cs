using System;
using UnityEngine;

[RequireComponent(typeof(SearchArea))]
public class Enemy : Creature
{

	[SerializeField] private Transform[] guardPoints;
	[SerializeField] private Transform playerTransform;

	private SearchArea searchArea;

	private IMovable moveStrategy;
	private PointToPointMoveStrategy pointToPoint;
	private MoveToTargetStrategy moveToTarget;
	private EnemyMovement enemyMovement;

	public enum MoveStrategyState
	{
		PointToPoint,
		MoveToPlayer
	}

	private MoveStrategyState state = MoveStrategyState.PointToPoint;

	private void Awake()
	{
		searchArea = GetComponent<SearchArea>();
		enemyMovement = GetComponent<EnemyMovement>();
	}

	private void Start()
	{
		Init();

		pointToPoint = new PointToPointMoveStrategy(guardPoints, enemyMovement);
		moveToTarget = new MoveToTargetStrategy(playerTransform, enemyMovement, searchArea);
		moveStrategy = pointToPoint;

		movement.OnReadyToMove += Movement_OnReadyToMove;
		enemyMovement.OnMovedToLastPosition += Movement_OnMovedToLastPosition;
	}

	private void EnemyStrategy()
	{
		switch (state)
		{
			case MoveStrategyState.PointToPoint:
				if (searchArea.IsInsideSearchArea(Tools.GetVector2IntPosition(playerTransform.position)))
				{
					state = MoveStrategyState.MoveToPlayer;
					moveStrategy = moveToTarget;
					searchArea.ExpandSearchArea();
				}
				break;
			case MoveStrategyState.MoveToPlayer:
				// Move To Player
				break;
			default:
				throw new NotImplementedException();
		}

	}

	private void Movement_OnMovedToLastPosition(object sender, System.EventArgs e)
	{
		if (searchArea.IsInsideSearchArea(Tools.GetVector2IntPosition(playerTransform.position)) || state == MoveStrategyState.PointToPoint)
		{
			return;
		}
		state = MoveStrategyState.PointToPoint;
		moveStrategy = pointToPoint;
		searchArea.SetDefaultSearchArea();
	}

	private void Movement_OnReadyToMove(object sender, System.EventArgs e)
	{
		EnemyStrategy();
		moveStrategy.Move();
	}
}
