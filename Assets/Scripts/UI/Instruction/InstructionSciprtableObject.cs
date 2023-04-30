using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstructionSciprtableObject", menuName = "ScriptableObjects/InstructionSciprtableObject", order = 2)]

public partial class InstructionSciprtableObject : ScriptableObject
{
    public InstructionType instructionType;
    public string instruction;
    public int displayDuration;
    public int waitToTriggerDuration = 0;
}
