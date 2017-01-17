using UnityEngine;
using System.Collections;
using System.Collections.Generic;





public class Manager : MonoBehaviour {

	public List<GameObject> m_PlayerNPC;
	public List<GameObject> m_EnemyNpc;
	public Vector3 V3_Random;//隨機生成位置

	private static Manager m_Instance;
	public static Manager Instance() {
		return  m_Instance;
	}
	void Awake()
	{
		m_Instance = this;
		m_PlayerNPC = new List<GameObject> ();
		m_EnemyNpc = new List<GameObject> ();

	}

	public void AddEnemyNpc(GameObject Enemy)
	{
		m_EnemyNpc.Add (Enemy);

	}

	public List<GameObject> GetEnemyNpc()
	{
		return m_EnemyNpc;

	}

	public void AddPlayerNPC(GameObject Player)
	{

		m_PlayerNPC.Add (Player);
	}

	public  List<GameObject> GetPlayerNPC()
	{
		return  m_PlayerNPC;

	}



	public void RemovePlayerNPC(GameObject Player)
	{
		m_PlayerNPC.Remove (Player);
	}


	public void RemoveEnemyNpc(GameObject Enemy)
	{
		m_EnemyNpc.Remove (Enemy);
	}


	// Use this for initialization
	void Start () {

	


	


	}

	// Update is called once per frame
	void Update () {
		V3_Random=new Vector3(Random.Range(-10.5f,10.5f),0.0f,Random.Range(-10.5f,10.5f));

		if(Input.GetKeyDown(KeyCode.A)) {
			Object o = Resources.Load ("Footman_Unlit_Blue");
			int i = 0;
			GameObject go;

			go = Instantiate (o,V3_Random,Quaternion.identity) as GameObject;

			
		}


	   if(Input.GetKeyDown(KeyCode.B)) {
			Object o2 = Resources.Load ("Footman_Unlit_Red");
			int j = 0;
			GameObject go2;

			go2 = Instantiate (o2,V3_Random,Quaternion.identity) as GameObject;

		}


		/*int iCount=m_PlayerNPC.Count;
	
		for (int i = 0; i < iCount; i++) {

			Debug.Log (m_PlayerNPC [i]);
		
			}
		iCount=m_EnemyNpc.Count;

		for (int i = 0; i < iCount; i++) {

			Debug.Log (m_EnemyNpc [i]);

		}*/






	}
}
