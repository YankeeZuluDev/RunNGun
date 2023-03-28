using UnityEngine;

/// <summary>
/// This class is used to manage weapon positions
/// </summary>

[RequireComponent(typeof(WeaponList))]

public class WeaponPositionManager : MonoBehaviour
{
    [Range(0, 3)]
    [SerializeField] private float weaponAreaWidth;

    private WeaponList weaponList;

    private void Awake()
    {
        weaponList = GetComponent<WeaponList>();
    }

    public void UpdateWeaponPositions()
    {
        // Distribute weapon postions evenly along weaponAreaWidth

        // Get amount of weapons
        int n = weaponList.Weapons.Count;

        // Calculate the distance between each weapon position
        float weaponOffset = weaponAreaWidth / (n + 1);

        for (int i = 0; i < n; i++)
        {
            // Calculate the x position of the weapon
            float xPos = weaponOffset * (i + 1) - weaponAreaWidth / 2;

            // Set new x position of the weapon
            weaponList.Weapons[i].transform.position = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z);
        }
    }
}
