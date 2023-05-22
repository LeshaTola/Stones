using UnityEngine;

public class SearchArea : MonoBehaviour
{
	[Header("Настройки облости видимости")]
	[SerializeField] private int defaultSizeX = 20;
	[SerializeField] private int defaultSizeY = 20;
	[SerializeField] private int ExpansionX = 0;
	[SerializeField] private int ExpansionY = 0;

	[SerializeField] private Transform MidlePoint;

	private int searchAreaX;
	private int searchAreaY;

	private void Start()
	{
		SetDefaultSearchArea();
	}

	public bool IsInsideSearchArea(Transform currentPosition)
	{
		return IsInsideSearchArea(new Vector2Int(Mathf.RoundToInt(currentPosition.position.x), Mathf.RoundToInt(currentPosition.position.z)));
	}

	public bool IsInsideSearchArea(Vector2Int currentPosition)
	{
		return currentPosition.x >= MidlePoint.position.x - (searchAreaX / 2)
			&& currentPosition.y >= MidlePoint.position.z - (searchAreaY / 2)
			&& currentPosition.x <= MidlePoint.position.x + (searchAreaX / 2)
			&& currentPosition.y <= MidlePoint.position.z + (searchAreaY / 2);
	}

	public void ExpandSearchArea()
	{
		searchAreaX = defaultSizeX + ExpansionX;
		searchAreaY = defaultSizeY + ExpansionY;
	}

	public void SetDefaultSearchArea()
	{
		searchAreaX = defaultSizeX;
		searchAreaY = defaultSizeY;
	}
}
