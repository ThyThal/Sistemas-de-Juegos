using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void SetOwner(Weapon owner);
    void OnTriggerEnter(Collider other);
    void Travel();
}
