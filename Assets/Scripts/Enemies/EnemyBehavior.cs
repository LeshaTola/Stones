using Pathfinder;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Pathfinder.Pathfinder))]
public class EnemyBehavior : MonoBehaviour {

	[Header("Натройки поиска пути")]
	[SerializeField] private Transform target;// Для проверки

	private Pathfinder.Pathfinder pathfinder;
	private List<Node> lastPath;

	private void Start() {
		pathfinder = GetComponent<Pathfinder.Pathfinder>();
	}

	public Vector2Int GetNextPosition() {
		var lastTargetPosition = new Vector2Int(Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.z));
		
		if (pathfinder.IsInsideSearchArea(lastTargetPosition)) {
			pathfinder.ExpandSearchArea();
			var path = pathfinder.GetPath(target);
			if (path != null) {
				lastPath = path;
			}
		}

		if (lastPath == null) {
			return default;
		}

		if (lastPath.Count > 0) {
			var last = lastPath.Last();
			lastPath.Remove(lastPath.Last());
			return last.CurrentPosition;
		}
		else {
			pathfinder.SetDefaultSearchArea();
			return default;
		}
	}
}