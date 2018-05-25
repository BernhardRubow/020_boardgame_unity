using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class nvp_SelectTurnUiManager_scr : MonoBehaviour {

	public delegate void ButtonClickedEvent(int index);
	public ButtonClickedEvent OnButtonClicked;	

	[SerializeField] private Button[] _selectButtons;

	
	void Start () {
		ActivateButtons(2);
	}
	
	void Update () {
		
	}

	public void ActivateButtons(int numberOfButtons){

		for(int i = 0, n = _selectButtons.Length; i < n; i++){
			_selectButtons[i].gameObject.SetActive(false);
		}

		for(int i = 0, n = numberOfButtons; i < n; i++){
			_selectButtons[i].gameObject.SetActive(true);
		}
	}

	public void OnSelectButtonClicked(int index){
		if(OnButtonClicked != null) OnButtonClicked(index);
	}
}
