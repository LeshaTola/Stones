using Pathfinder;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour {

	[SerializeField] private Tilemap map;
	private int space = 1;

	private void Awake() {
	 var tiles = new List<Tile>();
		foreach(Transform child in map.transform) {
			tiles.Add(child.GetComponent<Tile>());
		}
	World.Init(tiles, space);
	}

	/*		void CreateWorld() {
				tiles = new List<Node>();

				Vector2Int startPosition = GetStartWorldPosition();
				Vector2Int curentPosition = startPosition;
				for (int i = 0; i < xWorldSize; i++) {
					for (int j = 0; j < yWorldSize; j++) {
						Node newNode = Instantiate(nodeList.Nodes[UnityEngine.Random.Range(0, nodeList.Nodes.Count)], WorldCenter);
						newNode.transform.position = new Vector3(curentPosition.x, 0, curentPosition.y);
						curentPosition.y += space;
					}
					curentPosition.x += space;
					curentPosition.y = startPosition.y;
				}
			}*/
}