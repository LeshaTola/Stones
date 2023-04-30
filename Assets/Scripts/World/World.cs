using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public static class World {

	private static List<Tile> tiles;
	public static int Space { get; private set; }


	public static void Init(List<Tile> tiles, int space) {

		World.Space = space;
		World.tiles = tiles;
		foreach (var tile in tiles) {
			tile.Position = new Vector2Int((int)tile.gameObject.transform.position.x, (int)tile.gameObject.transform.position.z);
		}

		/*		Vector2Int startPosition = GetStartWorldPosition();
		Vector2Int curentPosition = startPosition;
		for (int x = 0; x < xWorldSize; x++) {
			for (int y = 0; y < yWorldSize; y++) {

				int currentIterator = GetCurrentIterator(x, y, xWorldSize);

				tiles[currentIterator].transform.position = new Vector3(curentPosition.x, 0, curentPosition.y);
				tiles[currentIterator].Position = curentPosition;
				tiles[currentIterator].SetNeighbors(
					y == 0 ? null : tiles[GetCurrentIterator(x, y - 1, xWorldSize)], 
					x == 0 ? null : tiles[GetCurrentIterator(x - 1, y, xWorldSize)]
					);

				curentPosition.y -= space;
			}
			curentPosition.x += space;
			curentPosition.y = startPosition.y;
		}*/
	}

	public static Tile GetTileFromPosition(Vector2Int postiton) {
		return tiles.FirstOrDefault<Tile>((x)=>x.Position == postiton);
	}

/*	private static int GetCurrentIterator(int x, int y, int xWorldSize) {
		return x * xWorldSize + y;
	}*/

/*	private static Vector2Int GetStartWorldPosition() {
		return new Vector2Int((int)worldCenter.x - xWorldSize / 2 * space, (int)worldCenter.y + yWorldSize / 2 * space);
	}*/
}