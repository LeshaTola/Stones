using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathfinder {
	public class Pathfinder : MonoBehaviour {
		[Header("Настройки облости видимости")]
		[SerializeField] private int gridSizeX = 20;
		[SerializeField] private int gridSizeY = 20;
		[SerializeField] private Transform gridMidlePoint;

		[Space(20)]
		[Header("Натройки поиска пути")]
		[SerializeField] private Transform target;

		private List<Node> path;
		private List<Node> openList;
		private List<Node> closedList;

		public List<Node> GetPath() {
			path = new List<Node>();
			openList = new List<Node>();
			closedList = new List<Node>();
			var startPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
			var targetPosition = new Vector2Int(Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.z));

			Node StartNode = new Node(0, startPosition, null, targetPosition);

			openList.Add(StartNode);

			while (openList.Count > 0) {
				Node nodeToCheck = openList.Where(x => x.H == openList.Min(y => y.H)).FirstOrDefault();

				if (nodeToCheck.CurrentPosition == nodeToCheck.TargetPosition) {
					path = CalulateThePath(nodeToCheck);
					return path;
				}

				openList.Remove(nodeToCheck);
				closedList.Add(nodeToCheck);

				openList.AddRange(CheckTheNeighbor(nodeToCheck));
			}
			return null;
		}

		private bool IsInsideGrid(Node node) {
			return node.CurrentPosition.x >= gridMidlePoint.position.x - gridSizeX / 2
				&& node.CurrentPosition.y >= gridMidlePoint.position.z - gridSizeY / 2
				&& node.CurrentPosition.x <= gridMidlePoint.position.x + gridSizeX / 2
				&& node.CurrentPosition.y <= gridMidlePoint.position.z + gridSizeY / 2;
		}

		List<Node> CheckTheNeighbor(Node node) {
			List<Node> allNeighbors = node.GetNeighbors();

			List<Node> correctNeighbors = new List<Node>();
			foreach (var neighbor in allNeighbors) {
				if (World.GetTileFromPosition(neighbor.CurrentPosition) != null) {
					if (IsInsideGrid(neighbor)) {
						if (neighbor.Walkable) {
							if (closedList.Where(x => x.CurrentPosition == neighbor.CurrentPosition).FirstOrDefault() == default
							&& openList.Where(x => x.CurrentPosition == neighbor.CurrentPosition).FirstOrDefault() == default) {
								correctNeighbors.Add(neighbor);
							}
						}
					}
				}
			}

			return correctNeighbors;
		}

		List<Node> CalulateThePath(Node node) {
			var path = new List<Node>();
			Node currentNode = node;

			while (currentNode.PrevNode != null) {
				path.Add(currentNode);
				currentNode = currentNode.PrevNode;
			}
			return path;
		}

		private void OnDrawGizmos() {
			if (path != null) {
				foreach (var item in path) {
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(new Vector3(item.CurrentPosition.x, 0f, item.CurrentPosition.y), 0.2f);
				}
			}

			if (closedList != null) {
				foreach (var item in closedList) {
					Gizmos.color = Color.yellow;
					Gizmos.DrawSphere(new Vector3(item.CurrentPosition.x, 0f, item.CurrentPosition.y), 0.1f);
				}
			}
		}
	}
}