using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum eFSMSID
{
	FSM_GoToTarget = 0,
	FSM_CHASE,
	FSM_ATTACK,
	FSM_Die
}
public class FSMBase {
	public virtual void Init ()
	{
		m_fTime = 0.0f;
	}

	public virtual void DoState (AIData m_AIData)
	{

	}
	public virtual void CheckCondition(AIData m_AIData)
	{

	}
	public virtual void DoBeforeEntering (AIData m_AIData)
	{

	}

	public float m_fTime;
	public eFSMSID m_StateID;
}


public class FSMGoToTarget:FSMBase {
	public override void Init ()
	{
		base.Init ();
		m_StateID = eFSMSID.FSM_GoToTarget;
	}

	public override void DoState (AIData m_AIData)
	{

	
		m_AIData.AniControl.BattleWalkForward ();
		SteeringBehavior.Move(m_AIData);
		SteeringBehavior.Seek(m_AIData);
	}
	public override void CheckCondition(AIData m_AIData)
	{  	
		if (m_AIData.mHP <= 0) {
			m_AIData.mFSM.ChangeState (eFSMSID.FSM_Die);
		}

		MoveTest AttackTarget;

		if (m_AIData.mCurrentEnemy != null) {
			AttackTarget = m_AIData.mCurrentEnemy.GetComponent<MoveTest> ();
			Vector3 vec2 = m_AIData.mEnemyBase.transform.position - m_AIData.mTrans.position;	
			float fd2 = vec2.magnitude;
			Vector3 vec = m_AIData.mCurrentEnemy.transform.position - m_AIData.mTrans.position;
			float fd = vec.magnitude;

			if (fd <= m_AIData.fChaseDistance && AttackTarget.mData.mHP > 0) {
				m_AIData.mFSM.ChangeState (eFSMSID.FSM_CHASE);
			} else if (fd <= m_AIData.fAttackDistance && AttackTarget.mData.mHP > 0) {
				m_AIData.mFSM.ChangeState (eFSMSID.FSM_ATTACK);
			}
		



		}

	

	}
	public override void DoBeforeEntering (AIData m_AIData)
	{
		
		m_AIData.mCurrentTargetPosition = m_AIData.mEnemyBase.transform.position;
	}



}

public class FSMChase:FSMBase {
	public override void Init ()
	{
		base.Init ();
		m_StateID = eFSMSID.FSM_CHASE;
	}

	public override void DoState (AIData m_AIData)
	{
		m_AIData.mCurrentTargetPosition = m_AIData.mCurrentEnemy.transform.position;
		m_AIData.AniControl.BattleWalkForward ();
		SteeringBehavior.Move(m_AIData);
		SteeringBehavior.Seek(m_AIData);

	}
	public override void CheckCondition(AIData m_AIData)
	{

		if (m_AIData.mHP <= 0) {
			m_AIData.mFSM.ChangeState (eFSMSID.FSM_Die);
		}

		MoveTest AttackTarget;

		if (m_AIData.mCurrentEnemy != null) {

			AttackTarget = m_AIData.mCurrentEnemy.GetComponent<MoveTest> ();
         	Vector3 vec = m_AIData.mCurrentEnemy.transform.position - m_AIData.mTrans.position;
			float fd = vec.magnitude;

			if (AttackTarget.mData.mHP <= 0) {
				m_AIData.mFSM.ChangeState (eFSMSID.FSM_GoToTarget);
			} else if (fd <= m_AIData.fAttackDistance && AttackTarget.mData.mHP > 0) {
				m_AIData.mFSM.ChangeState (eFSMSID.FSM_ATTACK);
			} 


		}
		 


	}
	public override void DoBeforeEntering (AIData m_AIData)
	{
		//m_AIData.mCurrentTargetPosition = m_AIData.mEnemy.transform.position;
	}



}

public class FSMAttack:FSMBase {
	public override void Init ()
	{
		base.Init ();
		m_StateID = eFSMSID.FSM_ATTACK;
	}

	public override void DoState (AIData m_AIData)
	{   
		MoveTest AttackTarget;

		if (m_AIData.mCurrentEnemy != null) {
			AttackTarget = m_AIData.mCurrentEnemy.GetComponent<MoveTest> ();
		 if (m_fTime >= 3.0f&&AttackTarget.mData.mHP > 0) {
			m_AIData.mCurrentTargetPosition = m_AIData.mCurrentEnemy.transform.position;	
			SteeringBehavior.Seek(m_AIData);
			AttackTarget.GetHit (m_AIData.mATK);
			
			m_AIData.AniControl.Attack02 ();
			m_fTime = 0.0f;

			}

		}
	
	}
	public override void CheckCondition(AIData m_AIData)
	{

		if (m_AIData.mHP <= 0) {
			m_AIData.mFSM.ChangeState (eFSMSID.FSM_Die);
		}
	
		MoveTest AttackTarget;


		if (m_AIData.mCurrentEnemy != null) {
			m_fTime += Time.deltaTime;

	    	AttackTarget = m_AIData.mCurrentEnemy.GetComponent<MoveTest> ();
			Vector3 vec = m_AIData.mCurrentEnemy.transform.position - m_AIData.mTrans.position;
			float fd = vec.magnitude;
		
			if (fd >= m_AIData.fAttackDistance && fd <= m_AIData.fChaseDistance) {
				m_AIData.mFSM.ChangeState (eFSMSID.FSM_CHASE);
			} else if (AttackTarget.mData.mHP <= 0) {
				m_AIData.mFSM.ChangeState (eFSMSID.FSM_GoToTarget);

			} 

		}


	}
	public override void DoBeforeEntering (AIData m_AIData)
	{
		
		m_fTime = 0.0f;
		m_AIData.AniControl.Attack02();
	}
		
}


public class FSMDie:FSMBase {
	public override void Init ()
	{
		base.Init ();
		m_StateID = eFSMSID.FSM_Die;
	}

	public override void DoState (AIData m_AIData)
	{
		MoveTest Control;
		m_fTime += Time.deltaTime;
		m_AIData.AniControl.Die ();
		if (m_fTime >= 3.0f) {
			Control = m_AIData.mthis.GetComponent<MoveTest> ();
			Control.remove ();
			Control.dead ();

		}


	}
	public override void CheckCondition(AIData m_AIData)
	{
		
	}
	public override void DoBeforeEntering (AIData m_AIData)
	{
		m_fTime = 0.0f;

		



	
	}


}




public class FSMManager
{
	public List<FSMBase> m_StateList;
	public FSMBase m_CurrentState;
	public AIData m_AIData;

	public void Init (AIData data)
	{
		m_StateList = new List<FSMBase> ();
		m_AIData = data;
	}

	public FSMBase FindState(eFSMSID ID)
	{

		int iCount = m_StateList.Count;
		for (int i = 0; i < iCount; i++) {
			if (m_StateList [i].m_StateID == ID)
				return m_StateList [i];
		}
		return null;
	}


	public void AddState(FSMBase state)
	{
		if (FindState (state.m_StateID) != null) {
			return;
		}
		m_StateList.Add (state);

	}


	public void SetCurrentState(eFSMSID ID)
	{
		FSMBase state = FindState (ID);
		if (state == null) {
			return;
		}

		m_CurrentState = state;
		m_CurrentState.DoBeforeEntering (m_AIData);
	}


	public void DoCurrentState()
	{
		
		if (m_CurrentState != null) {

			m_CurrentState.DoState (m_AIData);
			m_CurrentState.CheckCondition (m_AIData);
		}
	}

	public void ChangeState(eFSMSID id)
	{
		m_CurrentState = FindState (id);
		m_CurrentState.DoBeforeEntering (m_AIData);
	}



}
