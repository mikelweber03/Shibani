using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimController
{
    void TurretFire(); //Function without any arguments
    float health { get; set; } //A variable
    void ApplyDamage(float points); //Function with one argument

    void CheckDistanceToPlayer();
}
