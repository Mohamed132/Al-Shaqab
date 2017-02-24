using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour {
    public RectTransform[] RandomPositions;
    public float RandomRadius;


    PuzzlePiece[] PuzzlePieces;
	// Use this for initialization
	void Start () {
        PuzzlePieces = GetComponentsInChildren<PuzzlePiece>();
		foreach(var Piece in PuzzlePieces)
        {
            Piece.SetRandomPositionAndRotation(RandomPositions[Random.Range(0, RandomPositions.Length)].position, RandomRadius);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
