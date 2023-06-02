using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
    public float timeToLive;    
    Vector3 maxScale;
    Vector3 startScale;
    Vector3 windVelocity;

    public int randomId;

    private float t;
    // Start is called before the first frame update
    void Start() {
        randomId = Random.Range(0, 100000000);
        timeToLive = 5 + Random.Range(0f, 5f);
        startScale = transform.localScale;
        maxScale = startScale  * (2 + Random.Range(0f, 0.5f));
        t=0;        
    }

    // Update is called once per frame
    void Update() {
        t += Time.deltaTime / timeToLive;
        transform.localScale = Vector3.Lerp(startScale, maxScale, t);

        if(t > 1) {
            Destroy(gameObject);
        }

        if(windVelocity != null) {
            transform.position += windVelocity * Time.deltaTime;
        }
    }

    public void SetWindVelocity(Vector3 windVelocity) {
        this.windVelocity = windVelocity * Random.Range(0.95f, 1.05f);
    }

    public void OnCollisionEnter(Collision other) {
        Debug.Log("Bubble OnCollisionEnter");
        if(other.gameObject.CompareTag("Bubble")) {
            Bubble otherBubble = other.gameObject.GetComponent<Bubble>();
            if(otherBubble.randomId < this.randomId) {
                Destroy(gameObject);
            } else {
                transform.localScale = transform.localScale * 1.5f;
            }
        } else {
            Destroy(gameObject);
        }
    }
}
