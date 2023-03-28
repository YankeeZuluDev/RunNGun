using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This is a class for bullet
/// </summary>
public class Bullet : MonoBehaviour
{
    private const float minDistanceThreshold = 0.1f;

    private float damage;
    private Action returnToPoolAction;

    public Action ReturnToPoolAction { get => returnToPoolAction; set => returnToPoolAction = value; }
    public float Damage { get => damage; set => damage = value; }

    public void Move(Vector3 targetPosition, float shotSpeed)
    {
        StartCoroutine(MoveCoroutine(targetPosition, shotSpeed));
    }

    public IEnumerator MoveCoroutine(Vector3 targetPosition, float shotSpeed)
    {
        // Move bullet towards targetPosition while distance between bullet and target position bigger that minimal threshold
        while (Vector3.Distance(transform.position, targetPosition) > minDistanceThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, shotSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure that position is set to it`s target value at the end
        transform.position = targetPosition;

        // Invoke return to pool action
        returnToPoolAction?.Invoke();
    }

    public void OnHit()
    {
        // Stop MoveCoroutine if bullet hit damageable before it reached target position
        StopAllCoroutines();

        // Invoke return to pool action
        returnToPoolAction?.Invoke();
    }
}
