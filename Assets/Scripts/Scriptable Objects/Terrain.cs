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
    public bool HasConnectedTextures;

    [Tooltip("Sprite 0 is assumed to be default sprite. Everything after should be used for connected textures")]
    public List<Sprite> Sprites;

    public Sprite GetSpriteFromName(string name = "")
    {
        if(Sprites.Count == 0)
        {
            return null;
        }
        
        if(name.Equals(""))
        {
            return Sprites[0];
        }
        
        foreach(Sprite S in Sprites) // There is 150% a better way to do this, but I'm just getting this working for now. (3/18/2020)
        {
            if(S.name.Equals(name))
            {
                return S;
            }
        }

        return Sprites[0];
    }
}
