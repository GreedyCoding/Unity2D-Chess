using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour {

    public GameObject mPiecePrefab;

    //List for the white pieces initally set to null
    private List<BasePiece> mWhitePieces = null;
    //List for the black pieces initally set to null
    private List<BasePiece> mBlackPieces = null;

    //Laying out the Pieceorder in a String Array
    private string[] mPieceOrder = new string[16]  {
        "P", "P", "P", "P", "P", "P", "P", "P",
        "R", "KN", "B", "K", "Q", "B", "KN", "R"
    };

    //Creating Dictionary that links the Strings from the Pieceorder to the actual Piecetypes
    private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>() {
        {"P",  typeof(Pawn)},
        {"R",  typeof(Rook)},
        {"KN", typeof(Knight)},
        {"B",  typeof(Bishop)},
        {"K",  typeof(King)},
        {"Q",  typeof(Queen)}
    };

    public void Setup(Board board) {

        //Creating black and white pieces
        mWhitePieces = CreatePieces(Color.white, new Color32(80, 124, 160, 255), board);
        mBlackPieces = CreatePieces(Color.black, new Color32(210, 90, 65, 255), board);

        PlacePieces(1, 0, mWhitePieces, board);
        PlacePieces(6, 7, mBlackPieces, board);

    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor, Board board) {

        //Creating a temporary list of the new pieces to return
        List<BasePiece> tempPieces = new List<BasePiece>();

        //Setting up all the pieces
        for (int i = 0; i < mPieceOrder.Length; i++) {

            //Create new Object from Piece Prefab
            GameObject newPieceObject = Instantiate(mPiecePrefab);
            newPieceObject.transform.SetParent(transform);

            //Set Scale and position
            newPieceObject.transform.localScale = new Vector3(1, 1, 1);
            newPieceObject.transform.localRotation = Quaternion.identity;

            //Get the type and apply it to the new object
            string key = mPieceOrder[i];
            Type pieceType = mPieceLibrary[key];

            //Store new piece
            BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);
            tempPieces.Add(newPiece);

            //Setup the piece
            newPiece.Setup(teamColor, spriteColor, this);

        }

        //Returning the temporary list
        return tempPieces;

    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board) {
        Cell pawnCell = null;
        Cell royaltyCell = null;
        for (int i = 0; i < 8; i++) {

            pawnCell = board.Get(i, pawnRow);
            if (pawnCell != null) {
                //Place the pawn row
                pieces[i].Place(pawnCell);
            }

            royaltyCell = board.Get(i, royaltyRow);
            if(royaltyCell != null) {
                //Place the royalty row
                pieces[i + 8].Place(royaltyCell);
            }


        }

    }

}

