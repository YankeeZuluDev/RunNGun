using System.Collections;
using UnityEngine;

/// <summary>
/// This is a class for obstacle
/// </summary>

[RequireComponent(typeof(ChangeText))]
[RequireComponent(typeof(ChangeColor))]

public class Obstacle : Spawnable, IInteractable, IDamageable, IResettable
{
    private const float minHealth = 0;
    private const float maxHealth = 500;

    [Range(minHealth, maxHealth)]
    [SerializeField] private float health;
    [Range(0, 1)]
    [SerializeField] private float impactStrenght;
    [SerializeField] private float impactAnimationDuration;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private ChangeText changeText;
    private ChangeColor changeColor;
    private ItemHolder itemHolder;
    private bool isPlayingImpactAnimaton;

    private void Awake()
    {
        changeText = GetComponent<ChangeText>();
        changeColor = GetComponent<ChangeColor>();
        itemHolder = GetComponentInChildren<ItemHolder>();
    }

    private void Start()
    {
        // Set health text
        changeText.SetText(health.ToString());

        // Set obstacle color
        changeColor.EvaluateColor(minHealth, maxHealth, health);

        // Spawn item above obstacle
        itemHolder.SpawnItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Return if other is not bullet
        if (!other.TryGetComponent(out Bullet bullet)) return;

        // Stop bullet and return it to it`s pool
        bullet.OnHit();

        TakeDamage(bullet.Damage);
    }

    public void TakeDamage(float damage)
    {
        // Decrease health
        health -= damage;

        // Update health text
        changeText.SetText(health.ToString());

        // Update obstacle color
        changeColor.EvaluateColor(minHealth, maxHealth, health);

        // Play impact animation if it is not playing
        if (!isPlayingImpactAnimaton)
            StartCoroutine(PlayImpact());

        // Drop item and destroy obstacle if health is less than min health 
        if (health <= minHealth)
            Kill();
    }

    private IEnumerator PlayImpact()
    {
        isPlayingImpactAnimaton = true;

        // Play particles
        impactParticleSystem.Play();

        // Play sound
        AudioManager.Instance.PlaySFX(AudioID.ObstacleImpact);

        // Play bounce in and out animation
        yield return Tweens.BounceInOut(transform, impactStrenght, impactAnimationDuration);

        isPlayingImpactAnimaton = false;
    }

    public void Kill()
    {
        // Drop item form obtacle
        itemHolder.DropItem(transform.position);

        // Destroy obstacle
        Destroy(gameObject);
    }

    public void ResetGameObject()
    {
        Destroy(gameObject);
    }
}
