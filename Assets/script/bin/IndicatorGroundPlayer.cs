using UnityEngine;
using System.Collections;

public class IndicatorGroundPlayer : MonoBehaviour {
    public float safeDistance;

    private LevelManager theLevelManager;
    private Vector3 wormPosition;
    private float angle;
    private float distance;
    private SpriteRenderer arrowRenderer;
    // private bool wormInDeep;
    // Use this for initialization
    void Start () {
        theLevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        wormPosition = theLevelManager.wormPosition;
        arrowRenderer = transform.Find("WithArrow").GetComponent<SpriteRenderer>();
        angle = 0;
        distance = 255;
	}

    // Update is called once per frame
    void Update() {
        // update worm depth for indicator.
        this.gameObject.GetComponent<Animator>().SetFloat("depth", theLevelManager.depth);
        // rotate for worm position.
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (distance < safeDistance)
        {
            float redLevel = (safeDistance - distance) / safeDistance;
            arrowRenderer.color = new Color(1f, 1 - redLevel, 1 - redLevel, 1f);
        }
        else
        {
            arrowRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    
    void FixedUpdate() {
        wormPosition = theLevelManager.wormPosition;
        angle = 90 + Mathf.Atan2((transform.position.y - wormPosition.y), (transform.position.x - wormPosition.x)) * 180 / Mathf.PI;
        distance = Vector3.Distance(transform.position, wormPosition);
    }
}
