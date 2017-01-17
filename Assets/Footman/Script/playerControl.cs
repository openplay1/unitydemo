using UnityEngine;
using System.Collections;


public class playerControl : MonoBehaviour 
{
	public Animator anim;
	int attack01;
	int attack02;
	int attack03;
	int battleWalkBackward;
	int battleWalkForward;
	int battleWalkLeft;
	int battleWalkRight;
	int defend;
	int die;
	int getHit;
	int idle02;
	int jump;
	int walk;
	int taunt;
	int run;
	int idle01;

	public AnimatorStateInfo currentBaseState;

	void Awake () 
	{

	
		anim = GetComponent<Animator>();
		currentBaseState= anim.GetCurrentAnimatorStateInfo(0);

		idle01 =Animator.StringToHash("idle_00");
		attack01 = Animator.StringToHash("attack_01");
		attack02 = Animator.StringToHash("attack_02");
		attack03 = Animator.StringToHash("attack_03");
		battleWalkBackward = Animator.StringToHash("walkBattleBackward");
		battleWalkForward = Animator.StringToHash("walkBattleForward");
		battleWalkLeft = Animator.StringToHash("walkBattleLeft");
		battleWalkRight = Animator.StringToHash("walkBattleRight");
		defend = Animator.StringToHash("defend");
		die = Animator.StringToHash("die");
		getHit = Animator.StringToHash("getHit");
		idle02 = Animator.StringToHash("idle_02");
		jump = Animator.StringToHash("jump");
		walk = Animator.StringToHash("walk");
		taunt = Animator.StringToHash("taunt");
		run = Animator.StringToHash("run");
	}
	

	public void Attack01 ()
	{anim.SetBool("walkBattleForward",false);
		anim.SetTrigger(attack01);
	}

	public void Attack02 ()
	{

		anim.SetBool("walkBattleForward",false);
		anim.SetTrigger(attack02);

	
	}

	public void Attack03 ()
	{
		anim.SetTrigger(attack03);
	}

	public void BattleWalkBackward ()
	{
		//anim.SetTrigger(battleWalkBackward);
	}

	public void BattleWalkForward ()
	{
		anim.SetBool("walkBattleForward",true);
	

	}

	public void BattleWalkLeft ()
	{
		anim.SetTrigger(battleWalkLeft);
	}

	public void BattleWalkRight ()
	{
		anim.SetTrigger(battleWalkRight);
	}

	public void Defend ()
	{
		anim.SetTrigger(defend);
	}

	public void Die ()
	{

		anim.SetBool("walkBattleForward",false);
		anim.SetTrigger(die);
	}

	public void GetHit ()
	{
		anim.SetTrigger(getHit);
	}

	public void Idle02 ()
	{
		anim.SetTrigger(idle02);
	}


	public void Idle01 ()
	{
		//anim.SetBool("walkBattleForward",false);
		anim.SetTrigger(idle01);
	}



	public void Jump ()
	{
		anim.SetTrigger(jump);
	}

	public void Walk ()
	{
		anim.SetTrigger(walk);
	}

	public void Taunt ()
	{
		anim.SetTrigger(taunt);
	}

	public void Run ()
	{
		anim.SetTrigger(run);
	}
	
}
