using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyState : MonoBehaviour {


	void Awake(){

	
		Manager.Instance ().AddEnemyNpc (this.gameObject);


	}




	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
