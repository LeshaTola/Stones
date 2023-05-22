using UnityEngine;

public static class Tools
{
	public static Vector2Int GetVector2IntPosition(Vector3 direction)
	{
		return new Vector2Int(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.z));
	}
}
