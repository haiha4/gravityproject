using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;
    public int witdh = 256;
    public int height = 256;

    public int scale = 20;

    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start() {
        offsetX = Random.Range(0f, 999f);
        offsetY = Random.Range(0f, 999f);
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offsetX += Time.deltaTime * 5f;
    }

    TerrainData GenerateTerrain (TerrainData terrainData) {
        terrainData.heightmapResolution = witdh + 1;

        terrainData.size = new Vector3(witdh, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights () {
        float[,] heights = new float[witdh, height];
        for (int x = 0; x < witdh; x++) {
            for (int y = 0; y < height; y++) {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight (int x, int y) {
        float xCoord = (float)x / witdh * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
