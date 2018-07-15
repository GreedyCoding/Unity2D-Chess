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

    private void CreateCellPath(int xDirection, int yDirection, int movement) {

        //Getting the x and y from the current cell the piece is on
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        Cell currentCell = null;

        //Loop through all the cells the piece can move to
        for(int i = 1; i <= movement; i++) {

            currentX += xDirection;
            currentY += yDirection;

            currentCell = mCurrentCell.mBoard.Get(currentX, currentY);

            if (currentCell != null) {
                mHighlightedCells.Add(currentCell);
            }

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
        CreateCellPath(1, 1, mMovement.x);
        CreateCellPath(-1, 1, mMovement.x);

        //Lower Diagonal
        CreateCellPath(-1, -1, mMovement.x);
        CreateCellPath(1, -1, mMovement.x);
        
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

    public override void OnBeginDrag(PointerEventData eventData) {
        base.OnBeginDrag(eventData);

        //Check which Cells the piece can move to
        CheckPathing();
        //Show all Cells the piece can move to
        ShowHighlightedCells();

    }

    public override void OnDrag(PointerEventData eventData) {
        base.OnDrag(eventData);

        //Set the position of the piece transform to the mouse position
        transform.position += (Vector3)eventData.delta/90;

    }

    public override void OnEndDrag(PointerEventData eventData) {
        base.OnEndDrag(eventData);

        //Clear all the highlihgte
        ClearHighlightedCells();

    }

}
