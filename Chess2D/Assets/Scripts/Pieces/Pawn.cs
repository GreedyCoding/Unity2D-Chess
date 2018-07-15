using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : BasePiece {

    private bool isFirstMove = true;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager) {

        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        isFirstMove = true;

        mMovement = (mColor == Color.white) ? new Vector3Int(0, 1, 1) : new Vector3Int(0, -1, -1);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Pawn");

    }

    protected override void Move() {

        base.Move();

        isFirstMove = false;

    }

    public override void ResetPiece() {

        base.ResetPiece();

        isFirstMove = true;

    }

    private bool MatchStates(int targetX, int targetY, CellState targetState) {

        CellState cellState = CellState.None;
        cellState = mCurrentCell.mBoard.CheckCellState(targetX, targetY, this);

        if (cellState == targetState) {
            mHighlightedCells.Add(mCurrentCell.mBoard.Get(targetX, targetY));
            return true;
        }

        return false;

    }

    protected override void CheckPathing() {

        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        //Check Top Left for Enemy
        MatchStates(currentX - mMovement.z, currentY + mMovement.z, CellState.Enemy);

        //Check Forward
        if (MatchStates(currentX, currentY + mMovement.y, CellState.Free)) {

            if (isFirstMove) {

                MatchStates(currentX, currentY + (mMovement.y * 2), CellState.Free);

            }

        }

        //Check Top Right for Enemy
        MatchStates(currentX + mMovement.z, currentY + mMovement.z, CellState.Enemy);

    }

}
