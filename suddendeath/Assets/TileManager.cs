using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject tilePrefab;

    public Vector2 arenaSize = new Vector2(32, 18);
    public Vector2 arenaUpperLeft = new Vector2(-16, 9);

    List<CollapsingFloor> tiles;

    public float collapseTime = 1.0f;
    float lastCollapse = 0.0f;

    public int timeGroupSize = 16;
    public float timeGroupAdjust = 0.5f;

    int tilesCollapsedInGroup = 0;

	// Use this for initialization
	void Start () {
        tiles = new List<CollapsingFloor>();
        
        for (int x = 0; x < arenaSize.x; x++)
        {
            int tileX = (int)arenaUpperLeft.x + x;
            for (int y = 0; y < arenaSize.y; y++)
            {
                int tileY = (int)arenaUpperLeft.y - y;

                GameObject newTile = GameObject.Instantiate(tilePrefab, transform);
                newTile.transform.position = new Vector2(tileX, tileY);
                tiles.Add(newTile.GetComponent<CollapsingFloor>());
            }
        }

        lastCollapse = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (tiles.Count > 0 && Time.time - lastCollapse > collapseTime)
        {
            int tileIndex = Random.Range(0, tiles.Count);
            CollapsingFloor tile = tiles[tileIndex];
            tiles.RemoveAt(tileIndex);
            tile.Collapse();
            tilesCollapsedInGroup++;

            if (tilesCollapsedInGroup >= timeGroupSize)
            {
                collapseTime += timeGroupAdjust;
                tilesCollapsedInGroup = 0;
            }

            lastCollapse = Time.time;
            
        }
		
	}
}
