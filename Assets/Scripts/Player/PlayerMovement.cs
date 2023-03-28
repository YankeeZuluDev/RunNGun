using UnityEngine;

/// <summary>
/// This class handles the horizontal and forward movement of the player
/// </summary>

[RequireComponent(typeof(PlayerInput))]

public class PlayerMovement : MonoBehaviour, IResettable
{
    [Range(0, 50)]
    [SerializeField] private float forwardMoveSpeed;
    [Range(0, 50)]
    [SerializeField] private float horizontalMoveSpeed;
    [Range(0, 20)]
    [SerializeField] private float smoothness;

    private PlayerInput playerInput;
    private Vector3 initialPlayerPosition;
    private Vector3 targetPosition;
    private bool allowMovement;
    private float roadWidth;
    private float leftBorder;
    private float rightBorder;

    public void InitializePlayerMovement(float roadWidth)
    {
        this.roadWidth = roadWidth;
    }

    private void Awake()
    {
        // Get player input
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        // Set initial player positon
        targetPosition = initialPlayerPosition = transform.position;

        // Set left and right borders
        leftBorder = -roadWidth / 2;
        rightBorder = roadWidth / 2;
    }

    private void Update() => Move();

    private void Move()
    {
        // Exit if movement is not allowed
        if (!allowMovement) return; 

        // Cache delta time
        float deltaTime = Time.deltaTime;

        // Set x and z postions
        float xPos = playerInput.HorizontalInput * horizontalMoveSpeed;
        float zPos = forwardMoveSpeed * deltaTime;

        // Add xPos and zPos to target position
        targetPosition += new Vector3(xPos, 0, zPos);

        // Clamp x position
        targetPosition.x = Mathf.Clamp(targetPosition.x, leftBorder, rightBorder);

        // Move
        transform.position = Vector3.Lerp(transform.position, targetPosition, deltaTime * smoothness);
    }

    public Vector3 GetPlayerPositon()
    {
        return transform.position;
    }

    public void AllowMovement()
    {
        allowMovement = true;
    }

    public void StopMovement()
    {
        allowMovement = false;
    }

    public void ResetGameObject()
    {
        // Reset player position
        targetPosition = transform.position = initialPlayerPosition;
    }
}
