using UnityEngine;
using System.Collections;

public class bloodStain : MonoBehaviour {
    public float startTime;
    public float duration;

    private float currentFadeTime;
    private float currentFadeLevel;
    private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
        Invoke("remove",duration+startTime);
        currentFadeTime = duration+startTime;
        rend = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentFadeTime > 0) {
            currentFadeTime -= Time.deltaTime;
        }

        if (currentFadeTime < duration)
        {
            currentFadeLevel = currentFadeTime / (duration+Mathf.Epsilon);
            rend.material.color = new Color(1.0f, 1.0f, 1.0f, currentFadeLevel);
        }

    }

    void remove()
    {
        Destroy(gameObject);
    }
}
