using Command.Entity;

namespace Command
{
    /// <summary>
    /// �R�}���h�����N���X
    /// </summary>
    public class CommandCreater1
    {
        /// <summary>
        /// �R�}���h�\���̂����ɃR�}���h�N���X�𐶐����܂�
        /// </summary>
        /// <param name="status">�������̃R�}���h�\����</param>
        /// <returns>���������R�}���h�\����</returns>
        public static MainCommand CreateCommand(CommandContainer status)
        {
            MainCommand command = new MainCommand();  // ���C���R�}���h�̃��[�J���ϐ����쐬

            // �R�}���h�^�C�v�����ɃR�}���h���쐬
            switch (status.commandType)
            {
                case MainCommandType.Move: command = new MoveCommand(status); break;
                case MainCommandType.Rotate: command = new RotateCommand(status); break;
                case MainCommandType.Scale: command = new ScaleCommand(status); break;
            }

            return command; // �쐬�����R�}���h��Ԃ�
        }
    }
}