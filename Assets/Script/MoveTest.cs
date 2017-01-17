using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIData
{
	public GameObject mthis;
	public Transform mTrans;
	public Vector3 mCurrentTargetPosition;
	public float mTargetDistance;
	public float mSpeed;
	public float fAttackDistance;
	public float fChaseDistance;
	public playerControl AniControl;
	public GameObject mEnemyBase;
	public List<GameObject> mEnemy;
	public GameObject mCurrentEnemy;
	public FSMManager mFSM;
	public float mHP;
	public float mATK;
	public bool mAlive;
	public MoveTest mCurrentEnemystate;







}





public class MoveTest : MonoBehaviour {

	public AIData mData; 

    


	// Use this for initialization
	void Awake() {

		mData=new AIData();
		mData.mthis = this.gameObject;
		mData.mTrans=this.gameObject.transform;
		mData.mCurrentTargetPosition=Vector3.zero;
		mData.mSpeed = 1.0f;
		mData.fAttackDistance = 2.0f;
		mData.fChaseDistance =10.0f;
		mData.AniControl = this.gameObject.GetComponent<playerControl>(); 

		mData.mHP = 100.0f;
		mData.mATK = 50.0f;
		mData.mAlive = true;

		mData.mFSM=new FSMManager();
		mData.mFSM.Init (mData);

		if (gameObject.tag == "Player.minion") {
			mData.mEnemyBase = GameObject.FindWithTag ("Enemy.base");
			mData.mEnemy = Manager.Instance ().GetEnemyNpc ();
		} else {
			mData.mEnemyBase = GameObject.FindWithTag ("Player.base");
			mData.mEnemy = Manager.Instance ().GetPlayerNPC ();
		}






	}


	void Start () {
		
		FSMGoToTarget stateGoToTarget = new FSMGoToTarget ();
		stateGoToTarget.Init ();
		FSMChase statechase = new FSMChase ();
		statechase.Init ();
		FSMAttack stateattack = new FSMAttack ();
		stateattack.Init ();
		FSMDie statedie = new FSMDie ();
		statedie.Init ();
		mData.mFSM.AddState (stateGoToTarget);
		mData.mFSM.AddState (statechase);
		mData.mFSM.AddState (stateattack);
		mData.mFSM.AddState (statedie);

		mData.mFSM.SetCurrentState (stateGoToTarget.m_StateID);



	


	
	}
	public void remove()
	{

		if(gameObject.tag=="Player.minion")
			Manager.Instance().RemovePlayerNPC(this.gameObject);
		else
			Manager.Instance().RemoveEnemyNpc(this.gameObject);

	}

	public void GetHit(float atk)
	{
		mData.mHP -= atk;


	}


	public void dead()
	{

		Destroy (this.gameObject);

	}




	// Update is called once per frame
	void Update () {

	
		int iCount=mData.mEnemy.Count;
		float Bestfd=1000f;
		for (int i = 0; i < iCount; i++) {
			
			Vector3 vec = mData.mEnemy[i].transform.position - mData.mTrans.position;
			float fd = vec.magnitude;
			if (fd < Bestfd) {
				Bestfd = fd;
				mData.mCurrentEnemy = mData.mEnemy [i];
				mData.mCurrentEnemystate = mData.mEnemy [i].GetComponent<MoveTest>();
			}
		}





		mData.mFSM.DoCurrentState ();








	}
}
