using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelToucher : MonoBehaviour {
    public void HapticFeedback() {
        transform.parent.GetComponent<ActionBasedController>().SendHapticImpulse(0.1f, 0.1f);
    }
}
