// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// Copyright Utrecht University (Department of Information and Computing Sciences)

using UnityEngine;

/// <summary>
/// Key item, which can be used to open doors.
/// </summary>
public class Key : Item
{
    /// <summary>
    /// The key ID, to check which door it can open.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Set the key id and color.
    /// </summary>
    /// <param name="id">The key id, which should match to the door id.</param>
    /// <param name="color">The color of the key, which should match to the door color.</param>
    public void Initialize(int id, Color color)
    {
        Id = id;
        GetComponent<Renderer>().material.color = color;
    }

    private void Start()
    {
        stackable = false;
        uses = 1;
        usesAddedPerPickup = 1;
    }
}
