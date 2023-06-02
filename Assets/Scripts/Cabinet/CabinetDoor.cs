using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    // Delegate que informa si la puesta está cerrada con llave o no
    public delegate void OnIsDoorLockedDelegate(bool locked);
    public OnIsDoorLockedDelegate OnIsDoorLocked;

    public HingeJoint hinge;
    JointLimits openDoorLimits, closeDoorLimits;


    void Start()
    {
        // Array hinge.limits
        openDoorLimits = hinge.limits;
        closeDoorLimits = openDoorLimits;

        // Para bloquear la bisagra se inicializa el limite.max a 0
        closeDoorLimits.max = 0;
        // .min = .max = 0;
        hinge.limits = closeDoorLimits;
    }

    // XR Socket Interactor > Interactor Events
    public void Lock(bool locked)
    {
        // Cada vez que XR Sochet Interactor lanza el evento se publica el nuevo estado
        Debug.Log($"{gameObject.name}.CabinetDoor.Lock({locked}) XR Socket Interactor");

        if (OnIsDoorLocked != null)
        {

            // Publicación del nuevo estado
            OnIsDoorLocked(locked);

            Debug.Log($"\t OnIsDoorLocked({locked}) DELEGATE \n\n");
        }

        if (locked)
        {
            hinge.limits = closeDoorLimits;
        }

        else
        {
            hinge.limits = openDoorLimits;
        }
    }
}
