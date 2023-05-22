using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI PressFText;

	private void Start()
	{
		Hide();
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}