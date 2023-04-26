using System;

[Serializable]
public class Instruction
{
    public InstructionType instructionType;
    public string instruction;
    public bool displayed;


    [Serializable]
    public enum InstructionType
    {
        PlayerSpawned,
        LightsOff,
        Interact,
        OpenDoor
    }

}
