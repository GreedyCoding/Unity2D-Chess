using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : BasePiece {

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager) {

        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Knight");

    }

    private void CreateCellPath(int flipper) {

        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        //Left
        MatchStates(currentX - 2, currentY + (1 * flipper));
        //Upper Left
        MatchStates(currentX - 1, currentY + (2 * flipper));
        //Upper right
        MatchStates(currentX + 1, currentY + (2 * flipper));
        //Right
        MatchStates(currentX + 2, currentY + (1 * flipper));

    }

    protected override void CheckPathing() {

        CreateCellPath(1);
        CreateCellPath(-1);

    }

    private void MatchStates(int targetX, int targetY) {

        CellState cellState = CellState.None;
        cellState = mCurrentCell.mBoard.CheckCellState(targetX, targetY, this);

        if (cellState != CellState.Friendly && cellState != CellState.OutOfBounds) {

            mHighlightedCells.Add(mCurrentCell.mBoard.Get(targetX, targetY));

        }

    }

}
