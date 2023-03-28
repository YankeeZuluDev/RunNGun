using UnityEngine;

/// <summary>
/// This is a class for camera, that follows player
/// </summary>
public class FollowCamera : MonoBehaviour, IResettable
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothness;

    private PlayerMovement playerMovement;
    private Vector3 initialCameraPosition;

    public void InitialzieCamera(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    private void Start()
    {
        //playerMovement = PlayerMovement.Instance;

        initialCameraPosition = transform.position;
    }

    private void LateUpdate()
    {
        // Follow player
        transform.position = Vector3.Lerp(transform.position, offset + playerMovement.GetPlayerPositon(), smoothness * Time.deltaTime);
    }

    public void ResetGameObject()
    {
        transform.position = initialCameraPosition;
    }
}
