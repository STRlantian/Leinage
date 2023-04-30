using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Charting
{
    /// <summary>
    /// ChartIO: 读取Chart文件
    /// </summary>
    public static partial class SChartIO
    {
        /// <summary>
        /// ReadChart(): 从文件读取铺面xml文件
        /// </summary>
        /// <param name="name">歌曲名 分大小写</param>
        /// <param name="diff">5个难度 RL-MD-SP-CL-QS</param>
        /// <returns>对应的铺面文件</returns>
        /// <exception cref="System.Exception">铺面文件找不到</exception>
        public static Chart ReadChart(string name, int diff)
        {
            try
            {
                XDocument file = new XDocument($"{Application.dataPath}\\{name}\\{name}_{diff}.xml");
                return new Chart(file);
            }
            catch(IOException)
            {
                throw new System.Exception($"Chart {name}\\{name}_{diff}.xml Not Found!");
            }
        }

        public static void PreloadAllCharts()
        {

        }
    }
}