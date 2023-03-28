using UnityEngine;
using TMPro;

/// <summary>
/// This class is responsble for changing the value text of obstacles and gates
/// </summary>
public class ChangeText : MonoBehaviour
{
    [SerializeField] private TextMeshPro valueText; //rename ot valueText

    public void SetText(string newText)
    {
        valueText.text = newText;
    }
}
