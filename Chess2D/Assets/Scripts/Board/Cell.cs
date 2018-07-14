using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    //Creating a reference field for the outline image to show highligted cells in the Unity Editor
    public Image mOutlineImage;

    //Constructor variables for the Cells
    [HideInInspector]
    public Vector2Int mBoardPosition = Vector2Int.zero;
    [HideInInspector]
    public Board mBoard = null;
    [HideInInspector]
    public RectTransform mRectTransform = null;

    //Piece the Cells holds at the momenn
    [HideInInspector]
    public BasePiece mCurrentPiece = null;

    public void Setup(Vector2Int newBoardPosition, Board newBoard) {

        //Setting Board position and passing in the board the cell gets created on
        mBoardPosition = newBoardPosition;
        mBoard = newBoard;
        //Getting the recttransform of the cell prefab
        mRectTransform = GetComponent<RectTransform>();

    }


}
