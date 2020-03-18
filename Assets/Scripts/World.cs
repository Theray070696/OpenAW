using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Random = System.Random;

public class World : MonoBehaviour
{
    public List<Terrain> Terrains;
    public GameObject TerrainGameObjectPrefab;
    public Camera C;

    [NonSerialized]
    public bool Generating = false;

    public static World INSTANCE;
    
    TerrainComponent[,] terrainXY;

    public int Width { get; protected set; }
    public int Height { get; protected set; }

    Action<TerrainComponent> cbTerrainChanged;

    void Awake()
    {
        if(INSTANCE != null)
        {
            Debug.LogError("Only one World may exist at any time!", gameObject);
            return;
        }

        INSTANCE = this;
    }

    void Start()
    {
        CreateWorldOfSize();
    }

    void OnDestroy()
    {
        if(INSTANCE == this)
        {
            INSTANCE = null;
        }
    }

    public void CreateWorldOfSize(int width = 100, int height = 100)
    {
        Generating = true;
        
        this.Width = width;
        this.Height = height;

        this.terrainXY = new TerrainComponent[Width, Height];

        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                TerrainComponent TC = Instantiate(TerrainGameObjectPrefab, transform).GetComponent<TerrainComponent>();
                
                TC.transform.position = new Vector3(x, y, 0f);
                TC.gameObject.name = new StringBuilder("Tile_").Append(x).Append("_").Append(y).ToString();
                TC.x = x;
                TC.y = y;
                
                terrainXY[x, y] = TC;
                
                terrainXY[x, y].RegisterTerrainChangedCallback(OnTerrainChanged);

                // Temp code to make sure connected textures properly function
                if(UnityEngine.Random.Range(0, 10) == 0)
                {
                    terrainXY[x, y].terrain = Terrains[0];
                } else
                {
                    terrainXY[x, y].terrain = Terrains[1];
                }
            }
        }

        Generating = false;

        foreach(TerrainComponent TC in terrainXY)
        {
            TC.UpdateSprite();
        }

        Debug.Log("World created with " + Width * Height + " tiles.");
        
        C.transform.position = new Vector3(width / 2f, height / 2f, C.transform.position.z);
    }

    public void CreateWorldOfType(Terrain terrain)
    {
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                terrainXY[x, y].terrain = terrain;
            }
        }
    }

    public TerrainComponent GetTerrainAt(int x, int y)
    {
        if(x < 0 || x >= Width || y < 0 || y >= Height)
        {
            //Debug.Log("Tile at coordinates (" + x + ", " + y + ") is null!");
            return null;
        }

        return terrainXY[x, y];
    }

    public void RegisterTerrainChanged(Action<TerrainComponent> callback)
    {
        cbTerrainChanged += callback;
    }

    public void UnregisterTerrainChanged(Action<TerrainComponent> callback)
    {
        cbTerrainChanged -= callback;
    }

    void OnTerrainChanged(TerrainComponent t)
    {
        if(cbTerrainChanged == null)
        {
            return;
        }
        
        cbTerrainChanged(t);
    }
}