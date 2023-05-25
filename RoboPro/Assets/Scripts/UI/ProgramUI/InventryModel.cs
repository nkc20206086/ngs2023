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
        CommandStruct st0 = new CommandStruct(MainCommandType.Move,false,false,false,-5,CoordinateAxis.X,0);
        CommandStruct st1 = new CommandStruct(MainCommandType.Rotate,false,false,false,1,CoordinateAxis.Y,0);
        CommandStruct st2 = new CommandStruct(MainCommandType.Scale,false,false,false,1,CoordinateAxis.Z,0);

        CommandBase[] commands = new CommandBase[3];
        commands[0] = CommandCreater.CreateCommand(st0);
        commands[1] = null;
        commands[2] = CommandCreater.CreateCommand(st2);

        UIEvent(commands);
    }
}
