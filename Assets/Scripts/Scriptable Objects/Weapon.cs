using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OpenAW/Weapon")]
public class Weapon : ScriptableObject
{
    public string Name;
    public ushort MinRange;
    public ushort MaxRange;
    public short Ammo;
    public float BaseDamage;
}
