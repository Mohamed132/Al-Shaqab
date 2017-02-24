using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    public int NoOfPieces = 35;
    public RectTransform ParentCanvas;
    public RectTransform[] RandomPositions;
    public RectTransform DragingLimitRect;
    public RectTransform SelectedPieceParent;
    public float RandomRadius;
    public static PuzzleGenerator singleton;


    private int SolvedPiecesCounter = 0 ;
    PuzzlePiece[] PuzzlePieces;

    private void Awake()
    {
        if(!singleton)
        {
            singleton = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        Vector2 MinDraging = (Vector2)DragingLimitRect.position + DragingLimitRect.rect.min * ParentCanvas.localScale.x;
        Vector2 MaxDraging = (Vector2)DragingLimitRect.position + DragingLimitRect.rect.max * ParentCanvas.localScale.x;
        PuzzlePieces = GetComponentsInChildren<PuzzlePiece>();
        foreach (var Piece in PuzzlePieces)
        {
            Piece.RandomizeThePiece(RandomPositions[Random.Range(0, RandomPositions.Length)].position, RandomRadius, MinDraging, MaxDraging, SelectedPieceParent);
        }
    }

    public void OnPieceSolved()
    {
        SolvedPiecesCounter++;
    }
}
