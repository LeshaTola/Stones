using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
	[Header("Настройки облости видимости")]
	[SerializeField] private int gridSizeX = 20;
	[SerializeField] private int gridSizeY = 20;
	[SerializeField] private Transform gridMidlePoint;

	[Space(20)]
	[Header("Натройки поиска пути")]
	[SerializeField] private Transform target;
	[SerializeField] private LayerMask layerMask;

	private List<Vector3> path;
	private List<Node> openList;
	private List<Node> closedList;

	public void GetPPP() {
		GetPath();
	}
	public List<Vector3> GetPath() {
		path = new List<Vector3>();
		openList = new List<Node>();
		closedList = new List<Node>();
		Vector3 startPosition = new Vector3(Mathf.Round(transform.position.x), 0, Mathf.Round(transform.position.z));
		Vector3 targetPosition = new Vector3(Mathf.Round(target.position.x), 0, Mathf.Round(target.position.z));

		Node StartNode = new Node(0, startPosition, null, targetPosition);

		openList.Add(StartNode);

		while (openList.Count > 0) {
			Node nodeToCheck = openList.Where(x => x.H == openList.Min(y => y.H)).FirstOrDefault();

			if (nodeToCheck.position == nodeToCheck.targetPosition) {
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
		return node.position.x >= gridMidlePoint.position.x - gridSizeX / 2
			&& node.position.z >= gridMidlePoint.position.z - gridSizeY / 2
			&& node.position.x <= gridMidlePoint.position.x + gridSizeX / 2
			&& node.position.z <= gridMidlePoint.position.z + gridSizeY / 2;
	}

	List<Node> CheckTheNeighbor(Node node) {
		List<Node> neighborsOffset = new List<Node> {
			new Node(node.G + 1, new Vector3(node.position.x + 1, 0, node.position.z), node, node.targetPosition),
			new Node(node.G + 1, new Vector3(node.position.x, 0,node.position.z + 1), node, node.targetPosition),
			new Node(node.G + 1, new Vector3(node.position.x - 1, 0, node.position.z), node, node.targetPosition),
			new Node(node.G + 1, new Vector3(node.position.x, 0, node.position.z - 1), node, node.targetPosition)
		};

		List<Node> neighborList = new List<Node>();
		foreach (var neighbor in neighborsOffset) {

			neighbor.walkable = Physics.OverlapBox(neighbor.position, new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity, layerMask).Length == 0;
			if (IsInsideGrid(neighbor)) {
				if (neighbor.walkable) {
					if (!closedList.Contains(neighbor) && !openList.Contains(neighbor)) {
						neighborList.Add(neighbor);
					}
				}
			}
		}

		return neighborList;
	}

	List<Vector3> CalulateThePath(Node node) {
		List<Vector3> path = new List<Vector3>();
		Node currentNode = node;

		while (currentNode.prevNode != null) {
			path.Add(new Vector3(currentNode.position.x, node.position.y, currentNode.position.z));
			currentNode = currentNode.prevNode;
		}
		return path;
	}

	private void OnDrawGizmos() {
		if (path != null) {
			foreach (var item in path) {
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(new Vector3(item.x, item.y, item.z), 0.2f);
			}
		}

		if (closedList != null) {
			foreach (var item in closedList) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(new Vector3(item.position.x, item.position.y, item.position.z), 0.1f);
			}
		}
	}
}

public class Node {
	public Vector3 position;
	public Vector3 targetPosition;
	public Node prevNode;

	public float F; // G + H // как показала практика, бесполезно
	public float G; //Расстояние от начальной позиции до текущей клетки
	public float H; //Расстояние от текущей клетки до конечной

	public bool walkable;

	public Node(float g, Vector3 position, Node prevNode, Vector3 targetPosition) {
		this.position = position;
		this.prevNode = prevNode;
		this.targetPosition = targetPosition;
		G = g;

		H = (targetPosition - position).magnitude;

		F = G + H;
		walkable = true;
	}
}
