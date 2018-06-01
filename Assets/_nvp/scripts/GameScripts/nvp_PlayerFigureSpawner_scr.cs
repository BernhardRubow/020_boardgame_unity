using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.enums;


public class nvp_PlayerFigureSpawner_scr : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] PlayerColors _playerColor;
	[SerializeField] GameObject _playerPrefab;


	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {

		var uiManagerScript = GameObject.Find("gameboardUiManager").GetComponent<nvp_GameBoardUiManager_scr>();
		Transform[] locations = null;
		string name = "";
		switch(_playerColor){
      case PlayerColors.red:
				name = "player_red_";
        locations = uiManagerScript.startPositionsRed;       
        break;

      case PlayerColors.black:
				name = "player_black_";
        locations = uiManagerScript.startPositionsBlack;
			break;
			
			case PlayerColors.yellow:
				name = "player_yellow_";
        locations = uiManagerScript.startPositionsYellow;
			break;
			
			case PlayerColors.green:
				name = "player_green_";
        locations = uiManagerScript.startPositionsGreen;
			break;
		}	
		Spawn(locations, name);
	}


	// +++ custom methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  private void Spawn(Transform[] locations, string name)
  {
    for (int i = 0, n = 4; i < n; i++)
    {
      var playerFigure = Instantiate(_playerPrefab, locations[i].position, Quaternion.identity);
      playerFigure.GetComponent<nvp_PlayerFigure_scr>().SetPlayerColor(_playerColor);
			playerFigure.gameObject.name = name + i.ToString();
    }
  }
}
