using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Material> materials;
    public List<PuzzlePiece> puzzlePieces;
    public List<PanelPiece> panelPieces;

    public EinsteinPortrait einsteinPortrait;

    public List<GameObject> doorRedLights;
    public List<GameObject> doorGreenLights;

    public BrazoBalanza brazoBalanza;
    private int[] colorKey;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (materials == null || materials.Count == 0)
        {
            Debug.Log("GameManager: A variable materials non está correctamente inicializada");
        }

        if (puzzlePieces == null || puzzlePieces.Count == 0)
        {
            Debug.Log("GameManager: A variable puzzlePieces non está correctamente inicializada");
        }

        if (panelPieces == null || panelPieces.Count == 0)
        {
            Debug.Log("GameManager: A variable panelPieces non está correctamente inicializada");
        }

        colorKey = new int[puzzlePieces.Count];

        brazoBalanza.OnWeightChanged += CheckWeightKey;

        InitializePuzzlePieces();

    }

    // Update is called once per frame
    void Update()
    {
        CheckColorKey();
    }

    private void InitializePuzzlePieces()
    {
        string clave = "";
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            colorKey[i] = Random.Range(0, materials.Count);
            puzzlePieces[i].SetMaterial(materials[colorKey[i]]);
            clave += colorKey[i] + ", ";

            panelPieces[i].SetMaterials(materials);
        }

        Debug.Log("GameManager colorKey " + clave);

    }
    private void TurnLightGreen(int lightIndex, bool green)
    {
        doorRedLights[lightIndex].SetActive(!green);
        doorGreenLights[lightIndex].SetActive(green);
    }



    public void CheckColorKey()
    {
        bool isKeyCorrect = true;
        for (int i = 0; i < panelPieces.Count; i++)
        {
            if (panelPieces[i].MaterialIndex != colorKey[i])
            {
                isKeyCorrect = false;
                break;
            }
        }

        TurnLightGreen(0, isKeyCorrect);
    }

    public void CheckEinsteinKey()
    {
        TurnLightGreen(1, einsteinPortrait.HitCountdown == 0);
    }

    void CheckWeightKey(bool isKeyCorrect)
    {
        TurnLightGreen(2, isKeyCorrect);
    }
}
