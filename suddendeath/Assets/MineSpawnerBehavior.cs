using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawnerBehavior : MonoBehaviour {
    public GameObject MinePrefab;
    public Vector2 UpperSpawnBounds;
    public Vector2 LowerSpawnBounds;
    public int MaxMines;
    public float RespawnFrequency;
    private float NextRespawnTime;
    public int MineCount;

    private GameManager gm;

    // Use this for initialization
    void Start () {
        gm = Globals.Instance.gameObject.GetComponent<GameManager>();

        while (MineCount < MaxMines)
        {
            AddMine();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (MineCount < MaxMines)
        {
            if (NextRespawnTime < Time.time)
            {
                AddMine();
            }
        }
	}

    public void AddMine()
    {
        GameObject mine = GameObject.Instantiate(MinePrefab, gm.dynamicsParent);
        float x = UnityEngine.Random.Range(LowerSpawnBounds.x, UpperSpawnBounds.x + 1);
        float y = UnityEngine.Random.Range(LowerSpawnBounds.y, UpperSpawnBounds.y + 1);
        mine.transform.position = new Vector2(x, y);
        mine.GetComponent<MineBehavior>().MineSpawnerBehavior = this;
        MineCount++;
        
        if (MineCount < MaxMines)
        {
            NextRespawnTime = Time.time + RespawnFrequency;
        }
    }

    public void RemoveMine(GameObject Mine)
    {
        MineCount--;
        NextRespawnTime = Time.time + RespawnFrequency;
    }
}
