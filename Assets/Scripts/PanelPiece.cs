using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPiece : MonoBehaviour {
    private List<Material> materials;
    int materialIndex;
    public int MaterialIndex { get { return materialIndex; } }
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }



    public void SetMaterials(List<Material> materials) {
        this.materials = materials;
        //Vestimos a peza do panel con un dos novos materiais recibidos, escollido ó azar
        materialIndex = Random.Range(0, materials.Count);
        GetComponent<MeshRenderer>().material = materials[materialIndex];
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("PanelPiece.OnTriggerEnter " + other.gameObject.tag);
        if(other.gameObject.CompareTag("PanelToucher")) {
            NextMaterial();
            GameManager.instance.CheckColorKey();
            other.GetComponent<PanelToucher>().HapticFeedback();
        }
    }

    private void NextMaterial() {
        materialIndex = (materialIndex+1) % materials.Count;
        GetComponent<MeshRenderer>().material = materials[materialIndex];
    }
}
