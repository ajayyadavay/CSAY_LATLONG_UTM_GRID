using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpKml.Dom;
using SharpKml.Base;
using SharpKml.Engine;
using System.Reflection.Emit;

namespace CSAY_LATLONG_UTM_GRID
{
    public partial class FrmMainGrid : Form
    {
        int First_V_Row, First_Hz_Row;
        int Last_V_Row, Last_Hz_Row;
        public FrmMainGrid()
        {
            InitializeComponent();
        }

        private void FrmMainGrid_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            for(int i =0; i <= 6; i++)
            {
                dataGridView1.Rows.Add();
            }

            //For RWY
            string[] RWYList = System.IO.File.ReadAllLines(@".\ComboBoxList\AIRPORT_CODE_ICAO.txt");
            foreach (var line in RWYList)
            {
                ComboBoxAirportCode.Items.Add(line);
            }

        }

        private void LoadRWYdataStripMenuItem1_Click(object sender, EventArgs e)
        {
            string[] ReadingText = new string[50];
            string RWYCoordFilenName;
            string line;
            line = "";
            RWYCoordFilenName = @".\ComboBoxList\" + TxtAirportCode.Text + ".txt";
            StreamReader sr = new StreamReader(RWYCoordFilenName);
            //Read the first line of text
            line = sr.ReadLine();
            ReadingText[0] = line;
            //Continue to read until you reach end of file
            int i = 1;
            while (line != null)
            {
                //Read the next line
                line = sr.ReadLine();
                ReadingText[i] = line;
                i++;
            }
            //close the file
            sr.Close();
            //load data to datagridview by splitting by tab character of coord of RWY
            for (int row = 2; row <= 6; row++)
            {
                string[] splittedtext = ReadingText[row].Split('\t');
                for (int col = 0; col <= 3; col++)
                {
                    dataGridView1.Rows[row - 2].Cells[0].Value = (row - 2).ToString();
                    dataGridView1.Rows[row - 2].Cells[col+1].Value = splittedtext[col];
                }
            }

            //load central meridian of Runway
            for (int row = 0; row <= 0; row++) //row 0 of text file contains info about central meridian
            {
                string[] splittedtext = ReadingText[row].Split('\t');
                for (int col = 1; col <= 1; col++)
                {
                    TxtCM.Text = splittedtext[col];
                }
            }
            //Centerpoint E
            dataGridView1.Rows[5].Cells[0].Value = 5.ToString();
            dataGridView1.Rows[5].Cells[1].Value = "E";
            dataGridView1.Rows[5].Cells[2].Value = "Mid AB";
            double temp_num1, temp_num2;
            temp_num1 = Convert.ToDouble(dataGridView1.Rows[1].Cells[3].Value);
            temp_num2 = Convert.ToDouble(dataGridView1.Rows[1].Cells[3].Value);
            dataGridView1.Rows[5].Cells[3].Value = (temp_num1 + temp_num2) / 2;
            temp_num1 = Convert.ToDouble(dataGridView1.Rows[2].Cells[4].Value);
            temp_num2 = Convert.ToDouble(dataGridView1.Rows[2].Cells[4].Value);
            dataGridView1.Rows[5].Cells[4].Value = (temp_num1 + temp_num2) / 2;

            //Centerpoint F
            dataGridView1.Rows[6].Cells[0].Value = 6.ToString();
            dataGridView1.Rows[6].Cells[1].Value = "F";
            dataGridView1.Rows[6].Cells[2].Value = "Mid CD";

            temp_num1 = Convert.ToDouble(dataGridView1.Rows[3].Cells[3].Value);
            temp_num2 = Convert.ToDouble(dataGridView1.Rows[3].Cells[3].Value);
            dataGridView1.Rows[6].Cells[3].Value = (temp_num1 + temp_num2) / 2;
            temp_num1 = Convert.ToDouble(dataGridView1.Rows[4].Cells[4].Value);
            temp_num2 = Convert.ToDouble(dataGridView1.Rows[4].Cells[4].Value);
            dataGridView1.Rows[6].Cells[4].Value = (temp_num1 + temp_num2) / 2;


            //convert latlong to UTM
            double[] EastNorth = new double[2];
            double latitude_in_degree, longitude_in_degree, a, one_by_f, K0, M0, phi0_DD, lambda0_DD;
            CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();

            //Input parameters
            a = 6378137.0;
            one_by_f = 298.2572201;
            K0 = 0.9996;
            M0 = 0; //distance in meter of origin latitude from equator
            phi0_DD = 0;
            lambda0_DD = 84;

            for (int k = 0; k <= 6; k++)
            {
                latitude_in_degree = Convert.ToDouble(dataGridView1.Rows[k].Cells[3].Value);
                longitude_in_degree = Convert.ToDouble(dataGridView1.Rows[k].Cells[4].Value);

                EastNorth = CSAYGeoFun.Convert_LatLong_To_UTM(latitude_in_degree, longitude_in_degree, a, one_by_f, K0, M0, phi0_DD, lambda0_DD);

                dataGridView1.Rows[k].Cells[5].Value = EastNorth[0].ToString();
                dataGridView1.Rows[k].Cells[6].Value = EastNorth[1].ToString();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void baseLineEqParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double slope, intercept;
            double x1, y1, x2, y2;

            x1 = Convert.ToDouble(dataGridView1.Rows[5].Cells[5].Value);
            y1 = Convert.ToDouble(dataGridView1.Rows[5].Cells[6].Value);
            x2 = Convert.ToDouble(dataGridView1.Rows[6].Cells[5].Value);
            y2 = Convert.ToDouble(dataGridView1.Rows[6].Cells[6].Value);

            CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
            slope = CSAYGeoFun.Find_Slope_Of_Equation(x1, y1, x2, y2);
            intercept = CSAYGeoFun.Find_Intercept_Of_Equation(slope, x1, y1);

            TxtslopeBase.Text = slope.ToString();
            TxtInterceptBase.Text = intercept.ToString(); 
        }

        private void CalcverticalGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double e1, Ginterval, x, y, radius;
            double ARP_X, ARP_Y;
            int mx, my, NextRow;
            int[,] LR_factor = new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
            double[] latlong = new double[2];
            string[] Pt_Name = new string[] { "Quad_I_", "Quad_IV_", "Quad_II_", "Quad_III_" };

            ARP_X = Convert.ToDouble(dataGridView1.Rows[0].Cells[5].Value);
            ARP_Y = Convert.ToDouble(dataGridView1.Rows[0].Cells[6].Value);

            radius = Convert.ToDouble(TxtRadius.Text);
            Ginterval = Convert.ToDouble(TxtGridInterval.Text);


            //Calculate Origin Axes COORD
            NextRow = 7;
            string[] Pt_Name_O = new string[] { "Y", "Y1", "X1", "X" };
            int[] O_fac_X = new int[] { 0, 0, -1, 1 };
            int[] O_fac_Y = new int[] { 1, -1, 0, 0 };
            for (int k = 0; k <= 3; k++)
            {
                x = ARP_X + radius * O_fac_X[k];
                y = ARP_Y + radius * O_fac_Y[k];

                dataGridView1.Rows.Add();
                dataGridView1.Rows[NextRow].Cells[5].Value = x.ToString();
                dataGridView1.Rows[NextRow].Cells[6].Value = y.ToString();


                //convert latlong to UTM
                double[] EastNorth = new double[2];
                double a, one_by_f, K0, M0, phi0_DD, lambda0;
                double Fasle_Easting_X = 500000;

                //Input parameters
                a = 6378137.0;
                one_by_f = 298.2572201;
                K0 = 0.9996;
                M0 = 0; //distance in meter of origin latitude from equator
                phi0_DD = 0;
                lambda0 = 84;

                CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
                latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name_O[k].ToString() + k.ToString();
                dataGridView1.Rows[NextRow].Cells[2].Value = "Origin Axis";
                NextRow++;
            }

            //Gridlines other than origin
            NextRow = 11;
            First_V_Row = NextRow;
            double no_of_line = radius / Ginterval - 1;
            int nline = Convert.ToInt32(no_of_line);
            TxtNlines.Text = nline.ToString();
            //For vertical lines
            for (int k = 0; k < nline; k++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    mx = LR_factor[i, 0];
                    my = LR_factor[i, 1];
                    double G1 = Ginterval * (k + 1);
                    e1 = Math.Sqrt(radius * radius - G1 * G1);
                    x = ARP_X + G1 * mx;
                    y = ARP_Y + e1 * my;

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[NextRow].Cells[5].Value = x.ToString();
                    dataGridView1.Rows[NextRow].Cells[6].Value = y.ToString();


                    //convert latlong to UTM
                    double[] EastNorth = new double[2];
                    double a, one_by_f, K0, M0, phi0_DD, lambda0;
                    double Fasle_Easting_X = 500000;

                    //Input parameters
                    a = 6378137.0;
                    one_by_f = 298.2572201;
                    K0 = 0.9996;
                    M0 = 0; //distance in meter of origin latitude from equator
                    phi0_DD = 0;
                    lambda0 = 84;

                    CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
                    latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                    dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                    dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                    dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                    dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name[i].ToString() + i.ToString();
                    dataGridView1.Rows[NextRow].Cells[2].Value = "Vertical Axis";
                    NextRow++;

                }
            }

            Last_V_Row = NextRow - 1;
            First_Hz_Row = NextRow;
            Last_Hz_Row = First_Hz_Row + (Last_V_Row - First_V_Row);

            //For Horizontal lines
            int[,] LR_factor_H = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
            string[] Pt_Name_H = new string[] { "Quad_I_", "Quad_II_", "Quad_IV_", "Quad_III_" };
            for (int k = 0; k < nline; k++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    mx = LR_factor_H[i, 0];
                    my = LR_factor_H[i, 1];
                    double G1 = Ginterval * (k + 1);
                    e1 = Math.Sqrt(radius * radius - G1 * G1);
                    x = ARP_X + e1 * mx;
                    y = ARP_Y + G1 * my;

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[NextRow].Cells[5].Value = x.ToString();
                    dataGridView1.Rows[NextRow].Cells[6].Value = y.ToString();


                    //convert latlong to UTM
                    double[] EastNorth = new double[2];
                    double a, one_by_f, K0, M0, phi0_DD, lambda0;
                    double Fasle_Easting_X = 500000;

                    //Input parameters
                    a = 6378137.0;
                    one_by_f = 298.2572201;
                    K0 = 0.9996;
                    M0 = 0; //distance in meter of origin latitude from equator
                    phi0_DD = 0;
                    lambda0 = 84;

                    CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
                    latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                    dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                    dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                    dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                    dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name_H[i].ToString() + i.ToString();
                    dataGridView1.Rows[NextRow].Cells[2].Value = "Horizontal Axis";
                    NextRow++;
                }
            }
        }

        private void ComboBoxAirportCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtAirportCode.Text = ComboBoxAirportCode.Text;
        }

        private void gridCOORDParallelToBaselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
            double m, c;

            m = Convert.ToDouble(TxtslopeBase.Text);
            c = Convert.ToDouble(TxtInterceptBase.Text);

            double[] Intercept_Parallel = new double[400];
            double x_plus, y_plus, x_minus, y_minus;
            double e1, Ginterval, x, y, radius, G1;
            double ARP_X, ARP_Y;
            int mx, my, NextRow;
            
            double[] latlong = new double[2];
            
            ARP_X = Convert.ToDouble(dataGridView1.Rows[0].Cells[5].Value);
            ARP_Y = Convert.ToDouble(dataGridView1.Rows[0].Cells[6].Value);

            radius = Convert.ToDouble(TxtRadius.Text);
            Ginterval = Convert.ToDouble(TxtGridInterval.Text);

            //Equation of origin
            //Parallel to EF equation i.e. XX'
            double perp_dist = Math.Abs(m * ARP_X - ARP_Y + c) / Math.Sqrt(m * m + 1.0 * 1.0);
            G1 = perp_dist;
            double m1 = m;
            double c1  = CSAYGeoFun.Intercept_of_Parallel_line(m, c, G1, 1);
            //MessageBox.Show("m1, c1 = " + m1.ToString() + "," + c1.ToString());
      

            //Eq of perpendicular line to EF i.e. vertical origin axis YY'
            double m2 = -1.0 / m;
            double c2 = ARP_Y - ARP_X * m2 ;
            //MessageBox.Show("m2, c2 = " + m2.ToString() + "," + c2.ToString());

            double[] slopes = new double[] { m2, m2, m1, m1 };
            double[] intercepts = new double[] { c2, c2, c1, c1 };
            //Calculate Origin Axes COORD
            NextRow = 7;
            string[] Pt_Name_O = new string[] { "Y", "Y1", "X1", "X" };
            int[] O_fac_X = new int[] { 1, -1, -1, 1 };
            for (int k = 0; k <= 3; k++)
            {
                x = CSAYGeoFun.Find_Quadratic_X(slopes[k], intercepts[k], ARP_X, ARP_Y, radius, O_fac_X[k]);
                y = slopes[k] * x + intercepts[k];

                dataGridView1.Rows.Add();
                dataGridView1.Rows[NextRow].Cells[5].Value = x.ToString();
                dataGridView1.Rows[NextRow].Cells[6].Value = y.ToString();

                //convert latlong to UTM
                double[] EastNorth = new double[2];
                double a, one_by_f, K0, M0, phi0_DD, lambda0;
                double Fasle_Easting_X = 500000;

                //Input parameters
                a = 6378137.0;
                one_by_f = 298.2572201;
                K0 = 0.9996;
                M0 = 0; //distance in meter of origin latitude from equator
                phi0_DD = 0;
                lambda0 = 84;

                latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name_O[k].ToString();
                dataGridView1.Rows[NextRow].Cells[2].Value = "Incl Origin Axis";
                NextRow++;
            }

            //Gridlines other than origin
            int[] LR_factor = new int[] { 1, -1};
            string[] Pt_Name = new string[] { "Quad_I_", "Quad_IV_", "Quad_II_", "Quad_III_" };
            NextRow = 11;
            First_V_Row = NextRow;
            double no_of_line = radius / Ginterval - 1;
            int nline = Convert.ToInt32(no_of_line);
            int pti;
            TxtNlines.Text = nline.ToString();
            //For vertical lines
            
            for (int k = 0; k < nline; k++)
            {
                G1 = Ginterval * (k + 1);
                double[] intrcpt = new double[2];
                intrcpt[0] = CSAYGeoFun.Intercept_of_Parallel_line(m2, c2, G1, 1);
                intrcpt[1] = CSAYGeoFun.Intercept_of_Parallel_line(m2, c2, G1, -1);
                pti = 0;
                for (int j = 0; j <=1; j++)
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        //mx = LR_factor[i, 0];
                        //my = LR_factor[i, 1];

                        //G1 = Ginterval * (k + 1);

                        x = CSAYGeoFun.Find_Quadratic_X(m2, intrcpt[j], ARP_X, ARP_Y, radius, LR_factor[i]);
                        y = m2 * x + intrcpt[j];

                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[NextRow].Cells[5].Value = x.ToString();
                        dataGridView1.Rows[NextRow].Cells[6].Value = y.ToString();


                        //convert latlong to UTM
                        double[] EastNorth = new double[2];
                        double a, one_by_f, K0, M0, phi0_DD, lambda0;
                        double Fasle_Easting_X = 500000;

                        //Input parameters
                        a = 6378137.0;
                        one_by_f = 298.2572201;
                        K0 = 0.9996;
                        M0 = 0; //distance in meter of origin latitude from equator
                        phi0_DD = 0;
                        lambda0 = 84;

                        latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                        dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                        dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                        dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                        dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name[pti].ToString() + pti.ToString();
                        dataGridView1.Rows[NextRow].Cells[2].Value = "Incl Vertical Axis";
                        NextRow++;
                        pti++;
                    }
                }
                
            }

            Last_V_Row = NextRow - 1;
            First_Hz_Row = NextRow;
            Last_Hz_Row = First_Hz_Row + (Last_V_Row - First_V_Row);

            //For Horizontal lines
            int[] LR_factor_H = new int[] { 1, -1 };
            string[] Pt_Name_H = new string[] { "Quad_I_", "Quad_II_", "Quad_IV_", "Quad_III_" };
            for (int k = 0; k < nline; k++)
            {
                G1 = Ginterval * (k + 1);
                double[] intrcpt = new double[2];
                intrcpt[0] = CSAYGeoFun.Intercept_of_Parallel_line(m1, c1, G1, 1);
                intrcpt[1] = CSAYGeoFun.Intercept_of_Parallel_line(m1, c1, G1, -1);
                pti = 0;
                for (int j = 0; j<=1; j++)
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        x = CSAYGeoFun.Find_Quadratic_X(m1, intrcpt[j], ARP_X, ARP_Y, radius, LR_factor[i]);
                        y = m1 * x + intrcpt[j];

                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[NextRow].Cells[5].Value = x.ToString();
                        dataGridView1.Rows[NextRow].Cells[6].Value = y.ToString();


                        //convert latlong to UTM
                        double[] EastNorth = new double[2];
                        double a, one_by_f, K0, M0, phi0_DD, lambda0;
                        double Fasle_Easting_X = 500000;

                        //Input parameters
                        a = 6378137.0;
                        one_by_f = 298.2572201;
                        K0 = 0.9996;
                        M0 = 0; //distance in meter of origin latitude from equator
                        phi0_DD = 0;
                        lambda0 = 84;

                        latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                        dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                        dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                        dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                        dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name_H[pti].ToString() + pti.ToString();
                        dataGridView1.Rows[NextRow].Cells[2].Value = "Incl Horizontal Axis";
                        NextRow++;
                    }
                }
                
            }

        }

        private void gridlinesParallelToBaselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Drawing lines
            //show google map
            double lat1, lat2, long1, long2, latav, longav;

            lat1 = Convert.ToDouble(dataGridView1.Rows[5].Cells[3].Value);
            long1 = Convert.ToDouble(dataGridView1.Rows[5].Cells[4].Value);

            lat2 = Convert.ToDouble(dataGridView1.Rows[6].Cells[3].Value);
            long2 = Convert.ToDouble(dataGridView1.Rows[6].Cells[4].Value);

            latav = (lat1 + lat2) / 2;
            longav = (long1 + long2) / 2;

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(latav, longav);
            gMapControl1.Zoom = 15;

            //Making red cross invisible
            gMapControl1.ShowCenter = false;

            Color Linecolor;
            int nline, LineStroke;
            nline = Convert.ToInt32(TxtNlines.Text);
            
            //Vertical lines
            Linecolor = Color.Red;
            LineStroke = 1;
            for (int i = First_V_Row; i < Last_V_Row; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                DrawLinesWithTwoPoints(lat1, long1, lat2, long2, Linecolor, LineStroke);
            }

            //Horizontal lines
            Linecolor = Color.Blue;
            LineStroke = 1;
            nline = Convert.ToInt32(TxtNlines.Text);
            for (int i = First_Hz_Row; i < Last_Hz_Row; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                DrawLinesWithTwoPoints(lat1, long1, lat2, long2, Linecolor, LineStroke);
            }

            //Draw origin line
            Linecolor = Color.Yellow;
            LineStroke = 2;
            nline = Convert.ToInt32(TxtNlines.Text);
            for (int i = 7; i <= 10; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                DrawLinesWithTwoPoints(lat1, long1, lat2, long2, Linecolor, LineStroke);
            }

            gMapControl1.Zoom += 0.1;
        }

        private void kMLOfGridlinesParallelToNorthToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double lat1, long1, lat2, long2, lat_ARP, Long_ARP;
            string MyFolder  = CreateBaseAccessProjectFolders();
            /*var folder_GL = new SharpKml.Dom.Folder
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Folder contains Gridlines Hz and Vertical adn origin axes"
                },
                Name = "Folder_Gridlines_" + TxtAirportCode.Text
            };*/

            var document_ARP = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Vertical Gridlines Perpendicular to Base line"
                },
                Name = "ARP_" + TxtAirportCode.Text
            };

            
            //take lat long input from text boxes
            lat_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value);
            Long_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);

            // This will be used for the placemark-----------------
            var point_ARP = new SharpKml.Dom.Point
            {
                Coordinate = new SharpKml.Base.Vector(lat_ARP, Long_ARP)
            };

            var placemark_ARP = new SharpKml.Dom.Placemark
            {
                Name = "ARP_" + TxtAirportCode.Text,
                Geometry = point_ARP
            };
            document_ARP.AddFeature(placemark_ARP);
            //folder_GL.AddFeature(document_ARP);

            // This is the root element of the file--------------------------
            var kml = new Kml
            {
                Feature = document_ARP
            };
            var serializer = new Serializer();
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilename = MyFolder + "ARP.kml";
            FileStream fileStream = new FileStream(kmlfilename, FileMode.Append);
            serializer.Serialize(kml, fileStream);



            //Vertical lines
            var document_Ver = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Vertical Gridlines Perpendicular to Base line"
                },
                Name = "Vertical Gridlines"
            };

            ///Style 1
            SharpKml.Dom.LineStyle lineStyle = new SharpKml.Dom.LineStyle();
            lineStyle.Color = Color32.Parse("#FF0000");
            lineStyle.Width = 12;

            SharpKml.Dom.Style SimpleStyle = new SharpKml.Dom.Style();
            SimpleStyle.Id = "Style1";
            SimpleStyle.Line = lineStyle;
            document_Ver.AddStyle(SimpleStyle);
            for (int i = First_V_Row; i < Last_V_Row; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                coordinates.Add(new SharpKml.Base.Vector(lat1, long1));
                coordinates.Add(new SharpKml.Base.Vector(lat2, long2));

                linestring.Coordinates = coordinates;
                SharpKml.Dom.Placemark placemark_line = new SharpKml.Dom.Placemark();
                placemark_line.Name = "GridLines_Vertical_" + i.ToString();
                placemark_line.Geometry = linestring;

                document_Ver.AddFeature(placemark_line);
            }

            //folder_GL.AddFeature(document_Ver);
            var kmlV = new Kml
            {
                Feature = document_Ver
            };
            string kmlfilenameV = MyFolder + "VerticalGridlines.kml";
            FileStream fileStreamV = new FileStream(kmlfilenameV, FileMode.Append);
            serializer.Serialize(kmlV, fileStreamV);





            //Horizontal lines-------------------------------------------------------------------------
            var document_Hz = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Horizontal Gridlines Parallel to Base line"
                },
                Name = "Horizontal Gridlines"
            };
            for (int i = First_Hz_Row; i < Last_Hz_Row; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                coordinates.Add(new SharpKml.Base.Vector(lat1, long1));
                coordinates.Add(new SharpKml.Base.Vector(lat2, long2));

                linestring.Coordinates = coordinates;
                SharpKml.Dom.Placemark placemark_line1 = new SharpKml.Dom.Placemark();
                placemark_line1.Name = "GridLines_Horizontal_" + i.ToString();
                placemark_line1.Geometry = linestring;

                document_Hz.AddFeature(placemark_line1);
            }
            //folder_GL.AddFeature(document_Hz);

            var kmlH = new Kml
            {
                Feature = document_Hz
            };
            string kmlfilenameH = MyFolder + "Hz_Gridlines.kml";
            FileStream fileStreamH = new FileStream(kmlfilenameH, FileMode.Append);
            serializer.Serialize(kmlH, fileStreamH);



            //Draw origin line---------------------------------------------------------------------
            var document_Or = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Origin axes Gridlines"
                },
                Name = "Origin Gridlines"
            };
            for (int i = 7; i <= 10; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                coordinates.Add(new SharpKml.Base.Vector(lat1, long1));
                coordinates.Add(new SharpKml.Base.Vector(lat2, long2));

                linestring.Coordinates = coordinates;
                SharpKml.Dom.Placemark placemark_line2 = new SharpKml.Dom.Placemark();
                placemark_line2.Name = "GridLines_Origin_" + i.ToString();
                placemark_line2.Geometry = linestring;

                document_Or.AddFeature(placemark_line2);
            }
            //folder_GL.AddFeature(document_Or);
            var kmlOr = new Kml
            {
                Feature = document_Or
            };
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameOr = MyFolder + "Origin Axes.kml";
            FileStream fileStreamOr = new FileStream(kmlfilenameOr, FileMode.Append);
            serializer.Serialize(kmlOr, fileStreamOr);


            MessageBox.Show("Successfully exported to KML");
        }

        private string CreateBaseAccessProjectFolders()
        {
            string A_Code;

            if (TxtAirportCode.Text == "")
            {
                A_Code = "New_Airport";
            }
            else
            {
                A_Code = TxtAirportCode.Text;
            }
            
           string Project_Folders = Environment.CurrentDirectory + "\\Grid_Project_Folders\\BaseGridLine\\" + A_Code + "\\";

            if (!Directory.Exists(Project_Folders))
            {
                Directory.CreateDirectory(Project_Folders);
            }

            return Project_Folders;
        }

        private void DrawverticalGridLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Drawing lines
            //show google map
            double lat1, lat2, long1, long2, latav, longav;

            lat1 = Convert.ToDouble(dataGridView1.Rows[5].Cells[3].Value);
            long1 = Convert.ToDouble(dataGridView1.Rows[5].Cells[4].Value);

            lat2 = Convert.ToDouble(dataGridView1.Rows[6].Cells[3].Value);
            long2 = Convert.ToDouble(dataGridView1.Rows[6].Cells[4].Value);

            latav = (lat1 + lat2) / 2;
            longav = (long1 + long2) / 2;


            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(latav, longav);
            gMapControl1.Zoom = 15;

            //Making red cross invisible
            gMapControl1.ShowCenter = false;

            Color Linecolor;
            int nline, LineStroke;
            Linecolor = Color.Red;
            LineStroke = 1;
            nline = Convert.ToInt32(TxtNlines.Text);
            for (int i = First_V_Row; i < Last_V_Row; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                DrawLinesWithTwoPoints(lat1, long1, lat2, long2, Linecolor, LineStroke);
            }

            Linecolor = Color.Blue;
            LineStroke = 1;
            nline = Convert.ToInt32(TxtNlines.Text);
            for (int i = First_Hz_Row; i < Last_Hz_Row; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                DrawLinesWithTwoPoints(lat1, long1, lat2, long2, Linecolor, LineStroke);
            }

            //Draw origin vertical line
            Linecolor = Color.Yellow;
            LineStroke = 2;
            nline = Convert.ToInt32(TxtNlines.Text);
            for (int i = 7; i <= 10; i += 2)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                lat2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[3].Value);
                long2 = Convert.ToDouble(dataGridView1.Rows[i + 1].Cells[4].Value);

                DrawLinesWithTwoPoints(lat1, long1, lat2, long2, Linecolor, LineStroke);
            }

            gMapControl1.Zoom += 0.1;
        }

        public void DrawLinesWithTwoPoints(double lat1, double long1, double lat2, double long2, Color Linecolor, int LineStroke)
        {
            GMapOverlay routes = new GMapOverlay("routes");

            List<PointLatLng> points_route = new List<PointLatLng>();
            points_route.Add(new PointLatLng(lat1, long1));
            points_route.Add(new PointLatLng(lat2, long2));
            GMap.NET.WindowsForms.GMapRoute route = new GMap.NET.WindowsForms.GMapRoute(points_route, "RWY to House");
            //route.Stroke = new Pen(Color.Red, 3);
            route.Stroke = new Pen(Linecolor, LineStroke);
            routes.Routes.Add(route);
            gMapControl1.Overlays.Add(routes);
        }
    }
}
