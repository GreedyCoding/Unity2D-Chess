using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Board mBoard;
    public PieceManager mPieceManager;

    //Gets called when the game is started
    void Start () {

        CreateBoard();
        CreatePieces();

    }

    private void CreateBoard() {

       //If there is no board created create a new board
       if (mBoard != null){
            mBoard.Create();
       }

    }

    private void CreatePieces() {

            //Setup the pieces on the passed in board    
            mPieceManager.Setup(mBoard);
            Debug.Log("Setup the pieces on the Board");

    }

}
