using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Hunter : Creature
{

	private Pathfinder pathfinder;

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
		if(path.Count > 1) { 
			targetPosition = path.Last();
		}
		StartCoroutine(MoveToTarget());
	}
}
