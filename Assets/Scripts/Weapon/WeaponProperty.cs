/// <summary>
/// This class represents weapon property
/// </summary>

[System.Serializable]
public class WeaponProperty
{
    public WeaponPropertyType PropertyType;
    public float Amount;
    public float MinAmount;
    public float MaxAmount;

    public WeaponProperty(WeaponProperty property)
    {
        PropertyType = property.PropertyType;
        Amount = property.Amount;
        MinAmount = property.MinAmount;
        MaxAmount = property.MaxAmount;
    }
}
