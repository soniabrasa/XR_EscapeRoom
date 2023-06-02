using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoBalanza : MonoBehaviour
{
    public delegate void WeightChangedDelegate(bool isWeightCorrect);
    public WeightChangedDelegate OnWeightChanged;

    public float rightDishWeight;

    float leftDishWeight, maxWeightDifference;

    // Máximo ángulo de balanceo do brazo
    float maxAngle;

    // Ángulo de destino do brazo da balanza
    float targetAngle;

    // Velocidade de rotación
    float angularSpeed;

    List<SmartWeightProvider> leftDishContent;

    // Start is called before the first frame update
    void Start()
    {
        leftDishContent = new List<SmartWeightProvider>();
        maxWeightDifference = 0.4f;
        leftDishWeight = 0;

        ZonaSensibleHandler.OnObjectEnter += AddObject;
        ZonaSensibleHandler.OnObjectExit += RemoveObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Reacción a info publicada polo delegante OnObjectEnter
    void AddObject(SmartWeightProvider swp)
    {
        // Se xa existe na lista non se ten en conta
        if (leftDishContent.Contains(swp)) { return; }

        // Agrégase á lista de obxetos a pesar
        leftDishContent.Add(swp);

        Debug.Log($"{gameObject.name}.AddObject({swp.gameObject.name} getWeight + {swp.GetWeigth()})");

        // Actualízase o peso total do PratoBalanza esquerdo
        UpdateLeftDishWeight();
    }

    // Reacción a info publicada polo delegante OnObjectExit
    void RemoveObject(SmartWeightProvider swp)
    {
        // Elimínase da lista de obxetos a pesar
        leftDishContent.Remove(swp);

        Debug.Log($"{gameObject.name}.RemoveObject({swp.gameObject.name} getWeight - {swp.GetWeigth()})");

        // Actualízase o peso total do PratoBalanza esquerdo
        UpdateLeftDishWeight();
    }


    void UpdateLeftDishWeight()
    {
        leftDishWeight = 0;

        foreach (SmartWeightProvider swp in leftDishContent)
        {
            leftDishWeight += swp.GetWeigth();
        }

        if (OnWeightChanged != null)
        {
            OnWeightChanged(rightDishWeight == leftDishWeight);
        }

        Debug.Log($"{gameObject.name}.UpdateLeftDishWeight = {leftDishWeight})");
    }

    void SetArmAngle()
    {
        float weightRatio = (rightDishWeight - leftDishWeight) / (leftDishWeight + rightDishWeight);

        targetAngle = Mathf.Clamp(weightRatio, -maxWeightDifference, maxWeightDifference) / maxWeightDifference * maxAngle;

    }
}
