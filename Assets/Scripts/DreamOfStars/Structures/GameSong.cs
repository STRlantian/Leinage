using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamOfStars.GamePlay;

[CreateAssetMenu(menuName = "DreamOfStars/GameSong")]
public class GameSong : ScriptableObject
{
    public string SongName;
    public string SongArtist;
    public string Genre = "Genreless"; // TODO: 更多属性字段

    public string FilePath;
    public AudioClip Clip;
    public Metronome Timer = new Metronome(60); // 默认BPM 60
    
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
