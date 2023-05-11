using UnityEngine;

[RequireComponent(typeof(SearchArea))]
public class Enemy : Creature {

	[SerializeField] private Transform[] guardPoints;
	[SerializeField] private Transform playerTransform;

	private SearchArea searchArea;

	private IMovable moveStrategy;
	private PointToPointMoveStrategy pointToPoint;
	private MoveToTargetStrategy moveToTarget;

	public enum MoveStrategyState {
		PointToPoint,
		MoveToPlayer
	}

	MoveStrategyState state = MoveStrategyState.PointToPoint;

	private void Awake() {
		searchArea = GetComponent<SearchArea>();
	}

	private void Start() {
		Init();

		pointToPoint = new PointToPointMoveStrategy(guardPoints, movement);
		moveToTarget = new MoveToTargetStrategy(playerTransform, movement, searchArea);
		moveStrategy = pointToPoint;

		movement.OnReadyToMove += Movement_OnReadyToMove;
		movement.OnMovedToLastPosition += Movement_OnMovedToLastPosition;
	}

	private void Movement_OnMovedToLastPosition(object sender, System.EventArgs e) {
		if (searchArea.IsInsideSearchArea(GetVector2IntPosition(playerTransform.position)) || state == MoveStrategyState.PointToPoint) {
			return;
		}
		state = MoveStrategyState.PointToPoint;
		moveStrategy = pointToPoint;
		searchArea.SetDefaultSearchArea();
	}

	private void Movement_OnReadyToMove(object sender, System.EventArgs e) {
		SwapStrategy();
		moveStrategy.Move();
	}

	private void SwapStrategy() {//StateMachine?
		if (searchArea.IsInsideSearchArea(GetVector2IntPosition(playerTransform.position))) {
			if (state == MoveStrategyState.PointToPoint) {
				state = MoveStrategyState.MoveToPlayer;
				moveStrategy = moveToTarget;
				searchArea.ExpandSearchArea();
			}
		}
	}
}
