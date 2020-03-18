using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OpenAW/Terrain")]
public class Terrain : ScriptableObject
{
    public string Name;
    public short Defense;
    public short Funds;
    public UnitType RepairType;
    public List<MoveCost> MoveCosts;
}
