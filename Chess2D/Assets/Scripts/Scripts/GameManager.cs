using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Board mBoard;
    public PieceManager mPieceManager;

	void Start () {

        CreateBoard();
        CreatePieces();

    }

    private void CreateBoard() {

        if (mBoard != null){
            mBoard.Create();
        }

    }

    private void CreatePieces() { 

            mPieceManager.Setup(mBoard);

    }

}
