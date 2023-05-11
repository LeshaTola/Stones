using UnityEngine;

public class MoveToTargetStrategy : IMovable {

	private readonly Transform target;
	private readonly Movement movement;
	private readonly SearchArea searchArea;
	public MoveToTargetStrategy(Transform target, Movement movement, SearchArea searchArea) { 
		this.target = target; 
		this.movement = movement;
		this.searchArea = searchArea;
	}

	public void Move() {
		if (searchArea.IsInsideSearchArea(target)) {
			movement.SetNextPositionToTarget(target);
		}
		else {
			movement.SetNextPosition(movement.GetNextPosition());
		}
	}
}
