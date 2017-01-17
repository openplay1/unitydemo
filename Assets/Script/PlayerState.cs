using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : MonoBehaviour {



	void Awake(){

	
		Manager.Instance ().AddPlayerNPC (this.gameObject);
	}



	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
