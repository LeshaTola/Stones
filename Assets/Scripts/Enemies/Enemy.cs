using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class Enemy : Creature {

	private EnemyBehavior enemyBehavior;

	private void Awake() {
		enemyBehavior = GetComponent<EnemyBehavior>();
	}

	private void Start() {
		Init();
		movement.OnReadyToMove += Movement_OnReadyToMove;
	}

	private void Movement_OnReadyToMove(object sender, System.EventArgs e) {
		var positionToMove = enemyBehavior.GetNextPosition();
		if (positionToMove != Vector2Int.zero) {
			//movement.SetTargetRotation(positionToMove);
			movement.SetTargetPosition(positionToMove);
		}
	}
}
