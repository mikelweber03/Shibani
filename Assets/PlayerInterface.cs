using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStats
{
    Vector3 CurrentPosition { get; }
    void Position(Vector3 position);
}