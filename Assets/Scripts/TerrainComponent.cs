using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainComponent : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    public int x, y;
    
    [SerializeField]
    private Terrain _terrain;

    public Terrain terrain
    {
        get
        {
            return _terrain;
        }

        set
        {
            if(value != _terrain)
            {
                _terrain = value;
                UpdateSprite();
                if(cbTerrainChanged != null)
                {
                    cbTerrainChanged(this);
                }
            }
        }
    }

    public Action<TerrainComponent> cbTerrainChanged;

    public void UpdateSprite()
    {
        if(terrain == null || terrain.Sprites.Count == 0)
        {
            spriteRenderer.sprite = null;
            return;
        }

        if(!terrain.HasConnectedTextures)
        {
            spriteRenderer.sprite = terrain.GetSpriteFromName();
            return;
        }

        string spriteName = terrain.GetSpriteFromName().name;

        World world = World.INSTANCE;
        
        TerrainComponent t;
        t = world.GetTerrainAt(x, y + 1);

        if(t != null && t.terrain == this.terrain)
        {
            spriteName += "N";
        }

        t = world.GetTerrainAt(x + 1, y);

        if(t != null && t.terrain == this.terrain)
        {
            spriteName += "E";
        }

        t = world.GetTerrainAt(x, y - 1);

        if(t != null && t.terrain == this.terrain)
        {
            spriteName += "S";
        }

        t = world.GetTerrainAt(x - 1, y);

        if(t != null && t.terrain == this.terrain)
        {
            spriteName += "W";
        }
        
        spriteRenderer.sprite = terrain.GetSpriteFromName(spriteName);
    }
    
    public void RegisterTerrainChangedCallback(Action<TerrainComponent> callback)
    {
        cbTerrainChanged += callback;
    }

    public void UnregisterTerrainChangedCallback(Action<TerrainComponent> callback)
    {
        cbTerrainChanged -= callback;
    }
}
