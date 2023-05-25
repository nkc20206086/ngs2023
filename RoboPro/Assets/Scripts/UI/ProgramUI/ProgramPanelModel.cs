using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Command;
using Command.Entity;

public class ProgramPanelModel : MonoBehaviour
{
    public event Action<MainCommand[]> UIEvent;
    private void Start()
    {
        CommandStruct st0 = new CommandStruct(MainCommandType.Move, false, false, false, 1, CoordinateAxis.NONE, 0);
        CommandStruct st1 = new CommandStruct(MainCommandType.Rotate, false, false, false, -1, CoordinateAxis.Y, 0);
        CommandStruct st2 = new CommandStruct(MainCommandType.Scale, false, false, false, 1, CoordinateAxis.Z, 0);

        MainCommand[] commands = new MainCommand[3];
        commands[0] = null;
        commands[1] = CommandCreater.CreateCommand(st1);
        commands[2] = CommandCreater.CreateCommand(st2);

        UIEvent(commands);
    }
}
