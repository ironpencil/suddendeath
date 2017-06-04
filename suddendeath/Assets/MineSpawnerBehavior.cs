using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<PlayerController> players = Globals.Instance.GameManager.livingPlayers.Values.ToList();

        GameObject mine = GameObject.Instantiate(MinePrefab, gm.dynamicsParent);

        bool mineOnPlayer = true;

        while (mineOnPlayer)
        {
            float x = UnityEngine.Random.Range(LowerSpawnBounds.x, UpperSpawnBounds.x + 1);
            float y = UnityEngine.Random.Range(LowerSpawnBounds.y, UpperSpawnBounds.y + 1);

            Vector2 minePos = new Vector2(x, y);

            if (!players.Any(p => Vector2.Distance(p.transform.position, minePos) < 2.0f))
            {
                mineOnPlayer = false;
            }

            mine.transform.position = new Vector2(x, y);
        }

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
