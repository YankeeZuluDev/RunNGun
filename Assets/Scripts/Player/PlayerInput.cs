using UnityEngine;

/// <summary>
/// This class is used to get input from the user
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [Range(0, 20)]
    [SerializeField] private float mobileInputMultiplier;

    private float horizontalInput;

    public float HorizontalInput => horizontalInput;

    private void Update()
    {
#if UNITY_EDITOR

        horizontalInput = Input.GetAxis("Mouse X");

#elif UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            horizontalInput = (touch.deltaPosition.x / Screen.width) * mobileInputMultiplier;
        }
        else
            horizontalInput = 0;    
            
#endif
    }
}
