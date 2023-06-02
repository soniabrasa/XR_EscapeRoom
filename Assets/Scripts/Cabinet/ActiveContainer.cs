using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveContainer : MonoBehaviour
{
    // Script que controla si la puerta se puede abrir o está cerrada con llave
    public CabinetDoor cabinetDoor;

    bool doorLocked;

    // Lista de objectos ocultos tras la puerta del armario
    List<GameObject> objectsInCabinet;


    void Start()
    {
        // Puerta bloqueada ( valor inicial )
        doorLocked = true;

        // Lista de objectos dentro de este gameObject Cabinet
        objectsInCabinet = new List<GameObject>();

        // Suscripción al delegate que avisa de los cambios en la cerradura de puerta del gameObject
        cabinetDoor.OnIsDoorLocked += ChangeLock;
    }

    void Update() { }

    // Reacción a la info publicada por el delegate CabinetDoor.OnIsDoorLocked
    private void ChangeLock(bool locked)
    {
        // Dev. Para saber qué Cabinet envía la info del delegate
        Debug.Log($"{gameObject.name}.ActiveContainer.ChangeLock({locked}) recibido del delegate");

        // Actualizamos this bool de puerta bloqueada con la info de la cerradura
        doorLocked = locked;

        // Se activan los GameObjects cuando la cerradura NO esté bloqueada (y a la inversa)
        ActivateObjects(!doorLocked);
    }

    // Recorrer los GameObject de tipo Collider de la lista para des/activación
    void ActivateObjects(bool activation)
    {
        foreach (GameObject go in objectsInCabinet)
        {
            ActivateOneObject(go, activation);
        }
    }

    // Des/Activación de cada GameObject parent de la lista
    void ActivateOneObject(GameObject go, bool activation)
    {
        Debug.Log($"{gameObject.name}.ActiveContainer.ActivateOneObject({go.name}, {activation})");

        GameObjectSwitch gs = go.GetComponent<GameObjectSwitch>();

        if (gs != null)
        {
            gs.SetActive(activation);
            Debug.Log($"\t {gs.name}.SetActive({activation})");
            Debug.Log($"\t o PAI do obxecto {go.name} foi activado a {activation}");
        }

        else
        {
            go.SetActive(activation);
            Debug.Log($"\t {go.name}.SetActive({activation})");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if (((1 << other.gameObject.layer) & layerMask.value) != 0)
        Debug.Log($"{gameObject.name}.ActiveContainer.OnTriggerEnter({other.gameObject.name})");

        // Agregamos other a la lista de GameObjects dentro del trigger
        objectsInCabinet.Add(other.gameObject);

        // Si la puerta está bloqueada, se desactiva other para evitar la interacción con él
        // P.ej., atravesando los collider con las gafas (verlos) o las manos (cogerlos)

        if (doorLocked)
        {
            // other.gameObject.SetActive(false);
            ActivateOneObject(other.gameObject, false);
        }

        // Comprobar por consola que other pasa a Active false
        Debug.Log($"\t SetActive({other.gameObject.activeSelf})");
    }

    void OnTriggerExit(Collider other)
    {
        // if (((1 << other.gameObject.layer) & layerMask.value) != 0)
        Debug.Log($"{gameObject.name}.ActiveContainer.OnTriggerExit({other.gameObject.name})");

        // Eliminamos other de la lista de GameObjects dentro del trigger
        objectsInCabinet.Remove(other.gameObject);
    }
}
