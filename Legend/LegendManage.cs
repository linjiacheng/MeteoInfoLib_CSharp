using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Windows.Forms;
using MeteoInfoC.Shape;
using MeteoInfoC.Drawing;
using MeteoInfoC.Global;
using MeteoInfoC.Layer;
using MeteoInfoC.Data;

namespace MeteoInfoC.Legend
{
    /// <summary>
    /// Legend methods manage
    /// </summary>
    public static class LegendManage
    {
        private static Color[] _rainbowColors = new Color[]
        {
            Color.FromArgb(129, 31, 240),
            Color.FromArgb(124, 30, 240),
            Color.FromArgb(119, 29, 241),
            Color.FromArgb(114, 27, 242),
            Color.FromArgb(108, 26, 242),
            Color.FromArgb(103, 24, 243),
            Color.FromArgb(97, 23, 244),
            Color.FromArgb(91, 21, 245),
            Color.FromArgb(86, 20, 245),
            Color.FromArgb(80, 18, 246),
            Color.FromArgb(73, 17, 247),
            Color.FromArgb(67, 15, 247),
            Color.FromArgb(61, 14, 248),
            Color.FromArgb(55, 12, 249),
            Color.FromArgb(48, 11, 250),
            Color.FromArgb(42, 9, 250),
            Color.FromArgb(35, 7, 251),
            Color.FromArgb(28, 6, 252),
            Color.FromArgb(21, 4, 252),
            Color.FromArgb(14, 3, 253),
            Color.FromArgb(7, 1, 254),
            Color.FromArgb(0, 0, 255),
            Color.FromArgb(3, 6, 253),
            Color.FromArgb(6, 12, 252),
            Color.FromArgb(10, 18, 250),
            Color.FromArgb(13, 24, 249),
            Color.FromArgb(17, 30, 247),
            Color.FromArgb(20, 36, 246),
            Color.FromArgb(23, 42, 245),
            Color.FromArgb(26, 47, 243),
            Color.FromArgb(29, 52, 242),
            Color.FromArgb(33, 57, 240),
            Color.FromArgb(36, 62, 239),
            Color.FromArgb(39, 67, 237),
            Color.FromArgb(42, 72, 236),
            Color.FromArgb(45, 76, 235),
            Color.FromArgb(48, 81, 233),
            Color.FromArgb(51, 85, 232),
            Color.FromArgb(53, 89, 230),
            Color.FromArgb(56, 93, 229),
            Color.FromArgb(59, 97, 227),
            Color.FromArgb(62, 101, 226),
            Color.FromArgb(65, 105, 225),
            Color.FromArgb(63, 106, 226),
            Color.FromArgb(62, 107, 227),
            Color.FromArgb(60, 108, 229),
            Color.FromArgb(59, 110, 230),
            Color.FromArgb(57, 111, 232),
            Color.FromArgb(56, 113, 233),
            Color.FromArgb(54, 115, 235),
            Color.FromArgb(52, 116, 236),
            Color.FromArgb(51, 118, 237),
            Color.FromArgb(49, 120, 239),
            Color.FromArgb(47, 122, 240),
            Color.FromArgb(46, 123, 242),
            Color.FromArgb(44, 125, 243),
            Color.FromArgb(42, 127, 245),
            Color.FromArgb(41, 130, 246),
            Color.FromArgb(39, 132, 247),
            Color.FromArgb(37, 134, 249),
            Color.FromArgb(35, 136, 250),
            Color.FromArgb(33, 139, 252),
            Color.FromArgb(31, 141, 253),
            Color.FromArgb(29, 144, 255),
            Color.FromArgb(28, 145, 255),
            Color.FromArgb(27, 147, 255),
            Color.FromArgb(25, 149, 255),
            Color.FromArgb(24, 151, 255),
            Color.FromArgb(22, 153, 255),
            Color.FromArgb(21, 155, 255),
            Color.FromArgb(19, 158, 255),
            Color.FromArgb(18, 160, 255),
            Color.FromArgb(17, 162, 255),
            Color.FromArgb(15, 164, 255),
            Color.FromArgb(14, 166, 255),
            Color.FromArgb(12, 169, 255),
            Color.FromArgb(11, 171, 255),
            Color.FromArgb(9, 173, 255),
            Color.FromArgb(8, 176, 255),
            Color.FromArgb(7, 178, 255),
            Color.FromArgb(5, 180, 255),
            Color.FromArgb(4, 183, 255),
            Color.FromArgb(2, 185, 255),
            Color.FromArgb(1, 188, 255),
            Color.FromArgb(0, 191, 255),
            Color.FromArgb(7, 191, 254),
            Color.FromArgb(14, 191, 253),
            Color.FromArgb(21, 191, 252),
            Color.FromArgb(28, 191, 251),
            Color.FromArgb(35, 192, 251),
            Color.FromArgb(41, 192, 250),
            Color.FromArgb(48, 193, 249),
            Color.FromArgb(55, 193, 248),
            Color.FromArgb(62, 194, 248),
            Color.FromArgb(69, 194, 247),
            Color.FromArgb(75, 195, 246),
            Color.FromArgb(82, 196, 245),
            Color.FromArgb(88, 196, 245),
            Color.FromArgb(95, 197, 244),
            Color.FromArgb(101, 198, 243),
            Color.FromArgb(108, 199, 242),
            Color.FromArgb(114, 200, 242),
            Color.FromArgb(121, 201, 241),
            Color.FromArgb(127, 202, 240),
            Color.FromArgb(133, 203, 239),
            Color.FromArgb(140, 205, 239),
            Color.FromArgb(143, 206, 239),
            Color.FromArgb(146, 208, 239),
            Color.FromArgb(149, 209, 240),
            Color.FromArgb(152, 211, 240),
            Color.FromArgb(155, 212, 241),
            Color.FromArgb(158, 214, 241),
            Color.FromArgb(161, 215, 242),
            Color.FromArgb(164, 217, 242),
            Color.FromArgb(168, 218, 243),
            Color.FromArgb(171, 220, 243),
            Color.FromArgb(174, 221, 244),
            Color.FromArgb(177, 222, 244),
            Color.FromArgb(180, 224, 245),
            Color.FromArgb(184, 225, 245),
            Color.FromArgb(187, 227, 246),
            Color.FromArgb(190, 228, 246),
            Color.FromArgb(193, 230, 247),
            Color.FromArgb(197, 231, 247),
            Color.FromArgb(200, 233, 248),
            Color.FromArgb(203, 234, 248),
            Color.FromArgb(207, 236, 249),
            Color.FromArgb(255, 255, 200),
            Color.FromArgb(255, 254, 192),
            Color.FromArgb(255, 254, 185),
            Color.FromArgb(255, 253, 178),
            Color.FromArgb(255, 252, 171),
            Color.FromArgb(255, 251, 164),
            Color.FromArgb(255, 250, 157),
            Color.FromArgb(255, 249, 149),
            Color.FromArgb(255, 248, 142),
            Color.FromArgb(255, 247, 135),
            Color.FromArgb(255, 246, 128),
            Color.FromArgb(255, 244, 121),
            Color.FromArgb(255, 243, 114),
            Color.FromArgb(255, 241, 107),
            Color.FromArgb(255, 239, 99),
            Color.FromArgb(255, 238, 92),
            Color.FromArgb(255, 236, 85),
            Color.FromArgb(255, 234, 78),
            Color.FromArgb(255, 231, 71),
            Color.FromArgb(255, 229, 64),
            Color.FromArgb(255, 227, 57),
            Color.FromArgb(255, 225, 49),
            Color.FromArgb(255, 222, 47),
            Color.FromArgb(255, 220, 45),
            Color.FromArgb(255, 218, 42),
            Color.FromArgb(255, 215, 40),
            Color.FromArgb(255, 213, 38),
            Color.FromArgb(255, 211, 35),
            Color.FromArgb(255, 208, 33),
            Color.FromArgb(255, 206, 30),
            Color.FromArgb(255, 203, 28),
            Color.FromArgb(255, 201, 26),
            Color.FromArgb(255, 198, 23),
            Color.FromArgb(255, 195, 21),
            Color.FromArgb(255, 193, 19),
            Color.FromArgb(255, 190, 16),
            Color.FromArgb(255, 187, 14),
            Color.FromArgb(255, 184, 11),
            Color.FromArgb(255, 181, 9),
            Color.FromArgb(255, 179, 7),
            Color.FromArgb(255, 176, 4),
            Color.FromArgb(255, 173, 2),
            Color.FromArgb(255, 170, 0),
            Color.FromArgb(255, 167, 0),
            Color.FromArgb(255, 164, 0),
            Color.FromArgb(255, 161, 0),
            Color.FromArgb(255, 158, 0),
            Color.FromArgb(255, 155, 0),
            Color.FromArgb(255, 152, 0),
            Color.FromArgb(255, 149, 0),
            Color.FromArgb(255, 147, 0),
            Color.FromArgb(255, 144, 0),
            Color.FromArgb(255, 141, 0),
            Color.FromArgb(255, 138, 0),
            Color.FromArgb(255, 135, 0),
            Color.FromArgb(255, 132, 0),
            Color.FromArgb(255, 129, 0),
            Color.FromArgb(255, 127, 0),
            Color.FromArgb(255, 124, 0),
            Color.FromArgb(255, 121, 0),
            Color.FromArgb(255, 118, 0),
            Color.FromArgb(255, 115, 0),
            Color.FromArgb(255, 112, 0),
            Color.FromArgb(255, 110, 0),
            Color.FromArgb(255, 104, 0),
            Color.FromArgb(255, 99, 0),
            Color.FromArgb(255, 94, 0),
            Color.FromArgb(255, 89, 0),
            Color.FromArgb(255, 83, 0),
            Color.FromArgb(255, 78, 0),
            Color.FromArgb(255, 73, 0),
            Color.FromArgb(255, 68, 0),
            Color.FromArgb(255, 62, 0),
            Color.FromArgb(255, 57, 0),
            Color.FromArgb(255, 52, 0),
            Color.FromArgb(255, 47, 0),
            Color.FromArgb(255, 41, 0),
            Color.FromArgb(255, 36, 0),
            Color.FromArgb(255, 31, 0),
            Color.FromArgb(255, 26, 0),
            Color.FromArgb(255, 20, 0),
            Color.FromArgb(255, 15, 0),
            Color.FromArgb(255, 10, 0),
            Color.FromArgb(255, 5, 0),
            Color.FromArgb(255, 0, 0),
            Color.FromArgb(252, 0, 0),
            Color.FromArgb(249, 0, 0),
            Color.FromArgb(247, 0, 0),
            Color.FromArgb(244, 0, 0),
            Color.FromArgb(241, 0, 0),
            Color.FromArgb(239, 0, 0),
            Color.FromArgb(236, 0, 0),
            Color.FromArgb(234, 0, 0),
            Color.FromArgb(231, 0, 0),
            Color.FromArgb(228, 0, 0),
            Color.FromArgb(226, 0, 0),
            Color.FromArgb(223, 0, 0),
            Color.FromArgb(220, 0, 0),
            Color.FromArgb(218, 0, 0),
            Color.FromArgb(215, 0, 0),
            Color.FromArgb(213, 0, 0),
            Color.FromArgb(210, 0, 0),
            Color.FromArgb(207, 0, 0),
            Color.FromArgb(205, 0, 0),
            Color.FromArgb(202, 0, 0),
            Color.FromArgb(200, 0, 0),
            Color.FromArgb(202, 6, 6),
            Color.FromArgb(205, 13, 13),
            Color.FromArgb(207, 20, 20),
            Color.FromArgb(210, 27, 27),
            Color.FromArgb(213, 35, 35),
            Color.FromArgb(215, 43, 43),
            Color.FromArgb(218, 50, 50),
            Color.FromArgb(220, 58, 58),
            Color.FromArgb(223, 66, 66),
            Color.FromArgb(226, 75, 75),
            Color.FromArgb(228, 83, 83),
            Color.FromArgb(231, 92, 92),
            Color.FromArgb(234, 101, 101),
            Color.FromArgb(236, 110, 110),
            Color.FromArgb(239, 119, 119),
            Color.FromArgb(241, 128, 128),
            Color.FromArgb(244, 138, 138),
            Color.FromArgb(247, 147, 147),
            Color.FromArgb(249, 157, 157),
            Color.FromArgb(252, 167, 167),
            Color.FromArgb(255, 178, 178)
        };

        #region Methods       
        /// <summary>
        /// Create legend scheme from grid data
        /// </summary>
        /// <param name="aGridData">GridData</param>
        /// <param name="aLT">legend type</param>
        /// <param name="aST">shape type</param>
        /// <returns>legend scheme</returns>
        public static LegendScheme CreateLegendSchemeFromGridData(GridData aGridData,
            LegendType aLT, ShapeTypes aST)
        {
            LegendScheme aLS = new LegendScheme(aST);
            double[] CValues;
            Color[] colors;
            double MinData = 0;
            double MaxData = 0;

            bool hasNoData = ContourDraw.GetMaxMinValue(aGridData.Data, aGridData.MissingValue, ref MinData, ref MaxData);
            CValues = CreateContourValues(MinData, MaxData);
            colors = CreateRainBowColors(CValues.Length + 1);

            //Generate lengendscheme  
            if (aLT == LegendType.UniqueValue)
            {
                aLS = CreateUniqValueLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, aGridData.MissingValue);
            }
            else
            {
                aLS = CreateGraduatedLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, aGridData.MissingValue);
            }

            return aLS;
        }

        /// <summary>
        /// Create legend scheme from grid data
        /// </summary>
        /// <param name="aGridData">GridData</param>
        /// <param name="aLT">legend type</param>
        /// <param name="aST">shape type</param>
        /// <param name="hasNoData">ref if has undefine data</param>
        /// <returns>legend scheme</returns>
        public static LegendScheme CreateLegendSchemeFromGridData(GridData aGridData,
            LegendType aLT, ShapeTypes aST, ref Boolean hasNoData)
        {
            LegendScheme aLS = new LegendScheme(aST);
            double[] CValues;
            Color[] colors;            
            double MinData = 0;
            double MaxData = 0;

            hasNoData = ContourDraw.GetMaxMinValue(aGridData.Data, aGridData.MissingValue, ref MinData, ref MaxData);            
            CValues = CreateContourValues(MinData, MaxData);
            colors = CreateRainBowColors(CValues.Length + 1);

            //Generate lengendscheme  
            if (aLT == LegendType.UniqueValue)
            {
                aLS = CreateUniqValueLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, aGridData.MissingValue);
            }
            else
            {
                aLS = CreateGraduatedLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, aGridData.MissingValue);
            }

            return aLS;
        }

        /// <summary>
        /// Create legend scheme from station data
        /// </summary>
        /// <param name="stationData">station data</param>
        /// <param name="aLT">legend type</param>
        /// <param name="aST">shape type</param>
        /// <returns>legend scheme</returns>
        public static LegendScheme CreateLegendSchemeFromStationData(StationData stationData,
            LegendType aLT, ShapeTypes aST)
        {
            LegendScheme aLS = null;
            double[] CValues;
            Color[] colors;
            double MinData = 0;
            double MaxData = 0;

            bool hasNoData = ContourDraw.GetMaxMinValueFDiscreteData(stationData.Data, stationData.MissingValue, ref MinData, ref MaxData);
            CValues = CreateContourValues(MinData, MaxData);
            colors = CreateRainBowColors(CValues.Length + 1);

            //Generate lengendscheme                       
            if (aLT == LegendType.UniqueValue)
            {
                aLS = CreateUniqValueLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, stationData.MissingValue);
            }
            else
            {
                aLS = CreateGraduatedLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, stationData.MissingValue);
            }

            return aLS;
        }


        /// <summary>
        /// Create legend scheme from station data
        /// </summary>
        /// <param name="stationData">station data</param>
        /// <param name="aLT">legend type</param>
        /// <param name="aST">shape type</param>
        /// <param name="hasNoData">undefine data</param>
        /// <returns>legend scheme</returns>
        public static LegendScheme CreateLegendSchemeFromStationData(StationData stationData,
            LegendType aLT, ShapeTypes aST, ref Boolean hasNoData)
        {
            LegendScheme aLS = null;
            double[] CValues;
            Color[] colors;            
            double MinData = 0;
            double MaxData = 0;

            hasNoData = ContourDraw.GetMaxMinValueFDiscreteData(stationData.Data, stationData.MissingValue, ref MinData, ref MaxData);            
            CValues = CreateContourValues(MinData, MaxData);
            colors = CreateRainBowColors(CValues.Length + 1);

            //Generate lengendscheme                       
            if (aLT == LegendType.UniqueValue)
            {
                aLS = CreateUniqValueLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, stationData.MissingValue);
            }
            else
            {
                aLS = CreateGraduatedLegendScheme(CValues, colors,
                    aST, MinData, MaxData, hasNoData, stationData.MissingValue);
            }

            return aLS;
        }

        /// <summary>
        /// Create single symbol legend scheme
        /// </summary>
        /// <param name="aST">shape type</param>
        /// <param name="aColor">color</param>
        /// <param name="size">size</param>        
        /// <returns>legend scheme</returns>
        public static LegendScheme CreateSingleSymbolLegendScheme(ShapeTypes aST, Color aColor,
            Single size)
        {
            LegendScheme legendScheme = new LegendScheme(aST);
            legendScheme.LegendType = LegendType.SingleSymbol;
            legendScheme.ShapeType = aST;
            legendScheme.MinValue = 0;
            legendScheme.MaxValue = 0;
            legendScheme.MissingValue = -9999;
            legendScheme.LegendBreaks = new List<ColorBreak>();
            switch (aST)
            {
                case ShapeTypes.Point:
                    PointBreak aPB = new PointBreak();
                    aPB.Color = aColor;
                    aPB.OutlineColor = Color.Black;
                    aPB.Size = size;
                    aPB.IsNoData = false;
                    aPB.DrawFill = true;
                    aPB.DrawOutline = true;
                    aPB.DrawShape = true;
                    aPB.Style = PointStyle.Circle;
                    aPB.StartValue = 0;
                    aPB.EndValue = 0;
                    aPB.Caption = "";
                    legendScheme.LegendBreaks.Add(aPB);
                    break;
                case ShapeTypes.Polyline:
                case ShapeTypes.PolylineZ:
                    PolyLineBreak aPLB = new PolyLineBreak();
                    aPLB.Color = aColor;
                    aPLB.DrawPolyline = true;
                    aPLB.Size = size;
                    aPLB.Style = LineStyles.Solid;
                    aPLB.StartValue = 0;
                    aPLB.EndValue = 0;
                    aPLB.Caption = "";
                    aPLB.SymbolColor = aColor;
                    legendScheme.LegendBreaks.Add(aPLB);
                    break;
                case ShapeTypes.Polygon:
                    PolygonBreak aPGB = new PolygonBreak();
                    aPGB.Color = aColor;
                    aPGB.DrawFill = true;
                    aPGB.DrawOutline = true;
                    aPGB.DrawShape = true;
                    aPGB.OutlineColor = Color.Gray;
                    aPGB.OutlineSize = size;
                    aPGB.StartValue = 0;
                    aPGB.EndValue = 0;
                    aPGB.Caption = "";
                    legendScheme.LegendBreaks.Add(aPGB);
                    break;
            }
            //legendScheme.BreakNum = legendScheme.LegendBreaks.Count;

            return legendScheme;
        }

        /// <summary>
        /// Create unique value legend scheme
        /// </summary>
        /// <param name="CValues"></param>
        /// <param name="colors"></param>
        /// <param name="aST"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="hasNodata"></param>
        /// <param name="unDef"></param>
        /// <returns></returns>
        public static LegendScheme CreateUniqValueLegendScheme(double[] CValues, Color[] colors, ShapeTypes aST,
            double min, double max, Boolean hasNodata, double unDef)
        {
            LegendScheme legendScheme = new LegendScheme(aST);
            legendScheme.LegendType = LegendType.UniqueValue;
            legendScheme.ShapeType = aST;
            legendScheme.LegendBreaks = new List<ColorBreak>();
            legendScheme.MinValue = min;
            legendScheme.MaxValue = max;
            legendScheme.MissingValue = unDef;
            int i;
            switch (aST)
            {
                case ShapeTypes.Point:                    
                    for (i = 1; i < colors.Length; i++)
                    {
                        PointBreak aPB = new PointBreak();
                        aPB.Color = colors[i];
                        aPB.StartValue = CValues[i - 1];
                        aPB.EndValue = aPB.StartValue;
                        aPB.Size = (Single)i / 2 + 2;
                        aPB.Style = PointStyle.Circle;
                        aPB.OutlineColor = Color.Black;
                        aPB.IsNoData = false;
                        aPB.DrawOutline = true;
                        aPB.DrawFill = true;
                        aPB.DrawShape = true;
                        aPB.Caption = aPB.StartValue.ToString();

                        legendScheme.LegendBreaks.Add(aPB);
                    }
                    legendScheme.HasNoData = false;
                    break;
                case ShapeTypes.Polyline: 
                case ShapeTypes.PolylineZ:
                    for (i = 1; i < colors.Length; i++)
                    {
                        PolyLineBreak aPLB = new PolyLineBreak();
                        aPLB.Color = colors[i];
                        aPLB.StartValue = CValues[i - 1];
                        aPLB.EndValue = aPLB.StartValue;
                        aPLB.Size = 1.0F;
                        aPLB.Style = LineStyles.Solid;
                        aPLB.DrawPolyline = true;
                        aPLB.Caption = aPLB.StartValue.ToString();
                        aPLB.SymbolColor = aPLB.Color;
                        if (Enum.IsDefined(typeof(PointStyle), i))
                            aPLB.SymbolStyle = (PointStyle)i;
                        legendScheme.LegendBreaks.Add(aPLB);
                    }
                    legendScheme.HasNoData = false;
                    break;
                case ShapeTypes.Polygon:                    
                    for (i = 1; i < colors.Length; i++)
                    {
                        PolygonBreak aPGB = new PolygonBreak();
                        aPGB.Color = colors[i];
                        aPGB.OutlineColor = Color.Gray;
                        aPGB.OutlineSize = 1.0F;
                        aPGB.DrawFill = true;
                        aPGB.DrawOutline = true;
                        aPGB.DrawShape = true;
                        aPGB.StartValue = CValues[i - 1];
                        aPGB.EndValue = aPGB.StartValue;
                        aPGB.Caption = aPGB.StartValue.ToString();
                        if (Enum.IsDefined(typeof(HatchStyle), i))
                            aPGB.Style = (HatchStyle)i;

                        legendScheme.LegendBreaks.Add(aPGB);
                    }
                    legendScheme.HasNoData = false;
                    break;
            }
            //legendScheme.breakNum = legendScheme.LegendBreaks.Count;

            return legendScheme;
        }

        /// <summary>
        /// Create unique value legend scheme
        /// </summary>
        /// <param name="CValues"></param>
        /// <param name="colors"></param>
        /// <param name="aST"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="hasNodata"></param>
        /// <param name="unDef"></param>
        /// <returns></returns>
        public static LegendScheme CreateUniqValueLegendScheme(List<string> CValues, Color[] colors, ShapeTypes aST,
            double min, double max, Boolean hasNodata, double unDef)
        {
            return CreateUniqValueLegendScheme(CValues, CValues, colors, aST, min, max, hasNodata, unDef);
        }

        /// <summary>
        /// Create unique value legend scheme
        /// </summary>
        /// <param name="CValues"></param>
        /// <param name="captions"></param>
        /// <param name="colors"></param>
        /// <param name="aST"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="hasNodata"></param>
        /// <param name="unDef"></param>
        /// <returns></returns>
        public static LegendScheme CreateUniqValueLegendScheme(List<string> CValues, List<string> captions, Color[] colors, ShapeTypes aST,
            double min, double max, Boolean hasNodata, double unDef)
        {
            LegendScheme legendScheme = new LegendScheme(aST);
            legendScheme.LegendType = LegendType.UniqueValue;
            legendScheme.ShapeType = aST;
            legendScheme.LegendBreaks = new List<ColorBreak>();
            legendScheme.MinValue = min;
            legendScheme.MaxValue = max;
            legendScheme.MissingValue = unDef;
            int i;
            List<int> idxList = new List<int>();
            switch (aST)
            {
                case ShapeTypes.Point:
                case ShapeTypes.PointZ:
                    for (i = 1; i < colors.Length; i++)
                    {
                        PointBreak aPB = new PointBreak();
                        aPB.Color = colors[i];
                        aPB.StartValue = CValues[i - 1];
                        aPB.EndValue = aPB.StartValue;
                        if (colors.Length <= 13)
                        {
                            aPB.Size = (Single)i / 2 + 2;
                        }
                        else
                        {
                            aPB.Size = 5;
                        }
                        aPB.Style = PointStyle.Circle;
                        aPB.OutlineColor = Color.Black;
                        aPB.IsNoData = false;
                        aPB.DrawOutline = true;
                        aPB.DrawFill = true;
                        aPB.DrawShape = true;
                        aPB.Caption = captions[i-1];

                        legendScheme.LegendBreaks.Add(aPB);
                    }
                    legendScheme.HasNoData = false;
                    break;
                case ShapeTypes.Polyline:
                case ShapeTypes.PolylineZ:                    
                    idxList = GetEnumList(colors.Length, typeof(PointStyle));
                    for (i = 1; i < colors.Length; i++)
                    {
                        PolyLineBreak aPLB = new PolyLineBreak();
                        aPLB.Color = colors[i];
                        aPLB.StartValue = CValues[i - 1];
                        aPLB.EndValue = aPLB.StartValue;
                        aPLB.Size = 1.0F;
                        aPLB.Style = LineStyles.Solid;
                        aPLB.DrawPolyline = true;
                        aPLB.Caption = captions[i - 1];
                        aPLB.SymbolColor = aPLB.Color;                        
                        aPLB.SymbolStyle = (PointStyle)idxList[i];

                        legendScheme.LegendBreaks.Add(aPLB);
                    }
                    legendScheme.HasNoData = false;
                    break;
                case ShapeTypes.Polygon:
                    idxList = GetEnumList(colors.Length, typeof(HatchStyle));
                    for (i = 1; i < colors.Length; i++)
                    {
                        PolygonBreak aPGB = new PolygonBreak();
                        aPGB.Color = colors[i];
                        aPGB.OutlineColor = Color.Gray;
                        aPGB.OutlineSize = 1.0F;
                        aPGB.DrawFill = true;
                        aPGB.DrawOutline = true;
                        aPGB.DrawShape = true;
                        aPGB.StartValue = CValues[i - 1];
                        aPGB.EndValue = aPGB.StartValue;
                        aPGB.Caption = captions[i - 1];                        
                        aPGB.Style = (HatchStyle)idxList[i];

                        legendScheme.LegendBreaks.Add(aPGB);
                    }
                    legendScheme.HasNoData = false;
                    break;
            }
            //legendScheme.breakNum = legendScheme.LegendBreaks.Count;

            return legendScheme;
        }

        /// <summary>
        /// Create unique value legend scheme from layer
        /// </summary>
        /// <param name="aLayer"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static LegendScheme CreateUniqValueLegendScheme(VectorLayer aLayer, double min, double max)
        {
            LegendScheme aLS = new LegendScheme(aLayer.ShapeType);
            double[] CValues;
            Color[] colors;            
            ArrayList valueList = new ArrayList();
            
            switch (aLayer.ShapeType)
            {
                case ShapeTypes.Point:
                    foreach (PointShape aPS in aLayer.ShapeList)
                    {
                        if (!valueList.Contains(aPS.Value))
                        {
                            valueList.Add(aPS.Value);
                        }
                    }
                    break;
                case ShapeTypes.Polyline:
                case ShapeTypes.PolylineZ:
                    foreach (PolylineShape aPLS in aLayer.ShapeList)
                    {
                        if (!valueList.Contains(aPLS.value))
                        {
                            valueList.Add(aPLS.value);
                        }
                    }
                    break;
                default:
                    foreach (PolygonShape aPGS in aLayer.ShapeList)
                    {
                        if (!valueList.Contains(aPGS.lowValue))
                        {
                            valueList.Add(aPGS.lowValue);
                        }
                    }
                    break;
            }

            CValues = (double[])valueList.ToArray(typeof(double));
            if (CValues.Length <= 13)
            {
                colors = CreateRainBowColors(CValues.Length);
            }
            else
            {
                colors = CreateRandomColors(CValues.Length);
            }
            List<Color> CList = new List<Color>(colors);
            CList.Insert(0, Color.White);
            colors = CList.ToArray();

            aLS = CreateUniqValueLegendScheme(CValues, colors,
                aLayer.ShapeType, min, max, false, -9999);

            return aLS;
        }

        /// <summary>
        /// Create Graduated color legend scheme
        /// </summary>
        /// <param name="CValues"></param>
        /// <param name="colors"></param>
        /// <param name="aST"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="hasNodata"></param>
        /// <param name="unDef"></param>
        /// <returns></returns>
        public static LegendScheme CreateGraduatedLegendScheme(double[] CValues, Color[] colors, ShapeTypes aST,
            double min, double max, Boolean hasNodata, double unDef)
        {
            LegendScheme legendScheme = new LegendScheme(aST);
            legendScheme.LegendType = LegendType.GraduatedColor;
            legendScheme.ShapeType = aST;
            legendScheme.LegendBreaks = new List<ColorBreak>();
            legendScheme.MinValue = min;
            legendScheme.MaxValue = max;
            legendScheme.MissingValue = unDef;
            int i;
            switch (aST)
            {
                case ShapeTypes.Point:                    
                    for (i = 0; i < colors.Length; i++)
                    {
                        PointBreak aPB = new PointBreak();
                        aPB.Color = colors[i];
                        aPB.OutlineColor = Color.Black;
                        aPB.IsNoData = false;
                        aPB.DrawOutline = true;
                        aPB.DrawFill = true;
                        aPB.DrawShape = true;
                        if (i == 0)
                        {
                            aPB.StartValue = min;
                        }
                        else
                        {
                            aPB.StartValue = CValues[i - 1];
                        }
                        if (i == colors.Length - 1)
                        {
                            aPB.EndValue = max;
                        }
                        else
                        {
                            aPB.EndValue = CValues[i];
                        }
                        aPB.Size = (Single)i / 2 + 2;
                        aPB.Style = PointStyle.Circle;
                        if (aPB.StartValue == aPB.EndValue)
                        {
                            aPB.Caption = aPB.StartValue.ToString();
                        }
                        else
                        {
                            if (i == 0)
                                aPB.Caption = "< " + aPB.EndValue.ToString();
                            else if (i == colors.Length - 1)
                                aPB.Caption = "> " + aPB.StartValue.ToString();
                            else
                                aPB.Caption = aPB.StartValue.ToString() + " - " + aPB.EndValue.ToString();
                        }

                        legendScheme.LegendBreaks.Add(aPB);
                    }
                    legendScheme.HasNoData = false;
                    if (hasNodata)
                    {
                        PointBreak aPB = new PointBreak();
                        aPB.Color = Color.Gray;
                        aPB.OutlineColor = Color.Black;
                        aPB.StartValue = unDef;
                        aPB.EndValue = aPB.StartValue;
                        aPB.Size = 1;
                        aPB.Style = PointStyle.Circle;
                        aPB.Caption = "NoData";
                        aPB.IsNoData = true;
                        aPB.DrawShape = true;
                        aPB.DrawOutline = true;
                        legendScheme.LegendBreaks.Add(aPB);
                        legendScheme.HasNoData = true;
                    }
                    break;
                case ShapeTypes.Polyline:     
                case ShapeTypes.PolylineZ:
                    for (i = 0; i < colors.Length; i++)
                    {
                        PolyLineBreak aPLB = new PolyLineBreak();
                        aPLB.Color = colors[i];
                        aPLB.Size = 1.0F;
                        aPLB.Style = LineStyles.Solid;
                        aPLB.DrawPolyline = true;
                        if (i == 0)
                        {
                            aPLB.StartValue = min;
                        }
                        else
                        {
                            aPLB.StartValue = CValues[i - 1];
                        }
                        if (i == colors.Length - 1)
                        {
                            aPLB.EndValue = max;
                        }
                        else
                        {
                            aPLB.EndValue = CValues[i];
                        }
                        if (aPLB.StartValue == aPLB.EndValue)
                        {
                            aPLB.Caption = aPLB.StartValue.ToString();
                        }
                        else
                        {
                            if (i == 0)
                                aPLB.Caption = "< " + aPLB.EndValue.ToString();
                            else if (i == colors.Length - 1)
                                aPLB.Caption = "> " + aPLB.StartValue.ToString();
                            else
                                aPLB.Caption = aPLB.StartValue.ToString() + " - " + aPLB.EndValue.ToString();
                        }
                        aPLB.SymbolColor = aPLB.Color;
                        if (Enum.IsDefined(typeof(PointStyle), i))
                            aPLB.SymbolStyle = (PointStyle)i;

                        legendScheme.LegendBreaks.Add(aPLB);
                    }
                    legendScheme.HasNoData = false;
                    break;
                case ShapeTypes.Polygon:                    
                    for (i = 0; i < colors.Length; i++)
                    {
                        PolygonBreak aPGB = new PolygonBreak();
                        aPGB.Color = colors[i];
                        aPGB.OutlineColor = Color.Gray;
                        aPGB.OutlineSize = 1.0F;
                        aPGB.DrawFill = true;
                        aPGB.DrawOutline = false;
                        aPGB.DrawShape = true;
                        if (i == 0)
                        {
                            aPGB.StartValue = min;
                        }
                        else
                        {
                            aPGB.StartValue = CValues[i - 1];
                        }
                        if (i == colors.Length - 1)
                        {
                            aPGB.EndValue = max;
                        }
                        else
                        {
                            aPGB.EndValue = CValues[i];
                        }
                        if (aPGB.StartValue == aPGB.EndValue)
                        {
                            aPGB.Caption = aPGB.StartValue.ToString();
                        }
                        else
                        {
                            if (i == 0)
                                aPGB.Caption = "< " + aPGB.EndValue.ToString();
                            else if (i == colors.Length - 1)
                                aPGB.Caption = "> " + aPGB.StartValue.ToString();
                            else
                                aPGB.Caption = aPGB.StartValue.ToString() + " - " + aPGB.EndValue.ToString();
                        }
                        if (Enum.IsDefined(typeof(HatchStyle), i))
                            aPGB.Style = (HatchStyle)i;

                        legendScheme.LegendBreaks.Add(aPGB);
                    }
                    legendScheme.HasNoData = false;
                    break;
                case ShapeTypes.Image:                    
                    for (i = 0; i < colors.Length; i++)
                    {
                        ColorBreak aCB = new ColorBreak();
                        aCB.Color = colors[i];                        
                        if (i == 0)
                        {
                            aCB.StartValue = min;
                        }
                        else
                        {
                            aCB.StartValue = CValues[i - 1];
                        }
                        if (i == colors.Length - 1)
                        {
                            aCB.EndValue = max;
                        }
                        else
                        {
                            aCB.EndValue = CValues[i];
                        }
                        if (aCB.StartValue == aCB.EndValue)
                        {
                            aCB.Caption = aCB.StartValue.ToString();
                        }
                        else
                        {
                            if (i == 0)
                                aCB.Caption = "< " + aCB.EndValue.ToString();
                            else if (i == colors.Length - 1)
                                aCB.Caption = "> " + aCB.StartValue.ToString();
                            else
                                aCB.Caption = aCB.StartValue.ToString() + " - " + aCB.EndValue.ToString();
                        }

                        legendScheme.LegendBreaks.Add(aCB);
                    }
                    legendScheme.HasNoData = false;
                    if (hasNodata)
                    {
                        ColorBreak aCB = new ColorBreak();
                        aCB.Color = Color.Gray;                        
                        aCB.StartValue = unDef;
                        aCB.EndValue = aCB.StartValue;                        
                        aCB.Caption = "NoData";
                        aCB.IsNoData = true;                        
                        legendScheme.LegendBreaks.Add(aCB);
                        legendScheme.HasNoData = true;
                    }
                    break;
            }
            //legendScheme.breakNum = legendScheme.LegendBreaks.Count;

            return legendScheme;
        }

        /// <summary>
        /// Set contour values and colors
        /// </summary>
        /// <param name="aLS"></param>
        /// <param name="cValues"></param>
        /// <param name="colors"></param>
        public static void SetContoursAndColors(LegendScheme aLS, ref double[] cValues, ref Color[] colors)
        {
            int i;
            if (aLS.HasNoData)
            {
                cValues = new double[aLS.BreakNum - 2];
                colors = new Color[aLS.BreakNum - 1];
            }
            else
            {
                cValues = new double[aLS.BreakNum - 1];
                colors = new Color[aLS.BreakNum];
            }
            switch (aLS.ShapeType)
            {
                case ShapeTypes.Polygon:                    
                    for (i = 0; i < aLS.BreakNum; i++)
                    {
                        PolygonBreak aPGB = (PolygonBreak)aLS.LegendBreaks[i];
                        colors[i] = aPGB.Color;
                        if (i > 0)
                        {
                            cValues[i - 1] = double.Parse(aPGB.StartValue.ToString());
                        }
                    }
                    break;
                case ShapeTypes.Polyline:                    
                    if (aLS.LegendType == LegendType.UniqueValue)
                    {
                        cValues = new double[aLS.BreakNum];
                        colors = new Color[aLS.BreakNum + 1];
                        colors[0] = Color.White;
                        for (i = 0; i < aLS.BreakNum; i++)
                        {
                            PolyLineBreak aPLB = (PolyLineBreak)aLS.LegendBreaks[i];
                            colors[i + 1] = aPLB.Color;
                            cValues[i] = double.Parse(aPLB.StartValue.ToString());
                        }
                    }
                    else
                    {
                        for (i = 0; i < aLS.BreakNum; i++)
                        {
                            PolyLineBreak aPLB = (PolyLineBreak)aLS.LegendBreaks[i];
                            colors[i] = aPLB.Color;
                            if (i > 0)
                            {
                                cValues[i - 1] = double.Parse(aPLB.StartValue.ToString());
                            }
                        }
                    }
                    break;
                case ShapeTypes.Point:                   
                    for (i = 0; i < aLS.BreakNum; i++)
                    {
                        PointBreak aPB = (PointBreak)aLS.LegendBreaks[i];
                        if (!aPB.IsNoData)
                        {
                            colors[i] = aPB.Color;
                            if (i > 0)
                            {
                                cValues[i - 1] = double.Parse(aPB.StartValue.ToString());
                            }
                        }
                    }
                    break;
            }
        }

        private static List<int> GetStyleIndexList(int breakNum, int styleNum)
        {
            List<int> indexList = new List<int>();
            int i;

            if (breakNum <= styleNum)
            {
                for (i = 0; i < breakNum; i++)
                {
                    indexList.Add(i);
                }
            }
            else
            {                
                int idx = 0;
                while (idx < breakNum)
                {
                    for (i = 0; i < styleNum; i++)
                    {
                        idx += 1;
                        indexList.Add(i);
                    }
                }
            }

            return indexList;
        }

        private static List<int> GetEnumList(int breakNum, Type enumType)
        {
            List<int> indexList = new List<int>();
            int i;
            Array enums = Enum.GetValues(enumType);
            int vNum = enums.Length;
            if (breakNum <= vNum)
            {
                for (i = 0; i < breakNum; i++)
                {
                    indexList.Add((int)enums.GetValue(i));
                }
            }
            else
            {
                int idx = 0;
                while (idx < breakNum)
                {
                    for (i = 0; i < vNum; i++)
                    {
                        idx += 1;
                        indexList.Add((int)enums.GetValue(i));
                    }
                }
            }

            return indexList;
        }

        /// <summary>
        /// Create rainbow colors
        /// </summary>
        /// <param name="cNum"></param>
        /// <returns></returns>
        public static Color[] CreateRainBowColors(int cNum)
        {
            if (cNum > _rainbowColors.Length)
            {
                return GetRainBowColors_HSL(cNum);
                //return GetRainBowColors_HSV(cNum);
            }

            ArrayList colorList = new ArrayList();

            for (int i = 0; i < cNum; i++)
            {
                int delta = _rainbowColors.Length / (cNum + 1);
                colorList.Add(_rainbowColors[(i + 1) * delta]);
            }

            return colorList.ToArray(typeof(Color)) as Color[];
        }

        /// <summary>
        /// Get rainbow colors
        /// </summary>
        /// <param name="cNum">color number</param>
        /// <returns>colors</returns>
        public static Color[] GetRainBowColors_HSL(int cNum)
        {
            double delta = 1.0 / cNum;
            Color[] colors = new Color[cNum];
            int n = cNum - 1;
            for (double i = 0; i < 1; i += delta)
            {
                if (n == -1)
                    break;

                ColorUtils.HSL hsl = new ColorUtils.HSL(i, 0.5, 0.5);
                colors[n] = ColorUtils.HSLToRGB(hsl);
                n -= 1;
            }

            return colors;
        }

        /// <summary>
        /// Get rainbow colors
        /// </summary>
        /// <param name="cNum">color number</param>
        /// <returns>colors</returns>
        public static Color[] GetRainBowColors_HSV(int cNum)
        {
            double p = 360.0 / cNum;
            Color[] colors = new Color[cNum];
            for (int i = 0; i < cNum; i++)
            {
                ColorUtils.HSV hsv = new ColorUtils.HSV(i * p, 1.0, 1.0);
                colors[cNum - i - 1] = ColorUtils.HSVToRGB(hsv);
            }

            return colors;
        }

        /// <summary>
        /// Create random colors
        /// </summary>
        /// <param name="cNum"></param>
        /// <returns></returns>
        public static Color[] CreateRandomColors(int cNum)
        {
            Color[] colors = new Color[cNum];
            int i;
            Random randomColor = new Random();

            for (i = 0; i < cNum; i++)
            {
                colors[i] = Color.FromArgb(randomColor.Next(0, 256),
                    randomColor.Next(0, 256), randomColor.Next(0, 256));
            }

            return colors;
        }

        /// <summary>
        /// Create contour values by minimum and maximum data
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double[] CreateContourValues(double min, double max)
        {
            double[] cValues;
            int i, cNum, aD, aE;
            double cDelt, range, newMin;
            string eStr;

            range = max - min;
            eStr = range.ToString("e1");
            aD = int.Parse(eStr.Substring(0, 1));
            aE = int.Parse(eStr.Substring(eStr.IndexOf("e") + 1, eStr.Length - eStr.IndexOf("e") - 1));
            if (aD > 5)
            {
                cDelt = Math.Pow(10, aE);
                cNum = aD;
                //newMin = Convert.ToInt32((min + cDelt) / Math.Pow(10, aE)) * Math.Pow(10, aE);
                newMin = (int)(min / cDelt + 1) * cDelt;
            }
            else if (aD == 5)
            {
                cDelt = aD * Math.Pow(10, aE - 1);
                cNum = 10;
                //newMin = Convert.ToInt32((min + cDelt) / Math.Pow(10, aE)) * Math.Pow(10, aE);
                newMin = (int)(min / cDelt + 1) * cDelt;
                cNum++;
            }
            else
            {
                cDelt = aD * Math.Pow(10, aE - 1);
                cNum = 10;
                //newMin = Convert.ToInt32((min + cDelt) / Math.Pow(10, aE - 1)) * Math.Pow(10, aE - 1);
                newMin = (int)(min / cDelt + 1) * cDelt;
            }

            if (newMin + (cNum - 1) * cDelt > max)
            {
                cNum -= 1;
            }
            cValues = new double[cNum];
            for (i = 0; i < cNum; i++)
            {
                cValues[i] = newMin + i * cDelt;
            }

            return cValues;
        }

        /// <summary>
        /// Create colors from start and end color
        /// </summary>
        /// <param name="sColor"></param>
        /// <param name="eColor"></param>
        /// <param name="cNum"></param>
        /// <returns></returns>
        public static Color[] CreateColors(Color sColor, Color eColor, int cNum)
        {
            Color[] colors = new Color[cNum];
            int sR, sG, sB, eR, eG, eB;
            int rStep, gStep, bStep;
            int i;

            sR = sColor.R;
            sG = sColor.G;
            sB = sColor.B;
            eR = eColor.R;
            eG = eColor.G;
            eB = eColor.B;
            rStep = Convert.ToInt32((eR - sR) / cNum);
            gStep = Convert.ToInt32((eG - sG) / cNum);
            bStep = Convert.ToInt32((eB - sB) / cNum);
            for (i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.FromArgb(sR + i * rStep, sG + i * gStep, sB + i * bStep);
            }

            return colors;
        }

        /// <summary>
        /// Create contour values by interval
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static double[] CreateContourValuesInterval(double min, double max, double interval)
        {
            double[] cValues;
            int cNum = (int)((max - min) / interval) + 1;
            int i;

            cValues = new double[cNum];
            for (i = 0; i < cNum; i++)
            {
                cValues[i] = min + i * interval;
            }

            return cValues;
        }
        
        #endregion
    }
}
