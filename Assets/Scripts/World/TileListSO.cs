using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Node List")]
public class TileListSO : ScriptableObject
{
	[SerializeField] private List<Tile> tiles;

	public List<Tile> Nodes => tiles;
}