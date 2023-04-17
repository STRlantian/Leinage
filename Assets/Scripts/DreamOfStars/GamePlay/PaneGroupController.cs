using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaneGroupController : MonoBehaviour
{
    public PaneGroup CurrentGroup;
    public PaneGroupController ParentGroup; // 为了套娃，可能有多层parent

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        var main = GameManager.MainInstance;
        if (main.IsPlaying)
        {
            CurrentGroup.Update(main.CurrentTime + main.VisualOffset);
            transform.localPosition = CurrentGroup.Position;
            transform.localEulerAngles = CurrentGroup.Rotation;
            
        }
    }

    public void SetGroup(PaneGroup group)
    {
        var mainTimer = GameManager.MainInstance.Song.Timer;
        foreach (TimeNode tn in group.StoryBoard.TimeNodes)
        {
            tn.Duration =mainTimer.BeatToSec(tn.Offset+tn.Duration);
            tn.Offset = mainTimer.BeatToSec(tn.Offset);
            tn.Duration -= tn.Offset;
        }
        CurrentGroup = group;
    }
    
}



public class PaneGroupManager
{
    public PaneGroup CurrentGroup;
    public Vector3 FinalPosition;
    public Quaternion FinalRotation;
    public bool isDirty;

    public PaneGroupManager(PaneGroup group)
    {
        CurrentGroup = group;
        isDirty = true;
    }

    public void Get(ref Vector3 pos, ref Quaternion rot)
    {
        pos = FinalRotation * pos + FinalPosition;
        rot = FinalRotation * rot;
    }

    public void Update(ChartManager chartMan,string original = null)
    {
        FinalPosition=CurrentGroup.Position;
        FinalRotation = Quaternion.Euler(CurrentGroup.Rotation);
        if (original == null) original = CurrentGroup.Name; // 默认为当前组
        if(!string.IsNullOrEmpty(CurrentGroup.Name) ) {
            PaneGroupManager group = chartMan.Groups[CurrentGroup.Name];
        }

    }
}
