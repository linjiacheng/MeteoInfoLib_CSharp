using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MeteoInfoC.Global;
using MeteoInfoC.Layer;
using MeteoInfoC.Projections;

namespace MeteoInfoC.Data.MeteoData.MICAPS
{
    /// <summary>
    /// MICAPS 131 data info
    /// </summary>
    public class MICAPS131DataInfo : DataInfo,IGridDataInfo
    {
        #region Variables
        ///// <summary>
        ///// File name
        ///// </summary>
        //public string FileName;
        /// <summary>
        /// Description
        /// </summary>
        public string Description;
        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime Time;
        /// <summary>
        /// Y number
        /// </summary>
        public int YNum;
        /// <summary>
        /// X number
        /// </summary>
        public int XNum;
        /// <summary>
        /// Z number
        /// </summary>
        public int ZNum;
        /// <summary>
        /// X delt
        /// </summary>
        public Single XDelt;
        /// <summary>
        /// Y delt
        /// </summary>
        public Single YDelt;
        /// <summary>
        /// Left top longitude
        /// </summary>
        public double Lon_LT;
        /// <summary>
        /// Left top latitude
        /// </summary>
        public double Lat_LT;
        /// <summary>
        /// Center longitude
        /// </summary>
        public double Lon_Center;
        /// <summary>
        /// Center latitude
        /// </summary>
        public double Lat_Center;
        /// <summary>
        /// X array
        /// </summary>
        public double[] X;
        /// <summary>
        /// Y array
        /// </summary>
        public double[] Y;
        /// <summary>
        /// Z List
        /// </summary>
        public List<double> ZList;
        /// <summary>
        /// Data List
        /// </summary>
        public List<double[,]> DataList;
        /// <summary>
        /// StationInfoData
        /// </summary>
        public StationInfoData StationInfo;

        /// <summary>
        /// 
        /// </summary>
        public double[,] MaxData;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MICAPS131DataInfo()
        {

        }

        #endregion

        #region Methods
        /// <summary>
        /// Read data info
        /// </summary>
        /// <param name="aFile">file path</param>
        public override void ReadDataInfo(string aFile)
        {
            FileStream fs = new FileStream(aFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);

            //Read file head
            //string header = System.Text.ASCIIEncoding.ASCII.GetString(br.ReadBytes(128));
            string micapsType = System.Text.Encoding.Default.GetString(br.ReadBytes(12)).Trim();
            Description = System.Text.Encoding.Default.GetString(br.ReadBytes(38)).Trim('\0');

            string flag = System.Text.Encoding.Default.GetString(br.ReadBytes(8)).Trim('\0');
            string version = System.Text.Encoding.Default.GetString(br.ReadBytes(8)).Trim('\0');

            short aYear = br.ReadInt16();
            short aMonth = br.ReadInt16();
            short aDay = br.ReadInt16();
            short aHour = br.ReadInt16();
            short aMinute = br.ReadInt16();
            Time = new DateTime(aYear, aMonth, aDay, aHour, aMinute, 0);

            short interval = br.ReadInt16();

            XNum = br.ReadInt16();
            YNum = br.ReadInt16();
            ZNum = br.ReadInt16();
            int radarCount = br.ReadInt32();

            Lon_LT = br.ReadSingle();
            Lat_LT = br.ReadSingle();
            Lon_Center = br.ReadSingle();
            Lat_Center = br.ReadSingle();

            XDelt = br.ReadSingle();
            YDelt = br.ReadSingle();

            ZList = new List<double>();
            for (int i = 0; i < 40; i++)
            {
                double z = br.ReadSingle();
                if (z != 0)
                {
                    ZList.Add(z);
                }
            }

            //Read Station Info
            StationInfo = new StationInfoData();
            StationInfo.Fields.AddRange(new string[] { "Longitude", "Latitude", "Altitude", "MosiacFlag" });
            for (int i = 0; i < 20; i++)
            {
                string stationName = System.Text.Encoding.Default.GetString(br.ReadBytes(16)).Trim('\0', '　');
                if (!string.IsNullOrEmpty(stationName))
                {
                    StationInfo.Stations.Add(stationName);
                    List<string> fieldList = new List<string>();
                    fieldList.Add("");
                    fieldList.Add("");
                    fieldList.Add("");
                    fieldList.Add("");
                    StationInfo.DataList.Add(fieldList);
                }
            }
            for (int i = 0; i < 20; i++)
            {
                double longitude = br.ReadSingle();
                if (i < radarCount)
                {
                    StationInfo.DataList[i][0] = longitude.ToString();
                }
            }
            for (int i = 0; i < 20; i++)
            {
                double latitude = br.ReadSingle();
                if (i < radarCount)
                {
                    StationInfo.DataList[i][1] = latitude.ToString();
                }
            }
            for (int i = 0; i < 20; i++)
            {
                double altitude = br.ReadSingle();
                if (i < radarCount)
                {
                    StationInfo.DataList[i][2] = altitude.ToString();
                }
            }
            for (int i = 0; i < 20; i++)
            {
                int mosiacFlag = br.ReadChar();
                if (i < radarCount)
                {
                    StationInfo.DataList[i][3] = mosiacFlag.ToString();
                }
            }

            int dataType = br.ReadInt16();
            int levelDim = br.ReadInt16();
            string reserved = System.Text.Encoding.Default.GetString(br.ReadBytes(168));

            DataList = new List<double[,]>();
            MaxData = new double[YNum, XNum];
            for (int i = 0; i < ZNum; i++)
            {
                double[,] aData = new double[YNum, XNum];
                for (int j = 0; j < YNum; j++)
                {
                    for (int k = 0; k < XNum; k++)
                    {
                        int datum = br.ReadByte();

                        aData[YNum - 1 - j, k] = (datum - 66) / 2.0;

                        MaxData[YNum - 1 - j, k] = Math.Max(aData[YNum - 1 - j, k], MaxData[YNum - 1 - j, k]);
                    }
                }

                DataList.Add(aData);
            }

            //Set projection parameters
            GetProjectionInfo();
            CalCoordinate();

            br.Close();
            fs.Close();

            Dimension tdim = new Dimension(DimensionType.T);
            tdim.DimValue.Add(DataConvert.ToDouble(Time));
            tdim.DimLength = 1;
            this.TimeDimension = tdim;
            Dimension zdim = new Dimension(DimensionType.Z);
            zdim.DimValue.AddRange(ZList);
            zdim.DimLength = ZNum;
            Dimension xdim = new Dimension(DimensionType.X);
            xdim.SetValues(X);
            Dimension ydim = new Dimension(DimensionType.Y);
            ydim.SetValues(Y);
            Variable var = new Variable();
            var.Name = "var";
            var.SetDimension(tdim);
            var.SetDimension(zdim);
            var.SetDimension(ydim);
            var.SetDimension(xdim);
            List<Variable> vars = new List<Variable>();
            vars.Add(var);
            this.Variables = vars;
        }

        private void GetProjectionInfo()
        {
            this.ProjectionInfo = KnownCoordinateSystems.Geographic.World.WGS1984;
        }

        private void CalCoordinate()
        {
            X = new double[XNum];
            Y = new double[YNum];

            int i;
            for (i = 0; i < XNum; i++)
                X[i] = Lon_LT + i * XDelt;
            for (i = 0; i < YNum; i++)
                Y[YNum - 1 - i] = Lat_LT - i * YDelt;
        }

        /// <summary>
        /// Get MICAPS 13 data info text
        /// </summary>        
        /// <returns>Info text</returns>
        public override string GenerateInfoText()
        {
            string dataInfo = "";
            dataInfo += "File Name: " + FileName;
            dataInfo += Environment.NewLine + "Description: " + Description;
            dataInfo += Environment.NewLine + "Time: " + Time.ToString("yyyy-MM-dd HH:mm");
            dataInfo += Environment.NewLine + "X number: " + XNum.ToString();
            dataInfo += Environment.NewLine + "Y number: " + YNum.ToString();
            dataInfo += Environment.NewLine + "Left-Top longitude: " + Lon_LT.ToString();
            dataInfo += Environment.NewLine + "Left-Top latitude: " + Lat_LT.ToString();
            dataInfo += Environment.NewLine + "Center longitude: " + Lon_Center.ToString();
            dataInfo += Environment.NewLine + "Center latitude: " + Lat_Center.ToString();
            dataInfo += Environment.NewLine + "Projection: " + "Lon/Lat";
            return dataInfo;
        }

        /// <summary>
        /// Read grid data - LonLat
        /// </summary>
        /// <param name="timeIdx">Time index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="levelIdx">Level index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_LonLat(int timeIdx, int varIdx, int levelIdx)
        {
            GridData gridData = new GridData();
            double[,] gData = new double[YNum, XNum];
            for (int i = 0; i < YNum; i++)
            {
                for (int j = 0; j < XNum; j++)
                {
                    gData[i, j] = DataList[levelIdx][i, j];
                }
            }
            gridData.Data = gData;
            gridData.X = X;
            gridData.Y = Y;

            return gridData;
        }

        /// <summary>
        /// Read grid data - TimeLat
        /// </summary>
        /// <param name="lonIdx">Longitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="levelIdx">Level index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_TimeLat(int lonIdx, int varIdx, int levelIdx)
        {
            return null;
        }

        /// <summary>
        /// Read grid data - TimeLon
        /// </summary>
        /// <param name="latIdx">Latitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="levelIdx">Level index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_TimeLon(int latIdx, int varIdx, int levelIdx)
        {
            return null;
        }

        /// <summary>
        /// Read grid data - LevelLat
        /// </summary>
        /// <param name="lonIdx">Longitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="timeIdx">Time index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_LevelLat(int lonIdx, int varIdx, int timeIdx)
        {
            return null;
        }

        /// <summary>
        /// Read grid data - LevelLon
        /// </summary>
        /// <param name="latIdx">Latitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="timeIdx">Time index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_LevelLon(int latIdx, int varIdx, int timeIdx)
        {
            return null;
        }

        /// <summary>
        /// Read grid data - LevelTime
        /// </summary>
        /// <param name="latIdx">Laititude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="lonIdx">Longitude index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_LevelTime(int latIdx, int varIdx, int lonIdx)
        {
            return null;
        }

        /// <summary>
        /// Read grid data - Time
        /// </summary>
        /// <param name="lonIdx">Longitude index</param>
        /// <param name="latIdx">Latitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="levelIdx">Level index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_Time(int lonIdx, int latIdx, int varIdx, int levelIdx)
        {
            return null;
        }

        /// <summary>
        /// Read grid data - Level
        /// </summary>
        /// <param name="lonIdx">Longitude index</param>
        /// <param name="latIdx">Latitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="timeIdx">Time index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_Level(int lonIdx, int latIdx, int varIdx, int timeIdx)
        {
            return null;
        }

        /// <summary>
        /// Get grid data - Lon
        /// </summary>
        /// <param name="timeIdx">Time index</param>
        /// <param name="latIdx">Latitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="levelIdx">Level Index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_Lon(int timeIdx, int latIdx, int varIdx, int levelIdx)
        {
            return null;
        }

        /// <summary>
        /// Get grid data - Lat
        /// </summary>
        /// <param name="timeIdx">Time index</param>
        /// <param name="lonIdx">Longitude index</param>
        /// <param name="varIdx">Variable index</param>
        /// <param name="levelIdx">Level index</param>
        /// <returns>Grid data</returns>
        public GridData GetGridData_Lat(int timeIdx, int lonIdx, int varIdx, int levelIdx)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GridData GetMaxGridData()
        {
            GridData gridData = new GridData();

            gridData.Data = MaxData;
            gridData.X = this.X;
            gridData.Y = this.Y;

            return gridData;
        }
        #endregion
    }
}
