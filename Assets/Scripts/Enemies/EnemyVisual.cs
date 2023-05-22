using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
	[SerializeField] private Movement enemyMovement;

	private const string STEP_TRIGGER = "Step";
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	private void OnEnable()
	{
		enemyMovement.OnReadyToMove += OnReadyToMove;
	}
	private void OnDisable()
	{
		enemyMovement.OnReadyToMove -= OnReadyToMove;
	}

	private void OnReadyToMove(object sender, System.EventArgs e)
	{
		animator.SetTrigger(STEP_TRIGGER);
	}
}
