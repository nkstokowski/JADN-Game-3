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
	public bool canActivate = true;
	SwitchAction action;


	enum SwitchType {
		Wall,
		Floor
	}
	SwitchType type;

	void Start() {
		if (switchTrigger.GetComponent<Switch> () != null) {
			type = SwitchType.Wall;
		} else {
			type = SwitchType.Floor;
		}
	}
	void Update() {
		ListenForSwitchStatus();
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
		} else {
			Create();
		}
	}

	void Move(){
		if(canActivate){
			transform.position = Vector3.Lerp(transform.position, movePosition, moveSpeed * Time.deltaTime);
			if (!canActivateLoop)
				canActivate = false;
		}
	}

	void Create(){
		GameObject block = Instantiate(changeBlock, transform.position, transform.rotation);
		block.transform.localScale = transform.localScale;
		if(layer == LayerMask.NameToLayer("TopLayer")){
			block.layer = 9;
			block.transform.parent = topParent.transform;
		} else {
			block.layer = 8;
			block.transform.parent = bottomParent.transform;
		}
		Destroy(gameObject);
	}

}
