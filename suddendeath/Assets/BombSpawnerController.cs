using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerController : MonoBehaviour {
    public float frequency = 5;
    public float startHeight = 10;
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
        float x = Random.Range(UpperLeftBound.x, LowerRightBound.x);
        float y = Random.Range(UpperLeftBound.y, LowerRightBound.y);
        GameObject bombShadow = Instantiate(ShadowPrefab, new Vector3(x, y, 0.0f), transform.rotation);
        GameObject bomb = Instantiate(BombPrefab, new Vector3(x, startHeight, 0.0f), new Quaternion(0.0f, 0.0f, 1.0f, 0.0f));
        bomb.GetComponent<BombBehavior>().shadow = bombShadow;
    }
}
