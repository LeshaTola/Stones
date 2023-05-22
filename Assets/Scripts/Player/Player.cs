using System;
using UnityEngine;

public class Player : Creature, IControllable
{

    private const float CAMERA_SHAKE_INTENSITY = 1.2f;
    private const float CAMERA_SHAKE_TIMER = 0.6f;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private InteractableChecker interactableChecker;
    [SerializeField] private CameraShaker cameraShaker;
    [SerializeField] private Weapon weapon;

    private WorldController worldController;
    private AttackSystem attackSystem;

    public Weapon Weapon { get => weapon; private set => weapon = value; }

    private void Awake()
    {
        worldController = FindObjectOfType<WorldController>();

        attackSystem = GetComponent<AttackSystem>();
        if (worldController == null)
        {
            throw new Exception($"{gameObject.name} can not find world controller");
        }
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
        Vector2Int Vec2IntDirection = Tools.GetVector2IntPosition(direction);
        if (worldController.IsPositionAvailable(Vec2IntDirection) == true)
        {
            if (movement.TryMoveToTargetPosition(Vec2IntDirection))
            {
                cameraShaker.ShakeCamera(CAMERA_SHAKE_INTENSITY, CAMERA_SHAKE_TIMER);
                worldController.ChangeOccupiedState(Tools.GetVector2IntPosition(transform.position), Vec2IntDirection, this);
            }
        }
    }

    public void Rotate(Vector3 direction)
    {
        //cameraShaker.ShakeCamera(CAMERA_SHAKE_INTENSITY, CAMERA_SHAKE_TIMER);
        movement.RotateToPosition(Tools.GetVector2IntPosition(direction));
    }

    public void Attack()
    {
        Debug.Log("Attack");
        attackSystem.Attack(this);
    }

    public void Interact()
    {
        _ = worldController.GetTileFromPosition(Tools.GetVector2IntPosition(transform.position + transform.forward)).TryInteract(this);
    }

    private void Movement_OnRotationEnd()
    {
        interactableChecker.UpdateVisual(worldController.GetTileFromPosition(Tools.GetVector2IntPosition(transform.position + transform.forward)).CanInteract(this));
    }

    private void Movement_OnMoveEnd()
    {
        interactableChecker.UpdateVisual(worldController.GetTileFromPosition(Tools.GetVector2IntPosition(transform.position + transform.forward)).CanInteract(this));
    }
}