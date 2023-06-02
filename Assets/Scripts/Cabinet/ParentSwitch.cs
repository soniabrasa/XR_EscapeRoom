using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSwitch : GameObjectSwitch
{
    public override void SetActive(bool active)
    {
        // Operador de "nulabilidad condicional" o "operador de navegación segura" (?)
        // Comprueba si un objeto es nulo antes de intentar acceder a una propiedad o método en él
        // Si transform.parent es nulo devuelve null (En vez de arrojar NullReferenceException)
        // Si no, se ejecuta el resto

        transform.parent?.gameObject.SetActive(active);
    }
}
