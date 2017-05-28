using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingFloor : MonoBehaviour {

    public GameObject pitPrefab;

    public bool doCollapse = false;

    public float collapseDuration = 1.0f;
    SpriteRenderer floorSprite;

    bool doDestroy = false;

	// Use this for initialization
	void Start () {
        floorSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (doCollapse)
        {
            doCollapse = false;
            Collapse();
        }

        if (doDestroy)
        {
            Destroy(gameObject);
        }
	}

    public void Collapse()
    {
        StartCoroutine(DoCollapse());
    }

    IEnumerator DoCollapse()
    {
        float startTime = Time.time;
        float elapsed = 0.0f;
        float duration = collapseDuration;

        Color floorColor = floorSprite.color;

        while (elapsed < duration)
        {
            elapsed = Time.time - startTime;
            floorSprite.color = Color.Lerp(floorColor, Color.black, elapsed / duration);
            yield return new WaitForSeconds(0.1f);
        }

        GameObject pit = GameObject.Instantiate(pitPrefab, transform);
        pit.transform.parent = transform.parent;
    }
}
