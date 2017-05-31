using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombSpawnerController : MonoBehaviour {
    public float frequency = 5;
    public float heightOffset = 20;
    public GameObject BombPrefab;
    public GameObject ShadowPrefab;
    public Vector2 UpperLeftBound;
    public Vector2 LowerRightBound;
    private float timeLeft = 0;

	// Use this for initialization
	void Start () {
        timeLeft = frequency;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            DropBomb();
            timeLeft = frequency;
        }
    }

    void DropBomb()
    {
        //float x = Random.Range(UpperLeftBound.x, LowerRightBound.x);
        //float y = Random.Range(UpperLeftBound.y, LowerRightBound.y);
        List<int> livingPlayerNums = Globals.Instance.GameManager.players.Keys.ToList();

        int targetPlayerNum = livingPlayerNums[Random.Range(0, livingPlayerNums.Count)];
        Debug.Log("Targeting Player: " + targetPlayerNum);
        Globals.Instance.GameManager.playerStats[targetPlayerNum].bombTargets++;
        PlayerController pc = Globals.Instance.GameManager.players[targetPlayerNum];

        Transform DynamicsParent = Globals.Instance.GetComponent<GameManager>().dynamicsParent;

        GameObject bombShadow = Instantiate(ShadowPrefab, DynamicsParent);
        bombShadow.transform.position = new Vector2(pc.transform.position.x, pc.transform.position.y);

        GameObject bomb = Instantiate(BombPrefab, DynamicsParent);
        bomb.transform.position = new Vector2(bombShadow.transform.position.x, bombShadow.transform.position.y + heightOffset);
        // TODO Use the euler angles
        bomb.transform.rotation = new Quaternion(0.0f, 0.0f, 1.0f, 0.0f);
        bomb.GetComponent<BombBehavior>().shadow = bombShadow;
    }
}
