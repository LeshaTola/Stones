using Pathfinder;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Pathfinder.Pathfinder))]
public class Hunter : Creature {

	private Pathfinder.Pathfinder pathfinder;
	private List<Node> lastPath;

	private void Start() {
		SetTargetTile(World.GetTileFromPosition(GetVector2IntPositionToMove(transform.position)));
		pathfinder = GetComponent<Pathfinder.Pathfinder>();
		StartCoroutine(TemerBetweenMoves());
	}

	private void FixedUpdate() {
		Move();
	}

	public IEnumerator TemerBetweenMoves() {
		yield return new WaitForSeconds(timeBetweenMoves);

		SetTargetTile(GetNextPosition());
		StartCoroutine(TemerBetweenMoves());
	}

	public Tile GetNextPosition() {
		var path = pathfinder.GetPath();
		if (path != null) {
			lastPath = path;
		}
		if (lastPath != null) {
			var last = lastPath.Last();

			if (lastPath.Count > 1) {
				lastPath.Remove(lastPath.Last());
				return World.GetTileFromPosition(last.CurrentPosition);
			}
			else {
				return currentTile;
			}
		}
		return currentTile;
	}
}
