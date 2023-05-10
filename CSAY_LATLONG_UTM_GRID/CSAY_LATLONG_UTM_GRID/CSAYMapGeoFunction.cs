using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAY_LATLONG_UTM_GRID
{
    internal class CSAYMapGeoFunction
    {
        public double Find_Slope_Of_Equation(double X1, double Y1, double X2, double Y2)
        {
            double slope;
            slope = (Y2 - Y1) / (X2 - X1);
            return slope;
        }

        public double Find_Intercept_Of_Equation(double slope, double X1, double Y1)
        {
            double intercept;
            intercept = (Y1 - slope * X1);
            return intercept;
        }

        public double Find_Intercept_Of_Equation(double X1, double Y1, double X2, double Y2)
        {
            double intercept;
            double slope;
            slope = (Y2 - Y1) / (X2 - X1);
            intercept = (Y1 - slope * X1);
            return intercept;
        }

        public double Find_Distance_Of_LineXY(double X1, double Y1, double X2, double Y2)
        {
            double distance, del_X, del_Y;
            del_X = Math.Abs(X2 - X1);
            del_Y = Math.Abs(Y2 - Y1);
            distance = Math.Sqrt(del_X * del_X + del_Y * del_Y);
            return distance;
        }

        public double Intercept_of_Parallel_line(double slope_1, double intercept_1, double distance_offset, int Line_Above)
        {
            double intercept_2 = 0;
            intercept_2 = intercept_1 + Line_Above * distance_offset * (Math.Sqrt(1 + slope_1 * slope_1));
            //Line_Above = 1 means parallel line is above the point on Runway i.e. parallel to CD
            //Line_Above = -1 means parallel line is below the point on Runway i.e. parallel to AB

            return intercept_2;
        }

        public double Find_Quadratic_X(double slope1, double intercept1, double a, double b, double radius, int factor)
        {
            double A, B, C, Quad_x;
            A = (slope1 * slope1 + 1);
            B = 2 * (slope1 * (intercept1 - b) - a);
            C = a * a + (intercept1 - b) * (intercept1 - b) - radius * radius;
            Quad_x = (-B + factor * Math.Sqrt(B * B - 4 * A * C)) / (2 * A); //J_X
            //Quad_x_minus = (-B - Math.Sqrt(B * B - 4 * A * C)) / (2 * A);//I_X
            return Quad_x;
        }

        public double[] Find_Intersection_XY(double slope1, double intercept1, double slope2, double intercept2)
        {
            double[] XY = new double[2];
            XY[0] = (intercept1 - intercept2) / (slope2 - slope1);
            XY[1] = (slope2 * intercept1 - slope1 * intercept2) / (slope2 - slope1);
            return XY;
        }

        public double[] Convert_LatLong_To_UTM(double latitude_in_degree, double longitude_in_degree, double a, double one_by_f, double K0, double M0, double phi0_DD, double lambda0_DD)
        {
            //double a, one_by_f, lambda0_DD, phi0_DD, K0, M0, f;
            double f;
            double[] EastNorthXY = new double[2];
            //double Easting_X, Northing_Y, f; 
            //double e_2, e_prime_2, mu, e1, phi1, R1, T1, C1, x, N1, D, phi, lambda;

            //Parameter values for WGS and UTM84

            //Input parameters
            /*a = 6378137.0;
            one_by_f = 298.2572201;
            K0 = 0.9996;
            M0 = 0; //distance in meter of origin latitude from equator
            f = 1 / one_by_f;
            phi0_DD = 0;
            lambda0_DD = 84;
            //lambda0_DD = Convert.ToDouble(TxtCM.Text);*/
            f = 1 / one_by_f;
            var phi0 = phi0_DD * Math.PI / 180;
            var lambda0 = lambda0_DD * Math.PI / 180;

            double Phi_DD = latitude_in_degree; //latitude input in degree decimal
            var Phi = Phi_DD * Math.PI / 180;//lat in radian

            double Lambda_DD = longitude_in_degree; //longitude input in degree decimal
            var Lambda = Lambda_DD * Math.PI / 180; //long in radian

            var e2 = 2 * f - f * f;
            var e_prime2 = e2 / (1 - e2);
            var RM = a * (1 - e2) / Math.Pow((1 - e2 * Math.Pow(Math.Sin(Phi), 2)), 3 / 2);
            var RN = a / Math.Sqrt(1 - e2 * Math.Sin(Phi) * Math.Sin(Phi));
            var T = Math.Tan(Phi) * Math.Tan(Phi);
            var C = e_prime2 * Math.Cos(Phi) * Math.Cos(Phi);
            var A1 = (Lambda - lambda0) * Math.Cos(Phi);
            var M_term1 = (1 - e2 / 4 - 3 * e2 * e2 / 64 - 5 * e2 * e2 * e2 / 256) * Phi;
            var M_term2 = (3 * e2 / 8 + 3 * e2 * e2 / 32 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(2 * Phi);
            var M_term3 = (15 * e2 * e2 / 256 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(4 * Phi);
            var M_term4 = (35 * e2 * e2 * e2 / 3072) * Math.Sin(6 * Phi);
            var M = a * (M_term1 - M_term2 + M_term3 - M_term4);


            var X_term1 = (1 - T + C) * A1 * A1 * A1 / 6;
            var X_term2 = (5 - 18 * T + T * T + 72 * C - 58 * e_prime2) * Math.Pow(A1, 5) / 6;

            var Easting_X = K0 * RN * (A1 + X_term1 + X_term2) + 500000;             //x coordinate

            var Y_term1 = (5 - T + 9 * C + 4 * C * C) * Math.Pow(A1, 4) / 24;

            var Y_term2 = (61 - 58 * T + T * T + 600 * C - 330 * e_prime2) * Math.Pow(A1, 6) / 720;
            var Northing_Y = K0 * (M - M0 + RN * Math.Tan(Phi) * (A1 * A1 / 2 + Y_term1 + Y_term2)); // y coordinate

            EastNorthXY[0] = Easting_X;
            EastNorthXY[1] = Northing_Y;
            return EastNorthXY;
        }

        public double[] Convert_UTM_To_Latitude_Longitude(double Easting_X, double Northing_Y, double a, double one_by_f, double K0, double M0, double False_Easting_X, double lambda0)
        {
            //double a, one_by_f, lambda0, K0, M0, False_Easting_X;
            //double lambda0;
            double f;
            double M, e_2, e_prime_2, mu, e1, phi1, R1, T1, C1, x, N1, D, phi, lambda;
            double[] LatLong = new double[2];

            //Parameter values for WGS and UTM84
            /*False_Easting_X = 500000.0;
            //False_Northing_Y = 0;
            a = 6378137.0;
            one_by_f = 298.2572201;
            K0 = 0.9996;
            M0 = 0; //distance in meter of origin latitude from equator

            //Input
            lambda0 = 84.0; //central meridian for zone 44*/

            //Formula and equation for conversion from UTM to WGS
            f = 1 / one_by_f;
            M = M0 + Northing_Y / K0;
            e_2 = 2.0 * f - f * f;
            e_prime_2 = e_2 / (1.0 - e_2);
            mu = M / (a * (1.0 - e_2 / 4.0 - 3.0 * e_2 * e_2 / 64.0 - 5.0 * e_2 * e_2 * e_2 / 256.0));
            e1 = (1.0 - Math.Sqrt(1 - e_2)) / (1 + Math.Sqrt(1.0 - e_2));

            double phi1_term1 = (3.0 * e1 / 2.0 - 27.0 * e1 * e1 * e1 / 32) * Math.Sin(2 * mu);
            double phi1_term2 = (21.0 * e1 * e1 / 16.0 - 55.0 * e1 * e1 * e1 * e1 / 32.0) * Math.Sin(4 * mu);
            double phi1_term3 = (151.0 * e1 * e1 * e1 / 96.0) * Math.Sin(6 * mu);
            double phi1_term4 = (1097.0 * e1 * e1 * e1 * e1 / 512.0) * Math.Sin(8 * mu);

            phi1 = mu + phi1_term1 + phi1_term2 + phi1_term3 + phi1_term4;

            R1 = a * (1.0 - e_2) / Math.Pow((1.0 - e_2 * Math.Sin(phi1) * Math.Sin(phi1)), 3.0 / 2.0);
            T1 = Math.Tan(phi1) * Math.Tan(phi1);
            C1 = e_prime_2 * Math.Cos(phi1) * Math.Cos(phi1);
            x = Easting_X - False_Easting_X;
            N1 = a / (Math.Sqrt(1.0 - e_2 * Math.Sin(phi1) * Math.Sin(phi1)));
            D = x / (N1 * K0);
            double phi_t1 = D * D / 2.0 - (5.0 + 3.0 * T1 + 10.0 * C1 - 4.0 * C1 * C1 - 9.0 * e_prime_2) * D * D * D * D / 24.0;
            double phi_t2 = (61.0 + 90.0 * T1 + 298.0 * C1 + 45.0 * T1 * T1 - 252.0 * e_prime_2 - 3.0 * C1 * C1) * D * D * D * D * D * D / 720.0;

            phi = phi1 - (N1 * Math.Tan(phi1) / R1) * (phi_t1 + phi_t2); //latitude in radian

            double lambda_t1 = D - (1.0 + 2.0 * T1 + C1) * D * D * D / 6.0;
            double lambda_t2 = (5.0 - 2.0 * C1 + 28.0 * T1 - 3 * C1 * C1 + 8.0 * e_prime_2 + 24.0 * T1 * T1) * D * D * D * D * D / 120.0;
            lambda = lambda0 * Math.PI / 180.0 + (lambda_t1 + lambda_t2) / Math.Cos(phi1); //longitude in radian

            //Final_Latitude_DD = phi * 180.0 / Math.PI;
            //Final_Longitude_DD = lambda * 180.0 / Math.PI;
            LatLong[0] = phi * 180.0 / Math.PI;
            LatLong[1] = lambda * 180.0 / Math.PI;
            return LatLong;

            //MessageBox.Show("phi_t1 +t2 = " + (phi_t1+phi_t2)* ((N1 * Math.Tan(phi1) / R1)) + "\nphi1 = " + phi1);
        }

    }
}
