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
        Color tempColor = floorColor;

        GameObject pit = GameObject.Instantiate(pitPrefab, transform);
        pit.transform.parent = transform.parent;
        PitController pitController = pit.GetComponent<PitController>();
        pitController.ActivatePit(false);

        Vector2 floorScale = floorSprite.transform.localScale;

        while (elapsed < duration)
        {
            elapsed = Time.time - startTime;
            floorSprite.transform.localScale = Vector2.Lerp(floorScale, Vector2.zero, elapsed / duration);
            //tempColor.a = Mathf.Lerp(floorColor.a, 0, elapsed / duration);
            //floorSprite.color = tempColor;
            yield return new WaitForSeconds(0.1f);
        }

        pitController.ActivatePit(true);
        
    }
}
