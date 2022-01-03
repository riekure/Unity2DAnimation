using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineExample : StateMachineBehaviour
{

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// �X�e�[�g�Ɉڍs�����Ƃ��i�ŏ��̃t���[���Ɏ��s�j
		Debug.Log("OnStateEnter");
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// ���̃X�e�[�g�Ɉڍs����Ƃ��i�Ō�̃t���[���Ɏ��s�j
		Debug.Log("OnStateExit");
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// �ŏ��ƍŌ�̃t���[���������e�t���[���Ŏ��s
		Debug.Log("OnStateUpdate");
	}

	public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// MonoBehaviour.OnAnimatorMove �̒���ɌĂяo��
		Debug.Log("OnStateMove");
	}

	public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// MonoBehaviour.OnAnimatorIK �̒���ɌĂяo��
		Debug.Log("OnStateIK");
	}
}
