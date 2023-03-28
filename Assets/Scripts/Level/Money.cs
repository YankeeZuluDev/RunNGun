using UnityEngine;

/// <summary>
/// This is a class for money
/// </summary>
public class Money : MonoBehaviour, IPickupable
{
    [SerializeField] private int amount;

    public int Amount => amount;
}
