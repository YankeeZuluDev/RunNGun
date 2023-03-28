using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class contains animations for weapons
/// </summary>
public class WeaponAnimationManager : MonoBehaviour
{
    [SerializeField] private float weaponAddingAnimationDuration;
    [SerializeField] private float weaponMergingAnimationDuration;

    public IEnumerator WeaponAddingAnimation(Transform transform, Transform targetTransform, Action onComplete)
    {
        yield return Tweens.SmoothlyMoveAndRotateTowards(transform, targetTransform, weaponAddingAnimationDuration);

        onComplete?.Invoke();
    }

    public IEnumerator WeaponMergingAnimation(Transform firstWeaponTransform, Transform secondWeaponTransform, Action onComplete) // onComplete // weapon1.transform //weapon2.transform //
    {
        // Move both weapons to common center
        yield return Tweens.MoveToCommonCenter(firstWeaponTransform, secondWeaponTransform, weaponMergingAnimationDuration);

        onComplete?.Invoke();
    }
}
