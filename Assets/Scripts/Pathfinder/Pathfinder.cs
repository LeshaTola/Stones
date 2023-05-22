using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathfinder
{
	public class Pathfinder : MonoBehaviour
	{
		private List<Node> path;
		private List<Node> openList;
		private List<Node> closedList;

		private WorldController worldController;

		private void Awake()
		{
			worldController = FindObjectOfType<WorldController>();
		}

		public List<Node> GetPath(Transform target)
		{
			path = new List<Node>();
			openList = new List<Node>();
			closedList = new List<Node>();
			Vector2Int startPosition = new(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
			Vector2Int targetPosition = new(Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.z));

			Node StartNode = new(0, startPosition, null, targetPosition);

			openList.Add(StartNode);

			while (openList.Count > 0)
			{
				Node nodeToCheck = openList.Where(x => x.H == openList.Min(y => y.H)).FirstOrDefault();

				if (nodeToCheck.CurrentPosition == nodeToCheck.TargetPosition)
				{
					path = CalulateThePath(nodeToCheck);
					return path;
				}

				_ = openList.Remove(nodeToCheck);
				closedList.Add(nodeToCheck);

				openList.AddRange(CheckTheNeighbor(nodeToCheck));
			}
			return null;
		}

		private List<Node> CheckTheNeighbor(Node node)
		{
			List<Node> allNeighbors = node.GetNeighbors();

			List<Node> correctNeighbors = new();
			foreach (Node neighbor in allNeighbors)
			{

				if (worldController.IsPositionAvailable(neighbor.CurrentPosition)
					|| neighbor.CurrentPosition == neighbor.TargetPosition
					|| neighbor.CurrentPosition == new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)))
				{
					if (closedList.Where(x => x.CurrentPosition == neighbor.CurrentPosition).FirstOrDefault() == default
					&& openList.Where(x => x.CurrentPosition == neighbor.CurrentPosition).FirstOrDefault() == default)
					{
						correctNeighbors.Add(neighbor);
					}
				}
			}
			return correctNeighbors;
		}

		private List<Node> CalulateThePath(Node node)
		{
			List<Node> path = new();
			Node currentNode = node;

			while (currentNode.PrevNode != null)
			{
				path.Add(currentNode);
				currentNode = currentNode.PrevNode;
			}
			return path;
		}

		private void OnDrawGizmos()
		{
			if (path != null)
			{
				foreach (Node item in path)
				{
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(new Vector3(item.CurrentPosition.x, 0f, item.CurrentPosition.y), 0.2f);
				}
			}

			if (closedList != null)
			{
				foreach (Node item in closedList)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawSphere(new Vector3(item.CurrentPosition.x, 0f, item.CurrentPosition.y), 0.1f);
				}
			}
		}
	}
}