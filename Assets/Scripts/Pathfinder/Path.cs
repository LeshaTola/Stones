using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public class Path : MonoBehaviour {
	private const float PerpendicularDistance = 1f;

	static Dictionary<int, Cell> visited;

	[SerializeField] private Transform target;
	[SerializeField] private LayerMask layerMask;
	private List<Vector3> path;
	private void Update() {
		GetPath();
	}

	public List<Vector3> GetPath() {
		path= new List<Vector3>();
		Cell curentCell = SearchDirected(new Cell(new Vector2Int((int)transform.position.x, (int)transform.position.z))
			, new Cell(new Vector2Int((int)target.transform.position.x, (int)target.transform.position.z)));

		while(curentCell?.Parent!= null) {
			path.Add(new Vector3(curentCell.Position.x,0, curentCell.Position.y));
			curentCell= curentCell.Parent;
		}
		Debug.Log(path.Count);
		return path;
	}

	private Cell SearchDirected(Cell entry, Cell target) {
		visited = new Dictionary<int, Cell>();
		SortedSet<Cell> toVisit = new SortedSet<Cell>(new CellComparer());
		Dictionary<int, Cell> toVisitDic = new Dictionary<int, Cell>();

		entry.DistanceLeft = (target.Position - entry.Position).magnitude;
		toVisit.Add(entry);
		toVisitDic.Add(entry.GetHashCode(), entry);
		while (toVisit.Count > 0) {
			Cell current = toVisit.Min;
			visited.Add(current.GetHashCode(), current);
			toVisit.Remove(current);
			toVisitDic.Remove(current.GetHashCode());

			if (current.Equals(target)) {
				Debug.Log(visited.Count);
				return current;
			}
			List<Cell> neighbours = GetNeighbours(current);
			foreach (Cell neighbour in neighbours) {
				if (!visited.ContainsKey(neighbour.GetHashCode()) && !toVisitDic.ContainsKey(neighbour.GetHashCode())) {
					neighbour.DistanceLeft = (target.Position - neighbour.Position).magnitude;
					toVisit.Add(neighbour);
					toVisitDic.Add(neighbour.GetHashCode(), neighbour);
				}
			}
		}

		return null;
	}

	private void OnDrawGizmos() {
		if (path != null) {
			foreach (var item in path) {
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(new Vector3(item.x, item.y, item.z), 0.2f);
			}
		}

		if (visited != null) {
			foreach (var item in visited) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(new Vector3(item.Value.Position.x,0, item.Value.Position.y), 0.1f);
			}
		}
	}

private List<Cell> GetNeighbours(Cell cell, bool addDiagonal = false) {
		List<Cell> neighbours = new List<Cell>();

		for (int x = -1; x < 2; x += 2) {
			Cell horizontalNeighbour = cell.GetNeighbour(x, 0);
			AddIfFreeToMove(horizontalNeighbour, PerpendicularDistance);

			for (int y = -1; y < 2; y += 2) {
				Cell verticalNeighbour = cell.GetNeighbour(0, y);

				if (x == -1)
					AddIfFreeToMove(verticalNeighbour, PerpendicularDistance);
			}
		}

		return neighbours;

		void AddIfFreeToMove(Cell newCell, float distance) {
			if (newCell.IsFreeToMove(layerMask))
				neighbours.Add(newCell.SetParent(cell).SetDistance(cell.Distance + distance));
		}
	}
}
