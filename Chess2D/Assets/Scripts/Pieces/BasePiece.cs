﻿using System.Collections;
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

    //Target Cell the mouse is hovering over
    protected Cell mTargetCell = null;

    protected Vector3Int mMovement = Vector3Int.one;
    protected List<Cell> mHighlightedCells = new List<Cell>();

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

    public virtual void ResetPiece() {

        Kill();

        Place(mOriginalCell);

    }

    public virtual void Kill() {

        //Clear the current cell
        mCurrentCell.mCurrentPiece = null;

        //Remove the piece by disabling it
        gameObject.SetActive(false);

    }

    private void CreateCellPath(int xDirection, int yDirection, int movement) {

        //Getting the x and y from the current cell the piece is on
        int targetX = mCurrentCell.mBoardPosition.x;
        int targetY = mCurrentCell.mBoardPosition.y;

        //Loop through all the cells the piece can move to
        for(int i = 1; i <= movement; i++) {

            targetX += xDirection;
            targetY += yDirection;

            CellState cellState = CellState.None;
            cellState = mCurrentCell.mBoard.CheckCellState(targetX, targetY, this);

            if (cellState == CellState.Enemy) {

                mHighlightedCells.Add(mCurrentCell.mBoard.Get(targetX, targetY));
                break;

            }

            if (cellState != CellState.Free) {
                break;
            }

            mHighlightedCells.Add(mCurrentCell.mBoard.Get(targetX, targetY));
        }

    }

    protected virtual void CheckPathing() {

        //Creating various paths the movement paramter indicates how much spaces the piece can move

        //Horizontal
        CreateCellPath(1, 0, mMovement.x);
        CreateCellPath(-1, 0, mMovement.x);

        //Vertical
        CreateCellPath(0, 1, mMovement.y);
        CreateCellPath(0, -1, mMovement.y);

        //Upper Diagonal
        CreateCellPath(1, 1, mMovement.z);
        CreateCellPath(-1, 1, mMovement.z);

        //Lower Diagonal
        CreateCellPath(-1, -1, mMovement.z);
        CreateCellPath(1, -1, mMovement.z);
        
    }

    protected void ShowHighlightedCells() {

        foreach (Cell cell in mHighlightedCells) {
            cell.mOutlineImage.enabled = true;
        }

    }

    protected void ClearHighlightedCells() {

        foreach (Cell cell in mHighlightedCells) {
            cell.mOutlineImage.enabled = false;
        }

        mHighlightedCells.Clear();

    }

    protected virtual void Move() {

        mTargetCell.RemovePiece();

        mCurrentCell.mCurrentPiece = null;

        mCurrentCell = mTargetCell;
        mCurrentCell.mCurrentPiece = this;

        transform.position = mCurrentCell.transform.position;
        mTargetCell = null;

    }

    public override void OnBeginDrag(PointerEventData eventData) {
        base.OnBeginDrag(eventData);

        //Check which Cells the piece can move to
        CheckPathing();
        //Show all Cells the piece can move to
        ShowHighlightedCells();

    }

    public override void OnDrag(PointerEventData eventData) {
        base.OnDrag(eventData);



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        this.transform.position = new Vector3(ray.origin.x, ray.origin.y, 0);


        ////Set the position of the piece transform to the mouse position
        //this.transform.position += (Vector3)eventData.delta;

        //Check for available squares under the mouse
        foreach (Cell cell in mHighlightedCells) {
            
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, new Vector2(ray.origin.x, ray.origin.y))) {

                mTargetCell = cell;
                break;

            }

            mTargetCell = null;

        }

    }

    public override void OnEndDrag(PointerEventData eventData) {
        base.OnEndDrag(eventData);

        //Clear all the highlihgte
        ClearHighlightedCells();

        if (!mTargetCell) {

            transform.position = mCurrentCell.gameObject.transform.position;
            return;

        }

        Move();

        mPieceManager.SwitchSides(mColor);

    }

}
