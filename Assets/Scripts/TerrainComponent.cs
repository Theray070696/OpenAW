using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

                if(!World.INSTANCE.Generating)
                {
                    UpdateSprite();
                    if(cbTerrainChanged != null)
                    {
                        cbTerrainChanged(this);
                    }
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

        StringBuilder spriteName = new StringBuilder(terrain.name).Append("_");

        World world = World.INSTANCE;
        
        TerrainComponent t = world.GetTerrainAt(x, y + 1);

        if(t != null && t.terrain.name.Equals(this.terrain.name))
        {
            spriteName.Append("N");
        }

        t = world.GetTerrainAt(x + 1, y);

        if(t != null && t.terrain.name.Equals(this.terrain.name))
        {
            spriteName.Append("E");
        }

        t = world.GetTerrainAt(x, y - 1);

        if(t != null && t.terrain.name.Equals(this.terrain.name))
        {
            spriteName.Append("S");
        }

        t = world.GetTerrainAt(x - 1, y);

        if(t != null && t.terrain.name.Equals(this.terrain.name))
        {
            spriteName.Append("W");
        }
        
        spriteRenderer.sprite = terrain.GetSpriteFromName(spriteName.ToString());
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
