﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public GameObject mCellPrefab;

    [HideInInspector]
    public Cell[,] mAllCells = new Cell[8, 8];

	public void Create() {
		
        //Creating the Cells
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

                //Checking if the row is odd or even and setting value to offset
                int offset = (y % 2 != 0) ? 0 : 1;
                //Adding offset to x. If line is odd x gets offset by 1 creating the checkers pattern  
                int finalX = x + offset;
                //Changing the color of the cells
                mAllCells[finalX, y].GetComponent<Image>().color = new Color(230, 150, 100, 255);

            }
        }

	}
	

}