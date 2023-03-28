using UnityEngine;

/// <summary>
/// This class is used to change the color of a gameobject based on given value and color gradient
/// </summary>

[RequireComponent(typeof(MeshRenderer))]

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Gradient colorGradient;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // Initialzie
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void EvaluateColor(float minValue, float maxValue, float value)
    {
        float normalizedValue = Mathf.InverseLerp(minValue, maxValue, value);
        meshRenderer.material.color = colorGradient.Evaluate(normalizedValue);
    }
}
