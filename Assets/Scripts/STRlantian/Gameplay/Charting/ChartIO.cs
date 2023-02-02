using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.RepresentationModel;

namespace STRlantian.Gameplay.Charting
{
    public static class ChartIO
    {
        private readonly static string PATH = $"{Application.dataPath}\\Chart\\";

        public static Chart ReadChart(string name, int diff)
        {
            try
            {
                return new Chart(File.ReadAllText($"{PATH}{name}\\{diff}.txt"));
            }catch(IOException)
            {
                throw new System.Exception($"Chart {name}\\{diff}.txt Not Found!");
            }
        }
    }
}