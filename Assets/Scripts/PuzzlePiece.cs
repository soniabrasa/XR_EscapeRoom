using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzlePiece : MonoBehaviour {
    private InteractionLayerMask initialInteractionLayerMask;
    private InteractionLayerMask noHandsInteractionLayerMask;

    private XRGrabInteractable xrGrabInteractable;
    // Start is called before the first frame update
    void Start() {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();

        initialInteractionLayerMask = xrGrabInteractable.interactionLayers;
        noHandsInteractionLayerMask = InteractionLayerMask.GetMask("NoInteractionWithHands");
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetMaterial(Material material) {
        GetComponent<MeshRenderer>().material = material;
    }

    public void SetHandInteraction(bool active) {
        if(active) {
            xrGrabInteractable.interactionLayers = initialInteractionLayerMask;
        } else {
            xrGrabInteractable.interactionLayers =  noHandsInteractionLayerMask;
        }
    }
}
