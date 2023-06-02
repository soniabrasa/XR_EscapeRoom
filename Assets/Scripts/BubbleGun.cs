using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : MonoBehaviour {
    public GameObject bubblePrefab;
    public Transform bubbleSpawnPoint;
    private bool bubbleGunActive = false;

    private Vector3 windVelocity;
    // Start is called before the first frame update
    void Start() {
        if(bubblePrefab == null) {
            Debug.Log("BubbleGun. A variable bubblePrefab non está correctamente inicializada");
        }
        if(bubbleSpawnPoint == null) {
            Debug.Log("BubbleGun. A variable bubbleSpawnPoint non está correctamente inicializada");
        }
        StartCoroutine(SpawningCoroutine());

        windVelocity = RandomWindVelocity();
        
    }

    // Update is called once per frame
    void Update()  {
        
    }

    public void ShotBubble() {
        GameObject bubbleGO = Instantiate(bubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
        Bubble bubble = bubbleGO.GetComponent<Bubble>();
        bubble.SetWindVelocity(transform.forward * 2);
    }

    public void SpawnBubbles(bool active) {
        bubbleGunActive = active;
    }

    private IEnumerator SpawningCoroutine() {
        while(true) {
            if(bubbleGunActive) {
                GameObject bubbleGO = Instantiate(bubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
                Bubble bubble = bubbleGO.GetComponent<Bubble>();
                bubble.SetWindVelocity(windVelocity);
            }
            yield return new WaitForSeconds(0.2f);

        }
    }

    private Vector3 RandomWindVelocity() {
        Vector2 windDirection = Random.insideUnitCircle;
        windDirection.Normalize();
        return new Vector3(windDirection.x, 0, windDirection.y) * Random.Range(0.05f, 0.25f);
    }

}
