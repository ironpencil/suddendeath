using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {
    public float FallTime = 2.0f;
    public GameObject ExplosionPrefab;
    public GameObject shadow;
    public AnimationCurve easing;

    Vector2 startingPos;
    Vector2 targetPos;
    float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
        targetPos = shadow.transform.position;
    }

    // Update is called once per frame
    void Update() {

        elapsedTime += Time.deltaTime;
        // Once we get to the shadow, blow up
        if (targetPos.y < transform.position.y)
        {
            float easedTime = easing.Evaluate(elapsedTime / FallTime);
            transform.position = Vector2.Lerp(startingPos, targetPos, easedTime);
        } else
        {
            Transform DynamicsParent = Globals.Instance.GetComponent<GameManager>().dynamicsParent;
            GameObject explosion = Instantiate(ExplosionPrefab, DynamicsParent);
            explosion.transform.position = targetPos;

            Destroy(shadow);
            Destroy(gameObject);
        }
    }
}
