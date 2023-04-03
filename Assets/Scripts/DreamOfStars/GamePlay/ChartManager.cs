using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartManager 
{
    public GameSong Song;
    public DreamOfStars.GamePlay.Chart CurrentChart;
    public Dictionary<string,PaneGroupManager> Groups = new Dictionary<string,PaneGroupManager>();
    public List<PaneManager> Panes = new List<PaneManager>();

    public float CurrentSpeed;
    public float CurrentTime;

}
