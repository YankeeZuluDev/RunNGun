using System.Collections.Generic;

/// <summary>
/// This class contains a collection of extension method
/// </summary>
public static class Extensions
{
    /// <summary>
    /// An extension method, that outputs weapon with the same configuration as the weapon from the list
    /// </summary>
    /// <returns>True if matching weapons was found, false if not</returns>
    public static bool TryGetWeapon(this List<Weapon> weapons, Weapon weapon, out Weapon matchingWeapon)
    {
        foreach (Weapon listWeapon in weapons)
        {
            // Return true if configuration matches
            if (listWeapon.WeaponConfiguration == weapon.WeaponConfiguration)
            {
                matchingWeapon = listWeapon;
                return true;
            }
        }

        // Return false if nothing was found
        matchingWeapon = null;
        return false;
    }

    /// <summary>
    /// An extension method, that checks if a list of audio ID-Clip pairs list has any duplicate audioID`s
    /// </summary>
    /// <returns>True if duplicate audio ID was found, false if not</returns>
    public static bool HasAnyDuplicateAudioIDs(this List<AudioIDClipPair> audioIDClipPairsList, out AudioID? duplicateID) // это точно я написал? Переделать с HashSet`ом
    {
        for (int i = 0; i < audioIDClipPairsList.Count; i++)
        {
            // Get current ID
            AudioID currentID = audioIDClipPairsList[i].ID;

            for (int j = i + 1; j < audioIDClipPairsList.Count; j++)
            {
                // Get other ID
                AudioID otherID = audioIDClipPairsList[j].ID;

                // Return true if audio IDs are matching
                if (currentID == otherID)
                {
                    duplicateID = otherID;
                    return true;
                }
            }
        }

        // Return false if nothing was found
        duplicateID = null;
        return false;
    }
}
