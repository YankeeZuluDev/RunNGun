using UnityEngine;

/// <summary>
/// This is a class for Gate
/// </summary>

[RequireComponent(typeof(ChangeText))]

public class Gate : Spawnable, IInteractable, IResettable
{
    [SerializeField] private WeaponPropertyType propertyType;
    [SerializeField] private string displayValueText;
    [SerializeField] private float realValue;

    private ChangeText changeText;
    private bool isTriggered;

    public WeaponPropertyType PropertyType => propertyType;
    public float RealValue => realValue;
    public bool IsTriggered { get => isTriggered; set => isTriggered = value; }

    private void Awake()
    {
        changeText = GetComponent<ChangeText>();
    }

    private void Start()
    {
        // Set display value text 
        changeText.SetText(displayValueText);
    }

    public void ResetGameObject()
    {
        Destroy(gameObject);
    }
}