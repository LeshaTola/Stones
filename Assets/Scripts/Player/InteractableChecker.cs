using UnityEngine;

public class InteractableChecker : MonoBehaviour
{
	[SerializeField] private MainUI PressFText;

	public void UpdateVisual(bool isInteractable)
	{
		if (isInteractable)
		{
			PressFText.Show();
		}
		else
		{
			PressFText.Hide();
		}
	}
}
