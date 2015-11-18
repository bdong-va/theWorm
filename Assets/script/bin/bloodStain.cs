using UnityEngine;
using System.Collections;

public class bloodStain : MonoBehaviour {

    public float duration;

    private float currentFadeTime;
    private float currentFadeLevel;
    private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
        Invoke("remove",duration);
        currentFadeTime = duration;
        rend = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        currentFadeTime -= Time.deltaTime;
        currentFadeLevel = currentFadeTime / duration;
        rend.material.color = new Color(1.0f, 1.0f, 1.0f, currentFadeLevel);

	}

    void remove()
    {
        Destroy(gameObject);
    }
}
