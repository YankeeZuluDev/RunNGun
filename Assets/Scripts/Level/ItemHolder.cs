using UnityEngine;

/// <summary>
/// This is a class for the item holder above obstacle 
/// </summary>

public class ItemHolder : MonoBehaviour, IInteractable, IResettable
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float rotateSpeed;

    private GameObject item;

    public GameObject Item { get => item; set => item = value; }

    public void SpawnItem()
    {
        item = Instantiate(itemPrefab, transform.position, transform.rotation, transform);;
    }

    public void DropItem(Vector3 parentPosition)
    {
        // Unparent
        transform.SetParent(null);

        // Drop item down // TODO: drop down smoothly
        transform.position = parentPosition;
    }

    private void Update()
    {
        // Rotate item holder around itself
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void ResetGameObject()
    {
        Destroy(gameObject);
    }
}
