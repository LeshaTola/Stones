using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hunter : Creature {

	private Pathfinder pathfinder;
	private List<Vector3> lastPath;

	private void Start() {
		targetPosition = transform.position;
		pathfinder = GetComponent<Pathfinder>();
		StartCoroutine(MoveToTarget());
	}

	private void FixedUpdate() {
		Move();
	}

	public IEnumerator MoveToTarget() {
		yield return new WaitForSeconds(timeBetweenMoves);
		var path = pathfinder.GetPath();
		if (path != null) {
			lastPath = path;
		}

		if (lastPath.Count > 1) {
			if (IsPositionEmpty(lastPath.Last())) {
				targetPosition = lastPath.Last();
				lastPath.Remove(lastPath.Last());
			}
		}
		StartCoroutine(MoveToTarget());
	}
}
