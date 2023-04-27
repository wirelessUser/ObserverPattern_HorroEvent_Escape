using System;

[Serializable]
public class Instruction
{
    // TODO -> Instruction Should be Scriptable Object and Reference Should be in UIManager
    // There will be seperate Mono for Insutrctions to maintain coroutines
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
