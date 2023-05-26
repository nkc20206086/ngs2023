using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Command;
using Command.Entity;

public class InventryModel : MonoBehaviour
{
    public event Action<CommandBase[]> UIEvent;
    private void Start()
    {
        CommandContainer st0 = new CommandContainer(MainCommandType.Move,false,false,false,-5,CoordinateAxis.X,0);
        CommandContainer st1 = new CommandContainer(MainCommandType.Rotate,false,false,false,1,CoordinateAxis.Y,0);
        CommandContainer st2 = new CommandContainer(MainCommandType.Scale,false,false,false,1,CoordinateAxis.Z,0);

        CommandBase[] commands = new CommandBase[3];
        commands[0] = CommandCreater.CreateCommand(st0);
        commands[1] = null;
        commands[2] = CommandCreater.CreateCommand(st2);

        UIEvent(commands);
    }

    public void Test(int main, int sub)
    {
        Debug.Log(main + "," + sub);
    }
}
