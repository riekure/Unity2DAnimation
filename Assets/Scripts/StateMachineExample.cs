using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineExample : StateMachineBehaviour
{

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// ステートに移行したとき（最初のフレームに実行）
		Debug.Log("OnStateEnter");
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// 次のステートに移行するとき（最後のフレームに実行）
		Debug.Log("OnStateExit");
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// 最初と最後のフレームを除く各フレームで実行
		Debug.Log("OnStateUpdate");
	}

	public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// MonoBehaviour.OnAnimatorMove の直後に呼び出し
		Debug.Log("OnStateMove");
	}

	public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// MonoBehaviour.OnAnimatorIK の直後に呼び出し
		Debug.Log("OnStateIK");
	}
}
