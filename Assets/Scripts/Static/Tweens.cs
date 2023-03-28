using System.Collections;
using UnityEngine;

/// <summary>
/// This class contains a collection of tweens
/// </summary>
public static class Tweens
{
    /// <summary>
    /// Smoothly changes position and rotation to the targetPosition and targetRotation
    /// </summary>
    public static IEnumerator SmoothlyMoveAndRotateTowards(Transform transform, Transform targetTransform, float duration)
    {
        float elapsedTime = 0;

        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < duration)
        {
            // Move
            transform.position = Vector3.Lerp(startingPosition, targetTransform.position, elapsedTime / duration);

            // Rotate
            transform.rotation = Quaternion.Slerp(startingRotation, targetTransform.rotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that position and rotation are set to their target values at the end
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }

    /// <summary>
    /// Smoothly bounce in and the bounce out
    /// </summary>
    public static IEnumerator BounceInOut(Transform transform, float strenght, float duration)
    {
        float halfDuration = duration / 2f;
        float elapsedTime = 0;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x - strenght, originalScale.y - strenght, originalScale.z);

        // Bounce in
        while (elapsedTime < halfDuration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, elapsedTime / halfDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Bounce out
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, elapsedTime / duration);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Move both transforms to mid point between them
    /// </summary>
    public static IEnumerator MoveToCommonCenter(Transform firstTransform, Transform secondTransform, float duration)
    {
        float elapsedTime = 0;

        // Calculate mid point
        Vector3 center = (firstTransform.position + secondTransform.position) / 2f; // ок, а если 3 и через params сделать?

        // original position
        Vector3 firstOriginalPosition = firstTransform.position;
        Vector3 secondOriginalPosition = secondTransform.position;

        while (elapsedTime < duration)
        {
            // Move first
            firstTransform.position = Vector3.Lerp(firstOriginalPosition, center, elapsedTime / duration);

            // Move second
            secondTransform.position = Vector3.Lerp(secondOriginalPosition, center, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}