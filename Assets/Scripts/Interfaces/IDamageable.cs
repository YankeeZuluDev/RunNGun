/// <summary>
/// This is an interface for all gameobjects, that are able to take damage
/// </summary>

public interface IDamageable
{
    void TakeDamage(float damage);
    void Kill();
}
