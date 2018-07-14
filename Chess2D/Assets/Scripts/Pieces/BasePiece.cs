using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BasePiece : EventTrigger 
{

    [HideInInspector]
    //Initializing the Piece with a clear base color
    public Color mColor = Color.clear;

    //Settin up a reference to the original cell to easily reset the Board to restart the game
    protected Cell mOriginalCell = null;
    //Reference to the current cell the piece stands on
    protected Cell mCurrentCell = null;

    protected RectTransform mRectTransform = null;
    //Creating a reference field for the piecemanager in the Unity Editor
    protected PieceManager mPieceManager;

    public virtual void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager) {

        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

    }

    public void Place(Cell newCell) {

        mOriginalCell = newCell;
        mCurrentCell = newCell;
        mCurrentCell.mCurrentPiece = this;

        transform.position = newCell.transform.position;
        gameObject.SetActive(true);

    }

}
