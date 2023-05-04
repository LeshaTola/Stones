using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pathfinder {
	public class Node {
		public Vector2Int CurrentPosition { get; private set; }
		public Node PrevNode { get; private set; }
		public Vector2Int TargetPosition { get; private set; }

		public float F { get; private set; } // G + H // как показала практика, бесполезно
		public float G { get; private set; } //Расстояние от начальной позиции до текущей клетки
		public float H { get; private set; } //Расстояние от текущей клетки до конечной

		public Node(float g, Vector2Int currentTilePosition, Node prevNode, Vector2Int targetTilePosition) {
			CurrentPosition = currentTilePosition;
			PrevNode = prevNode;
			TargetPosition = targetTilePosition;

			G = g;
			H = (targetTilePosition - currentTilePosition).magnitude;
			F = G + H;
		}
		public List<Node> GetNeighbors() {
			var neighbors = new List<Node>() {
				new Node(G+1,CurrentPosition + new Vector2Int(1,0),this,TargetPosition),
				new Node(G+1, CurrentPosition + new Vector2Int(-1, 0),this, TargetPosition),
				new Node(G+1, CurrentPosition + new Vector2Int(0, 1),this,TargetPosition),
				new Node(G+1, CurrentPosition + new Vector2Int(0, -1),this,TargetPosition),
			};
			return neighbors;
		}
	}
}