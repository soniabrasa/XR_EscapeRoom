using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EinsteinPortrait : MonoBehaviour {
    private int hitCountdown = 2;
    public int HitCountdown { get{ return hitCountdown; }}

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bubble")) {
            hitCountdown--;
        }

        if(hitCountdown == 0) {
            GameManager.instance.CheckEinsteinKey();
        }
    }


}
