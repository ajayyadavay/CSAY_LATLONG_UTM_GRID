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
using GMap.NET.MapProviders;
using System.Diagnostics;
using GMap.NET.Internals;
using static GMap.NET.Entity.OpenStreetMapGraphHopperRouteEntity;

namespace CSAY_LATLONG_UTM_GRID
{
    public partial class FrmMainGrid : Form
    {
        int First_V_Row, First_Hz_Row;
        int Last_V_Row, Last_Hz_Row;
        bool IsFromSetting = false;
        public FrmMainGrid()
        {
            InitializeComponent();
        }

        public void GenerateTreeViewInitial()
        {
            //Level 1
            treeView1.Nodes.Add("GridBoundaryName", "Grid Boundary");    //0
            //treeView1.Nodes.Add("Grid");    //1
            treeView1.Nodes.Add("GridName", "Grid");

            //level 1
            treeView1.Nodes[0].Nodes.Add("CircleName","Circle");//0,0
            treeView1.Nodes[0].Nodes.Add("CircularPolygonName", "Circle Polygon");//0,1

            treeView1.Nodes[1].Nodes.Add("GridlinesName", "Gridlines");//1,0

            treeView1.ExpandAll();
        }

        private void FrmMainGrid_Load(object sender, EventArgs e)
        {
            GenerateTreeViewInitial();

            dataGridView1.Rows.Clear();
            /*for(int i =0; i <= 6; i++)
            {
                dataGridView1.Rows.Add();
            }*/

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

            dataGridView1.Rows.Clear();
            for(int i1 =0; i1 <= 6; i1++)
            {
                dataGridView1.Rows.Add();
            }

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
            temp_num2 = Convert.ToDouble(dataGridView1.Rows[2].Cells[3].Value);
            dataGridView1.Rows[5].Cells[3].Value = (temp_num1 + temp_num2) / 2;
            temp_num1 = Convert.ToDouble(dataGridView1.Rows[1].Cells[4].Value);
            temp_num2 = Convert.ToDouble(dataGridView1.Rows[2].Cells[4].Value);
            dataGridView1.Rows[5].Cells[4].Value = (temp_num1 + temp_num2) / 2;

            //Centerpoint F
            dataGridView1.Rows[6].Cells[0].Value = 6.ToString();
            dataGridView1.Rows[6].Cells[1].Value = "F";
            dataGridView1.Rows[6].Cells[2].Value = "Mid CD";

            temp_num1 = Convert.ToDouble(dataGridView1.Rows[3].Cells[3].Value);
            temp_num2 = Convert.ToDouble(dataGridView1.Rows[4].Cells[3].Value);
            dataGridView1.Rows[6].Cells[3].Value = (temp_num1 + temp_num2) / 2;
            temp_num1 = Convert.ToDouble(dataGridView1.Rows[3].Cells[4].Value);
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
            //lambda0_DD = 84;
            lambda0_DD = Convert.ToDouble(TxtCM.Text);

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
            treeView1.Enabled = true;
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


            int totalrows_after_7 = (dataGridView1.RowCount - 1) - 7;
            if (totalrows_after_7 > 7)
            {
                dataGridView1.Rows.Clear();
                LoadRWYdataStripMenuItem1_Click(sender, e);
            }

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
                //lambda0 = 84;
                lambda0 = Convert.ToDouble(TxtCM.Text);

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
            double no_of_line = 0.0;
            if (circularToolStripMenuItem.Checked == true)
            {
                no_of_line = radius / Ginterval - 1;
            }
            else if (rectangularToolStripMenuItem.Checked == true)
            {
                no_of_line = radius / Ginterval - 1 + 1;
            }
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
                    //e1 = Math.Sqrt(radius * radius - G1 * G1);
                    e1 = 0;
                    if (circularToolStripMenuItem.Checked == true)
                    {
                        e1 = Math.Sqrt(radius * radius - G1 * G1);
                    }
                    else if (rectangularToolStripMenuItem.Checked == true)
                    {
                        e1 = radius;
                    }

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
                    lambda0 = Convert.ToDouble(TxtCM.Text); //84

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
                    e1 = 0;
                    if (circularToolStripMenuItem.Checked == true)
                    {
                        e1 = Math.Sqrt(radius * radius - G1 * G1);
                    }
                    else if (rectangularToolStripMenuItem.Checked == true)
                    {
                        e1 = radius;
                    }

                    //e1 = Math.Sqrt(radius * radius - G1 * G1);
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
                    lambda0 = Convert.ToDouble(TxtCM.Text);   //84

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

            if (treeView1.Nodes["GridName"].Nodes[0].Checked == true)
            {
                treeView1.Nodes["GridName"].Nodes[0].Checked = false;
                treeView1.Nodes["GridName"].Nodes[0].Checked = true;
            }
        }

        private void ComboBoxAirportCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtAirportCode.Text = ComboBoxAirportCode.Text;
        }

        private void gridCOORDParallelToBaselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Enabled = true;
            CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
            double m, c;
            double[] XY = new double[2];


            double Center_X, Center_Y;
            double Slope_Parallel_EF, Intercept_Parallel_EF, Slope_Perp_EF, Intercept_Perp_EF; 

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
            Slope_Parallel_EF = m1;
            Intercept_Parallel_EF = c1;
            //MessageBox.Show("m1, c1 = " + m1.ToString() + "," + c1.ToString());
      

            //Eq of perpendicular line to EF i.e. vertical origin axis YY'
            double m2 = -1.0 / m;
            double c2 = ARP_Y - ARP_X * m2 ;

            Slope_Perp_EF = m2;
            Intercept_Perp_EF= c2;
            //MessageBox.Show("m2, c2 = " + m2.ToString() + "," + c2.ToString());

            double[] slopes = new double[] { m2, m2, m1, m1 };
            double[] intercepts = new double[] { c2, c2, c1, c1 };
            //Calculate Origin Axes COORD
            NextRow = 7;
            string[] Pt_Name_O = new string[] { "Y", "Y1", "X1", "X" };
            int[] O_fac_X = new int[] { 1, -1, -1, 1 };

            int totalrows_after_7 = (dataGridView1.RowCount - 1) - 7;
            if ( totalrows_after_7 > 7)
            {
                dataGridView1.Rows.Clear();
                LoadRWYdataStripMenuItem1_Click(sender, e);
                /*for(int k = 0; k < totalrows_after_7; k++)
                {
                    int lastrow_index = (dataGridView1.RowCount - 1) - 7 - 1;
                    dataGridView1.Rows.RemoveAt(lastrow_index);
                }*/
            }
            //MessageBox.Show("rows no. = " + (dataGridView1.RowCount - 1).ToString());
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
                lambda0 = Convert.ToDouble(TxtCM.Text);//84

                latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name_O[k].ToString();
                if(NextRow == 7 || NextRow == 8)
                {
                    dataGridView1.Rows[NextRow].Cells[2].Value = "Inclined Perpendicular Axis";
                }
                else
                {
                    dataGridView1.Rows[NextRow].Cells[2].Value = "Inclined Parallel Axis";
                }
                
                NextRow++;
            }

            //Gridlines other than origin
            int[] LR_factor = new int[] { 1, -1};//find xplus and xminus
            string[] Pt_Name = new string[] { "Quad_I_", "Quad_IV_", "Quad_II_", "Quad_III_" };
            NextRow = 11;
            First_V_Row = NextRow;
            double no_of_line = 0.0;
            if (circularToolStripMenuItem.Checked == true)
            {
                no_of_line = radius / Ginterval - 1;
            }
            else if (rectangularToolStripMenuItem.Checked == true)
            {
                no_of_line = radius / Ginterval - 1 + 1;
            }

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
                        Center_X = 0.0;
                        Center_Y = 0.0;
                        if (circularToolStripMenuItem.Checked == true)
                        {
                            Center_X = ARP_X;
                            Center_Y = ARP_Y;
                        }
                        else if (rectangularToolStripMenuItem.Checked == true)
                        {
                            XY = CSAYGeoFun.Find_Intersection_XY(Slope_Parallel_EF, Intercept_Parallel_EF, Slope_Perp_EF, intrcpt[j]);
                            Center_X = XY[0];
                            Center_Y = XY[1];
                        }
                        

                        x = CSAYGeoFun.Find_Quadratic_X(m2, intrcpt[j], Center_X, Center_Y, radius, LR_factor[i]);
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
                        lambda0 = Convert.ToDouble(TxtCM.Text);

                        latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                        dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                        dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                        dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                        dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name[pti].ToString() + pti.ToString();
                        dataGridView1.Rows[NextRow].Cells[2].Value = "Inclined Perpendicular (RED) Axis"; //Vertical axis w.r.t. VNBW
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
                Center_X = 0.0;
                Center_Y = 0.0;
                for (int j = 0; j<=1; j++)
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        if (circularToolStripMenuItem.Checked == true)
                        {
                            Center_X = ARP_X;
                            Center_Y = ARP_Y;
                        }
                        else if (rectangularToolStripMenuItem.Checked == true)
                        {
                            XY = CSAYGeoFun.Find_Intersection_XY(Slope_Perp_EF, Intercept_Perp_EF, Slope_Parallel_EF, intrcpt[j]);
                            Center_X = XY[0];
                            Center_Y = XY[1];
                        }

                        x = CSAYGeoFun.Find_Quadratic_X(m1, intrcpt[j], Center_X, Center_Y, radius, LR_factor_H[i]);
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
                        lambda0 = Convert.ToDouble(TxtCM.Text);

                        latlong = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(x, y, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                        dataGridView1.Rows[NextRow].Cells[3].Value = latlong[0].ToString();
                        dataGridView1.Rows[NextRow].Cells[4].Value = latlong[1].ToString();

                        dataGridView1.Rows[NextRow].Cells[0].Value = NextRow.ToString();
                        dataGridView1.Rows[NextRow].Cells[1].Value = Pt_Name_H[pti].ToString() + pti.ToString();
                        dataGridView1.Rows[NextRow].Cells[2].Value = "Inclined Parallel (Blue) Axis";//Horizontal parallel w.r.t. VNBW
                        NextRow++;
                    }
                }
                
            }

            if (treeView1.Nodes["GridName"].Nodes[0].Checked == true)
            {
                treeView1.Nodes["GridName"].Nodes[0].Checked = false;
                treeView1.Nodes["GridName"].Nodes[0].Checked = true;
            }
            



        }


        private void DrawGridlines()
        {
            //Drawing lines
            //show google map
            double lat1, lat2, long1, long2;

            lat1 = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value);
            long1 = Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(lat1, long1);
            gMapControl1.Zoom = 13;

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

        private void gridlinesParallelToBaselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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
            FileStream fileStream = new FileStream(kmlfilename, FileMode.OpenOrCreate);
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
            lineStyle.Color = Color32.Parse("ff0000ff");
            lineStyle.Width = 1;

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
                placemark_line.StyleUrl = new Uri("#Style1", UriKind.Relative);
                document_Ver.AddFeature(placemark_line);
            }

            //folder_GL.AddFeature(document_Ver);
            var kmlV = new Kml
            {
                Feature = document_Ver
            };
            string kmlfilenameV = MyFolder + "VerticalGridlines.kml";
            FileStream fileStreamV = new FileStream(kmlfilenameV, FileMode.OpenOrCreate);
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

            //StyleH
            SharpKml.Dom.LineStyle lineStyleH = new SharpKml.Dom.LineStyle();
            lineStyleH.Color = Color32.Parse("ffff0000");//First two transparency; then Blue, green ,red
            lineStyleH.Width = 1;

            SharpKml.Dom.Style SimpleStyleH = new SharpKml.Dom.Style();
            SimpleStyleH.Id = "StyleH";
            SimpleStyleH.Line = lineStyleH;
            document_Hz.AddStyle(SimpleStyleH);

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
                placemark_line1.StyleUrl = new Uri("#StyleH", UriKind.Relative);

                document_Hz.AddFeature(placemark_line1);
            }
            //folder_GL.AddFeature(document_Hz);

            var kmlH = new Kml
            {
                Feature = document_Hz
            };
            string kmlfilenameH = MyFolder + "Hz_Gridlines.kml";
            FileStream fileStreamH = new FileStream(kmlfilenameH, FileMode.OpenOrCreate);
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

            ///StyleH
            SharpKml.Dom.LineStyle lineStyleO = new SharpKml.Dom.LineStyle();
            lineStyleO.Color = Color32.Parse("ff00ffff");//First two transparency; then Blue, green ,red
            lineStyleO.Width = 2;

            SharpKml.Dom.Style SimpleStyleO = new SharpKml.Dom.Style();
            SimpleStyleO.Id = "StyleO";
            SimpleStyleO.Line = lineStyleO;
            document_Or.AddStyle(SimpleStyleO);

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
                placemark_line2.StyleUrl = new Uri("#StyleO", UriKind.Relative);

                document_Or.AddFeature(placemark_line2);
            }
            //folder_GL.AddFeature(document_Or);
            var kmlOr = new Kml
            {
                Feature = document_Or
            };
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameOr = MyFolder + "Origin Axes.kml";
            FileStream fileStreamOr = new FileStream(kmlfilenameOr, FileMode.OpenOrCreate);
            serializer.Serialize(kmlOr, fileStreamOr);


            MessageBox.Show("Successfully exported to KML");
        }

        public void Clear_All_Surfaces()
        {
            int n_count;
            //clear map
            n_count = gMapControl1.Overlays.Count;
            if (n_count > 0)
            {
                for (int i = 1; i <= n_count; i++)
                {
                    gMapControl1.Overlays.RemoveAt(0);
                }
            }

            gMapControl1.Update();
            gMapControl1.Refresh();
        }

        public void Draw_Full_CircleCircumference(double lat0, double long0, double r, int segments, Color Circle_Color, int Circle_Stroke)
        {
            try
            {

                double[] latlong1 = new double[2];
                double aa, b, a1, b1, a_E, b_E;

                double seg, theta;

                CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();

                double a, one_by_f, K0, M0, lambda0, phi0_DD;
                
                phi0_DD = 0;
                //Input parameters
                a = 6378137.0;
                one_by_f = 298.2572201;
                K0 = 0.9996;
                M0 = 0; //distance in meter of origin latitude from equator
                lambda0 = Convert.ToDouble(TxtCM.Text);
                double[] ARP_XY = new double[2];
                a_E = lat0;
                b_E = long0;

                ARP_XY = CSAYGeoFun.Convert_LatLong_To_UTM(a_E, b_E, a, one_by_f, K0, M0, phi0_DD, lambda0);
                aa = ARP_XY[0];
                b = ARP_XY[1];


                //convert latlong to UTM
                double Fasle_Easting_X = 500000;
                double[,] latlong2 = new double[segments, 2];
                seg = (Math.PI * 2) / segments;//Math.PI * 2 / segments;
                //plot_position = "Below";
                for (int i = 0; i < segments; i++)
                {
                    theta = seg * i;
                    a1 = aa + Math.Cos(theta) * r;
                    b1 = b + Math.Sin(theta) * r;

                    latlong1 =CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(a1, b1, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                    //points.Add(new PointLatLng(latlong1[0], latlong1[1]));
                    latlong2[i, 0] = latlong1[0];
                    latlong2[i, 1] = latlong1[1];

                    if (i >= 1 && i < segments)
                    DrawLinesWithTwoPoints(latlong2[i-1,0], latlong2[i-1,1], latlong2[i, 0], latlong2[i, 1], Circle_Color, Circle_Stroke);
                }

            }
            catch
            {

            }
        }

        private string CreateBaseAccessProjectFolders()
        {
            string A_Code, mode;

            if(northToolStripMenuItem.Checked == true)
            {
                mode = "NorthGridLine";
            }
            else
            {
                mode = "BaseGridLine";
            }

            if (TxtAirportCode.Text == "")
            {
                A_Code = "New_Airport";
            }
            else
            {
                A_Code = TxtAirportCode.Text;
            }
            string GB;
            if (circularToolStripMenuItem.Checked == true)
            {
                GB = "Circular";
            }
            else
            {
                GB = "Rectangular";
            }

            string Project_Folders = Environment.CurrentDirectory + "\\Grid_Project_Folders\\" + mode +"\\" + GB + "\\" + A_Code + "\\";

            if (!Directory.Exists(Project_Folders))
            {
                Directory.CreateDirectory(Project_Folders);
            }

            return Project_Folders;
        }

        private void clearAllLayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_All_Surfaces();
        }

        public void Draw_Full_Circle(double r, int segments, Color Circle_Color)
        {
            try
            {
                //show google map
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                gMapControl1.DragButton = MouseButtons.Left;
                gMapControl1.MouseWheelZoomEnabled = true;
                gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                //gMapControl1.Zoom = 13;

                //Making red cross invisible
                gMapControl1.ShowCenter = false;

                //Draw Polygon
                GMapOverlay polygons = new GMapOverlay("polygons");
                List<PointLatLng> points = new List<PointLatLng>();


                double[] latlong1 = new double[2];


                double aa, b, a1, b1, a_E, b_E, a_F, b_F;
                //int segments;

                double seg, theta;
                //segments = 6000;

                //Input of center E
                /*a_E = Convert.ToDouble(dataGridView1.Rows[4].Cells["ColEasting"].Value);//E
                b_E = Convert.ToDouble(dataGridView1.Rows[4].Cells["ColNorthing"].Value);//E

                a_F = Convert.ToDouble(dataGridView1.Rows[5].Cells["ColEasting"].Value);//E
                b_F = Convert.ToDouble(dataGridView1.Rows[5].Cells["ColNorthing"].Value);//E

                a = (a_E + a_F) / 2;
                b = (b_E + b_F) / 2;*/

                double lat_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value);
                double long_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);

                double[] ARP_XY = new double[2];
                a_E = lat_ARP;
                b_E = long_ARP;
                //ARP_XY = Convert_LatLong_To_UTM(a_E, b_E);
                //a = ARP_XY[0];
                //b = ARP_XY[1];


                CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();

                double a, one_by_f, K0, M0, lambda0, phi0_DD;

                phi0_DD = 0;
                //Input parameters
                a = 6378137.0;
                one_by_f = 298.2572201;
                K0 = 0.9996;
                M0 = 0; //distance in meter of origin latitude from equator
                lambda0 = Convert.ToDouble(TxtCM.Text);
                //double[] ARP_XY = new double[2];
                //a_E = lat0;
                //b_E = long0;
                double Fasle_Easting_X = 500000;

                ARP_XY = CSAYGeoFun.Convert_LatLong_To_UTM(a_E, b_E, a, one_by_f, K0, M0, phi0_DD, lambda0);
                aa = ARP_XY[0];
                b = ARP_XY[1];


                seg = (Math.PI * 2) / segments;//Math.PI * 2 / segments;
                //plot_position = "Below";
                for (int i = 0; i < segments; i++)
                {
                    theta = seg * i;
                    a1 = aa + Math.Cos(theta) * r;
                    b1 = b + Math.Sin(theta) * r;

                    latlong1 = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(a1, b1, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                    points.Add(new PointLatLng(latlong1[0], latlong1[1]));

                }

                GMap.NET.WindowsForms.GMapPolygon polygon = new GMap.NET.WindowsForms.GMapPolygon(points, "CirclePoly");
                polygons.Polygons.Add(polygon);
                gMapControl1.Overlays.Add(polygons);
                polygon.Fill = new SolidBrush(Color.FromArgb(50, Circle_Color));
                polygon.Stroke = new Pen(Circle_Color, 0);

                gMapControl1.Invalidate();
                gMapControl1.Update();
                gMapControl1.Refresh();

            }
            catch
            {

            }
        }

        private void DrawCirclePolygonBoundary()
        {
            double lat_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value);
            double long_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(lat_ARP, long_ARP);
            gMapControl1.Zoom = 13;

            //Making red cross invisible
            gMapControl1.ShowCenter = false;

            Color mycolor = Color.WhiteSmoke;
            //int Circle_Stroke = 3;

            double radius = Convert.ToDouble(TxtRadius.Text);
            Draw_Full_Circle(radius, 6000, mycolor);

            gMapControl1.Zoom += 0.1;
        }


        private void DrawCircleBoundary()
        {
            double lat_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value);
            double long_ARP = Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(lat_ARP, long_ARP);
            gMapControl1.Zoom = 13;

            //Making red cross invisible
            gMapControl1.ShowCenter = false;

            Color mycolor = Color.DeepPink;
            int Circle_Stroke = 3;

            double radius = Convert.ToDouble(TxtRadius.Text);
            Draw_Full_CircleCircumference(lat_ARP, long_ARP, radius, 6000, mycolor, Circle_Stroke);

            gMapControl1.Zoom += 0.1;
        }

        private void DrawSelectiveGrids(int cr, int glines, int cpoly)
        {
            Clear_All_Surfaces();
            if(cr == 1)
            {
                DrawCircleBoundary();
            }
            if (cpoly == 1)
            {
                DrawCirclePolygonBoundary();
            }
            if (glines == 1)
            {
                DrawGridlines();
            }
        }



        private void baselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsFromSetting = true;

            lblModestatus.Text = "Mode Setting: " + baselineToolStripMenuItem.Text;

            int totalrows_after_7 = (dataGridView1.RowCount - 1) - 7;
            if (totalrows_after_7 > 7)
            {
                dataGridView1.Rows.Clear();
                LoadRWYdataStripMenuItem1_Click(sender, e);
            }
            
            //MessageBox.Show("after reload");
            treeView1.Nodes["GridBoundaryName"].Nodes[0].Checked = false;
            //MessageBox.Show("circle");
            treeView1.Nodes["GridBoundaryName"].Nodes[1].Checked = false;
            //MessageBox.Show("after rectangle");
            treeView1.Nodes["GridName"].Nodes[0].Checked = false;
            //MessageBox.Show("after grid");

            baseLineEqParameterToolStripMenuItem.Enabled = true;
            gridCOORDParallelToBaselineToolStripMenuItem.Enabled = true;

            CalcverticalGridToolStripMenuItem.Enabled = false;

            baselineModeToolStripMenuItem.Enabled = true;
            northModeToolStripMenuItem.Enabled = false;

            baselineToolStripMenuItem.Checked = true;
            northToolStripMenuItem.Checked = false;

            Clear_All_Surfaces();

            IsFromSetting = false;
        }

        private void northToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsFromSetting = true;

            lblModestatus.Text = "Mode Setting: " + northToolStripMenuItem.Text;

            int totalrows_after_7 = (dataGridView1.RowCount - 1) - 7;
            if (totalrows_after_7 > 7)
            {
                dataGridView1.Rows.Clear();
                LoadRWYdataStripMenuItem1_Click(sender, e);
            }

            treeView1.Nodes["GridBoundaryName"].Nodes[0].Checked = false;
            treeView1.Nodes["GridBoundaryName"].Nodes[1].Checked = false;
            treeView1.Nodes["GridName"].Nodes[0].Checked = false;

            baseLineEqParameterToolStripMenuItem.Enabled = false;
            gridCOORDParallelToBaselineToolStripMenuItem.Enabled = false;

            CalcverticalGridToolStripMenuItem.Enabled = true;

            baselineModeToolStripMenuItem.Enabled = false;
            northModeToolStripMenuItem.Enabled = true;

            baselineToolStripMenuItem.Checked = false;
            northToolStripMenuItem.Checked = true;

            Clear_All_Surfaces();

            IsFromSetting = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout fabout = new FrmAbout();
            fabout.Show();
        }

        private void baselineModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadRWYdataStripMenuItem1_Click(sender, e);

            baseLineEqParameterToolStripMenuItem_Click(sender, e);
            gridCOORDParallelToBaselineToolStripMenuItem_Click(sender, e);

            treeView1.Nodes["GridBoundaryName"].Nodes[0].Checked = true;
            treeView1.Nodes["GridBoundaryName"].Nodes[1].Checked = true;
            treeView1.Nodes["GridName"].Nodes[0].Checked = true;

            //gridlinesParallelToBaselineToolStripMenuItem_Click(sender, e);
            //circleCenteredAtARPToolStripMenuItem_Click(sender, e);

            kMLOfGridlinesParallelToNorthToolStripMenuItem1_Click(sender, e);
            kMLCircleCenteredAtARPToolStripMenuItem_Click(sender, e);
            kMLCirclePolygonCenteredAtARPToolStripMenuItem_Click(sender, e);
            kMLExtremePointsToolStripMenuItem_Click(sender, e);

        }

        private void northModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadRWYdataStripMenuItem1_Click(sender, e);

            CalcverticalGridToolStripMenuItem_Click(sender, e);

            treeView1.Nodes["GridBoundaryName"].Nodes[0].Checked = true;
            treeView1.Nodes["GridBoundaryName"].Nodes[1].Checked = true;
            treeView1.Nodes["GridName"].Nodes[0].Checked = true;

            //gridlinesParallelToBaselineToolStripMenuItem_Click(sender, e);
            //circleCenteredAtARPToolStripMenuItem_Click(sender, e);

            kMLOfGridlinesParallelToNorthToolStripMenuItem1_Click(sender, e);
            kMLCircleCenteredAtARPToolStripMenuItem_Click(sender, e);
            kMLCirclePolygonCenteredAtARPToolStripMenuItem_Click(sender, e);
            kMLExtremePointsToolStripMenuItem_Click(sender, e);
        }

        private void circularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rectangularToolStripMenuItem.Checked = false;
            circularToolStripMenuItem.Checked = true;

            lblGBstatus.Text = "Grid Boundary Setting: " + circularToolStripMenuItem.Text;
        }

        private void rectangularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rectangularToolStripMenuItem.Checked = true;
            circularToolStripMenuItem.Checked = false;

            lblGBstatus.Text = "Grid Boundary Setting: " + rectangularToolStripMenuItem.Text;

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            int cr, glines, cpoly;
            
            if (treeView1.Nodes["GridBoundaryName"].Nodes[0].Checked == true)
            {
                cr = 1;
            }
            else
            {
                cr = 0;
            }

            if (treeView1.Nodes["GridBoundaryName"].Nodes[1].Checked == true)
            {
                cpoly = 1;
            }
            else
            {
                cpoly = 0;
            }

            if (treeView1.Nodes["GridName"].Nodes[0].Checked == true)
            {
                glines = 1;
            }
            else
            {
                glines = 0;
            }

            if (IsFromSetting == false)
            {
                DrawSelectiveGrids(cr, glines, cpoly);
            }
        }

        private void kMLCirclePolygonCenteredAtARPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string MyFolder = CreateBaseAccessProjectFolders();
            var serializer = new Serializer();

            List<Polygon> lPolygon = new List<Polygon>();
            Polygon mPolygon = new Polygon();
            OuterBoundary outerBoundary = new OuterBoundary();
            outerBoundary.LinearRing = new LinearRing();
            outerBoundary.LinearRing.Coordinates = new CoordinateCollection();

            List<PointLatLng> points = new List<PointLatLng>();

            double[] latlong1 = new double[2];
            double aa, b, a1, b1, a_E, b_E;
            double seg, theta;
            int segments = 6000;

            CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
            double a, one_by_f, K0, M0, lambda0, phi0_DD;
            phi0_DD = 0;
            //Input parameters
            a = 6378137.0;
            one_by_f = 298.2572201;
            K0 = 0.9996;
            M0 = 0; //distance in meter of origin latitude from equator
            lambda0 = Convert.ToDouble(TxtCM.Text);

            double[] ARP_XY = new double[2];
            a_E = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value);
            b_E = Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);
            double r = Convert.ToDouble(TxtRadius.Text);
            ARP_XY = CSAYGeoFun.Convert_LatLong_To_UTM(a_E, b_E, a, one_by_f, K0, M0, phi0_DD, lambda0);
            aa = ARP_XY[0];
            b = ARP_XY[1];

            //Export to  KML
            //Draw Bounding circle Polygon---------------------------------------------------------------------
            var document_CP = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Boundary circle Polygon"
                },
                Name = "Boundary circle Polygon"
            };

            ///StyleCP
            SharpKml.Dom.LineStyle lineStyleCP = new SharpKml.Dom.LineStyle();
            lineStyleCP.Color = Color32.Parse("ff00ffff");//First two transparency; then Blue, green ,red
            lineStyleCP.Width = 2;

            SharpKml.Dom.PolygonStyle PolyStyleCP = new SharpKml.Dom.PolygonStyle();
            PolyStyleCP.Color = Color32.Parse("ff0f0f0f");//First two transparency; then Blue, green ,red
            

            SharpKml.Dom.Style SimpleStyleCP = new SharpKml.Dom.Style();
            SimpleStyleCP.Id = "StyleCP";
            SimpleStyleCP.Line = lineStyleCP;
            SimpleStyleCP.Polygon = PolyStyleCP;
            document_CP.AddStyle(SimpleStyleCP);

            //GMapOverlay polygons = new GMapOverlay("polygons");

            //LineString linestring = new LineString();
            //Polygon poly = new Polygon();
            //CoordinateCollection coordinates = new CoordinateCollection();

            //convert latlong to UTM
            double Fasle_Easting_X = 500000;
            seg = (Math.PI * 2) / segments;//Math.PI * 2 / segments;
            for (int i = 0; i < segments; i++)
            {
                theta = seg * i;
                a1 = aa + Math.Cos(theta) * r;
                b1 = b + Math.Sin(theta) * r;

                latlong1 = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(a1, b1, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                points.Add(new PointLatLng(latlong1[0], latlong1[1]));

                //coordinates.Add(new SharpKml.Base.Vector(latlong1[0], latlong1[1]));
                outerBoundary.LinearRing.Coordinates.Add(new SharpKml.Base.Vector(latlong1[0], latlong1[1]));

            }

            mPolygon.OuterBoundary = outerBoundary;
            lPolygon.Add(mPolygon);

            //linestring.Coordinates = coordinates;
            //poly.Coordinates = coordinates;

            SharpKml.Dom.Placemark placemark_lineCP = new SharpKml.Dom.Placemark();
            placemark_lineCP.Name = "BoundaryCirclePolygon";
            placemark_lineCP.Geometry = mPolygon;
            //placemark_lineC.Geometry = poly;
            placemark_lineCP.StyleUrl = new Uri("#StyleCP", UriKind.Relative);

            document_CP.AddFeature(placemark_lineCP);

            var kmlCP = new Kml
            {
                Feature = document_CP
            };
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameCP = MyFolder + "BoundaryCirclePolygon.kml";
            FileStream fileStreamCP = new FileStream(kmlfilenameCP, FileMode.OpenOrCreate);
            serializer.Serialize(kmlCP, fileStreamCP);


            MessageBox.Show("Boundary Circle Polygon successfully exported to KML");
        }

        private void kMLExtremePointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double lat1, long1, lat2, long2, lat_ARP, Long_ARP;
            string MyFolder = CreateBaseAccessProjectFolders();
           
            var serializer = new Serializer();

            //Vertical i.e., perpendicular points
            var document_VP = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Points of Vertical Gridlines Perpendicular to Base line"
                },
                Name = "VP_" + TxtAirportCode.Text + "_Points"
            };

            // This will be used for the placemark-----------------

            for (int i = First_V_Row; i <= Last_V_Row; i++)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                SharpKml.Dom.Point ptV = new SharpKml.Dom.Point();

                ptV.Coordinate = new SharpKml.Base.Vector(lat1, long1);
                SharpKml.Dom.Placemark placemark_VP = new SharpKml.Dom.Placemark();
                placemark_VP.Name = "Extreme_VPoints-" + i.ToString();
                placemark_VP.Geometry = ptV;
                document_VP.AddFeature(placemark_VP);
               
            }

            // This is the root element of the file--------------------------
            var kmlVP = new Kml
            {
                Feature = document_VP
            };
            //var serializer = new Serializer();
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameVP = MyFolder + "PerpendicularPoints.kml";
            FileStream fileStreamVP = new FileStream(kmlfilenameVP, FileMode.OpenOrCreate);
            serializer.Serialize(kmlVP, fileStreamVP);



            //Horizontal i.e., parallel points
            var document_HP = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Points of Horizontal Gridlines Parallel to Base line"
                },
                Name = "HP_" + TxtAirportCode.Text + "_Points"
            };

            // This will be used for the placemark-----------------

            for (int i = First_Hz_Row; i <= Last_Hz_Row; i++)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                SharpKml.Dom.Point ptH = new SharpKml.Dom.Point();
                ptH.Coordinate = new SharpKml.Base.Vector(lat1, long1);
                SharpKml.Dom.Placemark placemark_HP = new SharpKml.Dom.Placemark();
                placemark_HP.Name = "Extreme_HPoints-" + i.ToString();
                placemark_HP.Geometry = ptH;
                document_HP.AddFeature(placemark_HP);
            }

            // This is the root element of the file--------------------------
            var kmlHP = new Kml
            {
                Feature = document_HP
            };
            //var serializer = new Serializer();
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameHP = MyFolder + "ParallelPoints.kml";
            FileStream fileStreamHP = new FileStream(kmlfilenameHP, FileMode.OpenOrCreate);
            serializer.Serialize(kmlHP, fileStreamHP);



            //Origin points
            var document_OP = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Points of Origin axes"
                },
                Name = "OP_" + TxtAirportCode.Text + "_Points"
            };

            // This will be used for the placemark-----------------

            for (int i = 7; i <= 10; i++)
            {
                lat1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                long1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);

                SharpKml.Dom.Point ptO = new SharpKml.Dom.Point();
                ptO.Coordinate = new SharpKml.Base.Vector(lat1, long1);
                SharpKml.Dom.Placemark placemark_OP = new SharpKml.Dom.Placemark();
                placemark_OP.Name = "Extreme_OPoints-" + i.ToString();
                placemark_OP.Geometry = ptO;
                document_OP.AddFeature(placemark_OP);
            }

            // This is the root element of the file--------------------------
            var kmlOP = new Kml
            {
                Feature = document_OP
            };
            //var serializer = new Serializer();
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameOP = MyFolder + "OriginAxesPoints.kml";
            FileStream fileStreamOP = new FileStream(kmlfilenameOP, FileMode.OpenOrCreate);
            serializer.Serialize(kmlOP, fileStreamOP);


            /*
            //Vertical lines
            var document_Ver_pts = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Points of Vertical Gridlines Perpendicular to Base line"
                },
                Name = "Vertical Gridlines Extreme points"
            };

            ///Style 1
            SharpKml.Dom.LineStyle lineStyle = new SharpKml.Dom.LineStyle();
            lineStyle.Color = Color32.Parse("ff0000ff");
            lineStyle.Width = 1;

            SharpKml.Dom.Style SimpleStyle = new SharpKml.Dom.Style();
            SimpleStyle.Id = "Style1";
            SimpleStyle.Line = lineStyle;
            document_Ver_pts.AddStyle(SimpleStyle);

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
                placemark_line.StyleUrl = new Uri("#Style1", UriKind.Relative);
                document_Ver_pts.AddFeature(placemark_line);
            }

            //folder_GL.AddFeature(document_Ver);
            var kmlV = new Kml
            {
                Feature = document_Ver_pts
            };
            string kmlfilenameV = MyFolder + "VerticalGridlines.kml";
            FileStream fileStreamV = new FileStream(kmlfilenameV, FileMode.OpenOrCreate);
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

            //StyleH
            SharpKml.Dom.LineStyle lineStyleH = new SharpKml.Dom.LineStyle();
            lineStyleH.Color = Color32.Parse("ffff0000");//First two transparency; then Blue, green ,red
            lineStyleH.Width = 1;

            SharpKml.Dom.Style SimpleStyleH = new SharpKml.Dom.Style();
            SimpleStyleH.Id = "StyleH";
            SimpleStyleH.Line = lineStyleH;
            document_Hz.AddStyle(SimpleStyleH);

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
                placemark_line1.StyleUrl = new Uri("#StyleH", UriKind.Relative);

                document_Hz.AddFeature(placemark_line1);
            }
            //folder_GL.AddFeature(document_Hz);

            var kmlH = new Kml
            {
                Feature = document_Hz
            };
            string kmlfilenameH = MyFolder + "Hz_Gridlines.kml";
            FileStream fileStreamH = new FileStream(kmlfilenameH, FileMode.OpenOrCreate);
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

            ///StyleH
            SharpKml.Dom.LineStyle lineStyleO = new SharpKml.Dom.LineStyle();
            lineStyleO.Color = Color32.Parse("ff00ffff");//First two transparency; then Blue, green ,red
            lineStyleO.Width = 2;

            SharpKml.Dom.Style SimpleStyleO = new SharpKml.Dom.Style();
            SimpleStyleO.Id = "StyleO";
            SimpleStyleO.Line = lineStyleO;
            document_Or.AddStyle(SimpleStyleO);

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
                placemark_line2.StyleUrl = new Uri("#StyleO", UriKind.Relative);

                document_Or.AddFeature(placemark_line2);
            }
            //folder_GL.AddFeature(document_Or);
            var kmlOr = new Kml
            {
                Feature = document_Or
            };
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameOr = MyFolder + "Origin Axes.kml";
            FileStream fileStreamOr = new FileStream(kmlfilenameOr, FileMode.OpenOrCreate);
            serializer.Serialize(kmlOr, fileStreamOr);*/


            MessageBox.Show("Extreme points Successfully exported to KML");
        }

        private void kMLCircleCenteredAtARPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string MyFolder = CreateBaseAccessProjectFolders();
            var serializer = new Serializer();
            List<PointLatLng> points = new List<PointLatLng>();

            double[] latlong1 = new double[2];
            double aa, b, a1, b1, a_E, b_E;
            double seg, theta;
            int segments = 6000;

            CSAYMapGeoFunction CSAYGeoFun = new CSAYMapGeoFunction();
            double a, one_by_f, K0, M0, lambda0, phi0_DD;
            phi0_DD = 0;
            //Input parameters
            a = 6378137.0;
            one_by_f = 298.2572201;
            K0 = 0.9996;
            M0 = 0; //distance in meter of origin latitude from equator
            lambda0 = Convert.ToDouble(TxtCM.Text);

            double[] ARP_XY = new double[2];
            a_E = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value);
            b_E = Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);
            double r = Convert.ToDouble(TxtRadius.Text);
            ARP_XY = CSAYGeoFun.Convert_LatLong_To_UTM(a_E, b_E, a, one_by_f, K0, M0, phi0_DD, lambda0);
            aa = ARP_XY[0];
            b = ARP_XY[1];

            //Export to  KML
            //Draw Bounding circle---------------------------------------------------------------------
            var document_C = new SharpKml.Dom.Document
            {
                Description = new SharpKml.Dom.Description
                {
                    Text = "Doc Boundary circle"
                },
                Name = "Boundary circle"
            };

            ///StyleC
            SharpKml.Dom.LineStyle lineStyleC = new SharpKml.Dom.LineStyle();
            lineStyleC.Color = Color32.Parse("ff00ffff");//First two transparency; then Blue, green ,red
            lineStyleC.Width = 2;

            SharpKml.Dom.Style SimpleStyleC = new SharpKml.Dom.Style();
            SimpleStyleC.Id = "StyleC";
            SimpleStyleC.Line = lineStyleC;
            document_C.AddStyle(SimpleStyleC);

            LineString linestring = new LineString();
            CoordinateCollection coordinates = new CoordinateCollection();

            //convert latlong to UTM
            double Fasle_Easting_X = 500000;
            seg = (Math.PI * 2) / segments;//Math.PI * 2 / segments;
            for (int i = 0; i < segments; i++)
            {
                theta = seg * i;
                a1 = aa + Math.Cos(theta) * r;
                b1 = b + Math.Sin(theta) * r;

                latlong1 = CSAYGeoFun.Convert_UTM_To_Latitude_Longitude(a1, b1, a, one_by_f, K0, M0, Fasle_Easting_X, lambda0);
                points.Add(new PointLatLng(latlong1[0], latlong1[1]));

                coordinates.Add(new SharpKml.Base.Vector(latlong1[0], latlong1[1]));
                
            }

            linestring.Coordinates = coordinates;
            SharpKml.Dom.Placemark placemark_lineC = new SharpKml.Dom.Placemark();
            placemark_lineC.Name = "BoundaryCircle";
            placemark_lineC.Geometry = linestring;
            placemark_lineC.StyleUrl = new Uri("#StyleC", UriKind.Relative);

            document_C.AddFeature(placemark_lineC);

            var kmlC = new Kml
            {
                Feature = document_C
            };
            //string kmlfilename = Environment.CurrentDirectory + "\\KML_Files" + "\\ThisKML.kml";
            string kmlfilenameOr = MyFolder + "BoundaryCircle.kml";
            FileStream fileStreamOr = new FileStream(kmlfilenameOr, FileMode.OpenOrCreate);
            serializer.Serialize(kmlC, fileStreamOr);


            MessageBox.Show("Boundary Circle successfully exported to KML");


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
