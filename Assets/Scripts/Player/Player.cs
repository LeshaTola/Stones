using UnityEngine;

public class Player : Creature, IControllable
{

	private const float CAMERA_SHAKE_INTENSITY = 1.2f;
	private const float CAMERA_SHAKE_TIMER = 0.6f;

	[SerializeField] private GameInput gameInput;
	[SerializeField] private InteractableChecker interactableChecker;
	[SerializeField] private CameraShaker cameraShaker;
	[SerializeField] private Weapon weapon;

	private AttackSystem attackSystem;

	public Weapon Weapon { get => weapon; private set => weapon = value; }
	public Mana Mana { get; private set; }

	private void Awake()
	{
		Mana = GetComponent<Mana>();
		attackSystem = GetComponent<AttackSystem>();
	}

	private void Start()
	{
		Init();
		movement.OnMoveEnd += Movement_OnMoveEnd;
		movement.OnRotationEnd += Movement_OnRotationEnd;
	}

	private void OnDisable()
	{
		movement.OnMoveEnd -= Movement_OnMoveEnd;
		movement.OnRotationEnd -= Movement_OnRotationEnd;
	}


	public void Move(Vector3 direction)
	{
		if (!IsStuned)
		{
			Vector2Int Vec2IntDirection = Tools.GetVector2IntPosition(direction);

			if (World.IsPositionAvailable(direction) == true)
			{
				if (movement.TryMoveToTargetPosition(Vec2IntDirection))
				{
					cameraShaker.ShakeCamera(CAMERA_SHAKE_INTENSITY, CAMERA_SHAKE_TIMER);
					World.ChangeOccupiedState(transform.position, direction, this);
				}
			}
		}
	}

	public void Rotate(Vector3 direction)
	{
		//cameraShaker.ShakeCamera(CAMERA_SHAKE_INTENSITY, CAMERA_SHAKE_TIMER);
		if (!IsStuned)
		{
			movement.RotateToPosition(Tools.GetVector2IntPosition(direction));
		}
	}

	public void Attack()
	{
		if (!IsStuned)
		{
			attackSystem.Attack(this);
		}
	}

	public void Interact()
	{
		if (!IsStuned)
		{
			_ = World.GetTileFromPosition(transform.position + transform.forward).TryInteract(this);
		}
	}

	private void Movement_OnRotationEnd()
	{
		interactableChecker.UpdateVisual(World.GetTileFromPosition(transform.position + transform.forward).CanInteract(this));
	}

	private void Movement_OnMoveEnd()
	{
		interactableChecker.UpdateVisual(World.GetTileFromPosition(transform.position + transform.forward).CanInteract(this));
	}
}