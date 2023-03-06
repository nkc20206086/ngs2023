using Command.Entity;

namespace Command
{
    /// <summary>
    /// �R�}���h�����N���X
    /// </summary>
    public class CommandCreater
    {
        /// <summary>
        /// �R�}���h�\���̂����ɃR�}���h�N���X�𐶐����܂�
        /// </summary>
        /// <param name="status">�������̃R�}���h�\����</param>
        /// <returns>���������R�}���h�\����</returns>
        public static MainCommand CreateCommand(CommandStruct status)
        {
            MainCommand command = default;  // ���C���R�}���h�̃��[�J���ϐ����쐬

            // �R�}���h�^�C�v�����ɃR�}���h���쐬
            switch (status.type)
            {
                case MainCommandType.Move: command = new MoveCommand(status); break;
                case MainCommandType.Rotate: command = new RotateCommand(status); break;
            }

            return command; // �쐬�����R�}���h��Ԃ�
        }
    }
}