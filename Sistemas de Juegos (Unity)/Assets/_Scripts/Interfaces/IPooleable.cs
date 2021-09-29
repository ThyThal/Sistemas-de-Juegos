using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooleable
{
    bool Active { get; set; }
    object Owner { get; set; }
    void Reset();
    void Activate();
}
