using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.enums;

public class nvp_PlayerFigure_scr : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] private Material[] _playerMaterials;


	// +++ custom methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void SetPlayerColor(PlayerColors playerColor){

		switch (playerColor)
		{
				case PlayerColors.red:
					SetColorOnChilds(_playerMaterials[0]);
				break;

				case PlayerColors.black:
					SetColorOnChilds(_playerMaterials[1]);
				break;

				case PlayerColors.yellow:
					SetColorOnChilds(_playerMaterials[2]);
				break;

				case PlayerColors.green:
					SetColorOnChilds(_playerMaterials[3]);
				break;
		}
	}

	private void SetColorOnChilds(Material mat){
		for(int i = 0, n = 2; i < n; i++){
			var mats = this.transform.GetChild(i).GetComponent<Renderer>().materials;
			mats[0] = mat;
			this.transform.GetChild(i).GetComponent<Renderer>().materials = mats;
		}
	}
}
