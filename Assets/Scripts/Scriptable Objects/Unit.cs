using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OpenAW/Unit")]
public class Unit : ScriptableObject
{
    public string Name;
    public List<Weapon> Weapons;
    public int BaseCost;
    public ushort MoveRange;
    public MovementType MovementType;
    public short Fuel;
    public short Vision;
}
