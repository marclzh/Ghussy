using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDameagable
{
    Health health { get; }

    void TakeDamage(float damageAmount);
}
