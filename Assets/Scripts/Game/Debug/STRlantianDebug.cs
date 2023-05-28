using STRlantian.Gameplay;

namespace Game.Debug
{
    public class STRlantianDebug : LMGameDebugger
    {
        protected override void Debug()
        {
            debuggerNotes[0].posY = -debuggerNotes[0].transform.position.y;
            debuggerNotes[0].ActiveNote();
        }
    }
}