using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Board mBoard;

	void Start () {

        CreateBoard();

    }

    private void CreateBoard() {

        if (mBoard != null){
            mBoard.Create();
        }
        

    }

}
