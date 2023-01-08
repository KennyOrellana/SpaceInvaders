using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpaceshipDB : ScriptableObject
{
    public Spaceship[] spaceships;

    public int SpaceshipCount
    {
        get
        {
            return spaceships.Length;
        }
    }

    public Spaceship GetSpaceship(int index)
    {
        return spaceships[index];
    }
}

