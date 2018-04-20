using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomBlock : MonoBehaviour {

	public GameObject switchTrigger;
	public Vector3 movePosition;
	public float moveSpeed = 5.0f;
	public GameObject changeBlock;
	public LayerMask layer;
	public GameObject topParent;
	public GameObject bottomParent;
	public bool canActivateLoop = false;
	public bool takeSwitchLayer = false;
	public bool canActivate = true;
	SwitchAction action;


	enum SwitchType {
		Wall,
		Floor
	}
	SwitchType type;

	void Start() {
		if(switchTrigger.GetComponent<Switch>() != null) {
			type = SwitchType.Wall;
		} else {
			type = SwitchType.Floor;
		}
	}
	void Update() {
		ListenForSwitchStatus();

		if(action == SwitchAction.Move && transform.position == movePosition){
			if(!canActivateLoop)
				canActivate = false;
		}
	}

	void ListenForSwitchStatus() {
		switch (type){
			case SwitchType.Wall:
				if(switchTrigger.GetComponent<Switch>().turnedOn){
					action = switchTrigger.GetComponent<Switch>().action;
					SetTrigger(type);
				}
				break;
			case SwitchType.Floor:
				if(switchTrigger.GetComponent<FloorSwitch>().triggered){
					action = switchTrigger.GetComponent<FloorSwitch>().action;
					SetTrigger(type);
				}
				break;
			default:
				break;
		}
	}
	void SetTrigger(SwitchType type){
		if(action == SwitchAction.Move){
			Move();
		}
		else if (action == SwitchAction.Portal){
			TurnOnPortal ();
		}
		else {
			Create();
		}
	}

	void Move(){
		if(canActivate){
			transform.position = Vector3.Lerp(transform.position, movePosition, moveSpeed * Time.deltaTime);
		}
	}

	void Create(){
		GameObject block = Instantiate(changeBlock, transform.position, transform.rotation);
		block.transform.localScale = transform.localScale;
		block.transform.parent = transform.parent;
		if(takeSwitchLayer){
			block.layer = switchTrigger.layer;
			block.transform.parent = switchTrigger.transform.parent;
		}
		else {
			block.layer = gameObject.layer;
			block.transform.parent = transform.parent;
		}
		Destroy(gameObject);
	}

	void TurnOnPortal(){
		transform.Find ("Portal").gameObject.SetActive (true);
	}

}
