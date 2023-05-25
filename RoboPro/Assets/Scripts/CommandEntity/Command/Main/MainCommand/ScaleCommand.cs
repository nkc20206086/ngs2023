using System;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// �g����s���R�}���h�N���X
    /// </summary>
    public class ScaleCommand : MainCommand
    {
        private const float TOLERANCE = 0.3f;

        private Vector3 baseScale;      // ���X�̎ړx
        private Vector3 targetScale;    // �ڕW�̎ړx

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public ScaleCommand(CommandContainer status) : base(status) { }

        public override object InitCommand(object target, Action completeAction)
        {
            // �e���ڂ����݂̏�ԂōĐݒ�
            this.completeAction = completeAction;
            usableValue = value.getValue;
            usableAxis = axis.getAxis;

            baseScale = (Vector3)target;                                                    // ���X�̎ړx����������擾����

            targetScale = baseScale + GetDirection() * (Mathf.Abs(value.getValue) * 0.1f);  // �ڕW�̎ړx���v�Z���ĕۑ�

            // �ڕW�̎ړx��1�����̐�����1�ɂ���
            targetScale = new Vector3(targetScale.x < 1.0f ? 1.0f : targetScale.x, targetScale.y < 1.0f ? 1.0f : targetScale.y, targetScale.z < 1.0f ? 1.0f : targetScale.z);

            return targetScale;                                                             // �ڕW�̎ړx��Ԃ�
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                                     // �������X�e�[�g�𑗐M����Ă���Ȃ瑁�����^�[������

            if (state == CommandState.MOVE_ON)                                                              // �ʏ폈���ł���Ȃ�
            {
                if (usableValue > 0)                                                                        // �R�}���h�̎��l�����̐��Ȃ�
                {
                    if (Vector3.Distance(targetTransform.localScale,baseScale) >= (usableValue * 0.1f))     // �g��l���g�債�Ă���Ȃ�
                    {
                        targetTransform.localScale = targetScale;                                           // �ړx��ڕW�̎ړx�ɂ���
                        completeAction?.Invoke();                                                           // �R�}���h���������������s
                    }
                    else
                    {
                        targetTransform.localScale += GetDirection() * Time.deltaTime;                      // ���X�Ɋg�傷��
                    }
                }
                else                                                                                        // �R�}���h�̎��l�����̐��Ȃ�
                {
                    if (Mathf.Abs(Vector3.Distance(targetTransform.localScale, targetScale)) <= TOLERANCE)  // �ڕW�̎ړx�ɋ߂Â�����
                    {
                        targetTransform.localScale = targetScale;                                           // �ړx��ڕW�̎ړx�ɂ���
                        completeAction?.Invoke();                                                           // �R�}���h���������������s
                    }
                    else
                    {
                        targetTransform.localScale += GetDirection() * Time.deltaTime;                      // ���X�ɏk������(�������}�C�i�X�ł��邽�߉��Z)
                    }
                }
            }
            else�@                                                                                          // �t�����ł���Ȃ�
            {
                if (usableValue > 0)                                                                        // �R�}���h�̎��l�����̐��Ȃ�
                {
                    if (Vector3.Distance(targetTransform.localScale,baseScale) <= TOLERANCE)                // ���X�̎ړx�ɋ߂Â�����
                    {
                        targetTransform.localScale = baseScale;                                             // ���X�̎ړx�ɂ���
                        completeAction?.Invoke();                                                           // �R�}���h���������������s
                    }
                    else
                    {
                        targetTransform.localScale -= GetDirection() * Time.deltaTime;                      // ���X�ɏk������
                    }
                }
                else
                {
                    if (Mathf.Abs(Vector3.Distance(targetTransform.localScale, baseScale)) <= TOLERANCE)    // ���X�̎ړx�ɋ߂Â�����
                    {
                        targetTransform.localScale = baseScale;                                             // �ړx�����X�̎ړx�ɂ���
                        completeAction?.Invoke();                                                           // �R�}���h���������������s
                    }
                    else                                                                            
                    {
                        targetTransform.localScale -= GetDirection() * Time.deltaTime;                      // ���X�Ɋg�傷��(�������}�C�i�X�ł��邽�ߌ��Z)
                    }
                }
            }
        }

        public override MainCommandType GetMainCommandType()
        {
            return MainCommandType.Scale;
        }

        public override string GetString()
        {
            return "�g��";
        }
    }
}