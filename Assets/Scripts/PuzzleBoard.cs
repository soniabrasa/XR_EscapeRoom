using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleBoard : MonoBehaviour {
    private XRSocketInteractor[] puzzleSockets;
    private List<PuzzlePiece> onBoardPuzzlePieces;
    // Start is called before the first frame update
    void Start() {
        puzzleSockets = GetComponentsInChildren<XRSocketInteractor>();
        onBoardPuzzlePieces = new List<PuzzlePiece>();
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnSelectEntered() {
        SetPuzzlePieceHandInteraction(true);
    }

    public void OnSelectExited() {
        SetPuzzlePieceHandInteraction(false);
    }

    private void SetPuzzlePieceHandInteraction(bool active) {
        onBoardPuzzlePieces = GetPiecesInSockets();
       
        foreach(PuzzlePiece puzzlePiece in onBoardPuzzlePieces) {
            puzzlePiece.SetHandInteraction(active);
        }
    }

    private List<PuzzlePiece> GetPiecesInSockets() {
        List<PuzzlePiece> foundPuzzlePieces = new List<PuzzlePiece>();

        foreach(XRSocketInteractor xrsi in puzzleSockets) {
            XRBaseInteractable puzzlePieceInteractable = xrsi.selectTarget;
            if(puzzlePieceInteractable != null) {
                PuzzlePiece puzzlePiece = puzzlePieceInteractable.GetComponent<PuzzlePiece>();
                foundPuzzlePieces.Add(puzzlePiece);
            }

        }

        return foundPuzzlePieces;
    }
 }
