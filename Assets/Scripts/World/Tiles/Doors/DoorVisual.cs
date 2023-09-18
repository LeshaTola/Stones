using UnityEngine;

public class DoorVisual : MonoBehaviour
{
	private const string OPEND_BOOL = "Opened";

	[SerializeField] private AnimatedDoor door;

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		door.OnDoorOpening += OnDoorOpening;
		door.OnDoorClosing += OnDoorClosing;
	}

	private void OnDisable()
	{
		door.OnDoorOpening -= OnDoorOpening;
		door.OnDoorClosing -= OnDoorClosing;
	}

	private void OnDoorClosing()
	{
		animator.SetBool(OPEND_BOOL, false);
	}

	private void OnDoorOpening()
	{
		animator.SetBool(OPEND_BOOL, true);
	}

	public void Opened()
	{
		door.Open();
	}

	public void Closed()
	{
		door.Close();
	}
}
