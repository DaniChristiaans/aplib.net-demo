// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// Copyright Utrecht University (Department of Information and Computing Sciences)

using System;
using Unity.AI.Navigation;
using UnityEngine;

/// <summary>
/// This component builds the nav mesh on awake.
/// </summary>
[RequireComponent(typeof(NavMeshSurface))]
public class BuildNavMesh : MonoBehaviour
{
    void Start()
    {
        Console.WriteLine("Generating navmesh...");
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
