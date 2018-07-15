using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    //Creating a reference field for the cell prefab in the Unity Editor
    public GameObject mCellPrefab;
    private int maxXValue;
    private int maxYValue;

    [HideInInspector]
    //Creating a two dimensional array to store the cells for the board
    private Cell[,] mAllCells = new Cell[8, 8];


	public void Create() {
		
        
        //Creating the all the cells
        for(int x = 0; x < 8; x++) {
            for(int y = 0; y < 8; y++) {

                //Instantiating a new cell from the Cell Prefab
                GameObject newCell = Instantiate(mCellPrefab, transform);

                //Setting the position of the Cell
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                //Setting up the Cells
                mAllCells[x, y] = newCell.GetComponent<Cell>();
                mAllCells[x, y].Setup(new Vector2Int(x, y), this);

            }
        }

        //Coloring the cells
        for(int x = 0; x < 8; x += 2) {
            for(int y = 0; y < 8; y++) {

                //Checking if the row is odd or even and setting according value to offset variable
                int offset = (y % 2 != 0) ? 0 : 1;
                //Adding offset to x. If line is odd x gets offset by 1 creating the checkers pattern  
                int finalX = x + offset;
                //Changing the color of the cells
                mAllCells[finalX, y].GetComponent<Image>().color = new Color32(186, 177, 150, 255);

            }
        }

        this.maxXValue = this.mAllCells.GetLength(0) - 1;
        this.maxYValue = this.mAllCells.GetLength(1) - 1;
       
	}

    public Cell Get(int x,int y) {

        if(x < 0 || x > maxXValue || y < 0 || y > maxYValue) {
            return null;
        }
        else {
            return this.mAllCells[x, y];
        }
    }
	

}
