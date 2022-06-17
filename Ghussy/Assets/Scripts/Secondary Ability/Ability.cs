using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : ScriptableObject
{
    [SerializeField] private string abilityName;
    [SerializeField] private Sprite abilityImage;
    [SerializeField] private float cooldownTime;

    public virtual void Activate() { }

    public Sprite GetImage() { return abilityImage; }
    public float GetCooldownTime() { return cooldownTime; }
}
