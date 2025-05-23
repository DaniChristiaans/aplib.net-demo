// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// Copyright Utrecht University (Department of Information and Computing Sciences)

using Assets.Scripts.Doors;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a keyring that stores keys and can be queried to check if a key is present.
/// </summary>
public class KeyRing : MonoBehaviour
{
    private readonly List<Key> _keyRing = new();

    /// <summary>
    /// Adds a key to the keyring.
    /// </summary>
    /// <param name="key">The key to be stored.</param>
    public void StoreKey(Key key) => _keyRing.Add(key);

    /// <summary>
    /// Checks the ID of all keys in the keyring against the inputted doorId, if a match is found, true is returned and the key
    /// is consumed.
    /// </summary>
    /// <param name="door">The door that is checked against all the keys in the keyring.</param>
    /// <returns>True if the correct key is present otherwise False.</returns>
    public bool KeyQuery(Door door)
    {
        Key key = _keyRing.Find(door.KeyMatchesDoor);

        return _keyRing.Remove(key);
    }

    /// <summary>
    /// Checks if given key is in the inventory.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool HasKey(int keyNumber) => _keyRing.Find(k => k.Id == keyNumber);

}
