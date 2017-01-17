using UnityEngine;
using System.Collections;




public class SteeringBehavior  {


	public static void Move(AIData mdata)
	{

		mdata.mTrans.position = mdata.mTrans.position + mdata.mTrans.forward * mdata.mSpeed * Time.deltaTime;


	}

	public static bool Seek(AIData mdata)
	{

		Vector3 vFinal;
		Vector3 vec = mdata.mCurrentTargetPosition - mdata.mTrans.position; //兩者向量
		vec.y = 0.0f;     //避免高度干擾
		float fd=vec.magnitude; //兩者距離
		/*mdata.mTargetDistance = fd;
		if (fd-mdata.fAttackDistance <= mdata.mSpeed * Time.deltaTime) {

			mdata.mSpeed = 0f;
			return false;
		}*/
		vec.Normalize ();  

		Vector3 vfor = mdata.mTrans.forward;
		float fCos = Vector3.Dot (vfor, vec);
		if (fCos < -1.0f) {            //防止不明的bug
			fCos = -1.0f;
		} else if (fCos > 1.0f) {
			fCos = 1.0f;
		}

		float fTheta = Mathf.Acos (fCos);
		float fAngle = fTheta * Mathf.Rad2Deg;


		Vector3 vCross = Vector3.Cross (vfor, vec);  //外積
		if (vCross.y < 0.0f) {
			fAngle = -fAngle;
		} 

		mdata.mTrans.Rotate (0.0f, fAngle, 0.0f);
		return true;
	}



}
