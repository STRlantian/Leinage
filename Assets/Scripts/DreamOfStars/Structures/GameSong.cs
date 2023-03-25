using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamOfStars.GamePlay;

public class GameSong : ScriptableObject
{
    public string SongName;
    public string SongArtist;
    public string Genre = "Genreless"; // TODO: ¸ü¶àÊôÐÔ×Ö¶Î

    public string FilePath;
    public AudioClip Clip;
    
    public List<ChartMeta> Charts=new List<ChartMeta>();
}

public class ChartFile : ScriptableObject
{
    public Chart ChartData;
}

[System.Serializable]
public class ChartMeta
{
    public string RelativePath;

    public string DifficultyLevel;
    public string DifficultyName;
}
