using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    static class Predictor
    {
        public static int[] GatherSalesData(string workbook, string table, DateTime from, DateTime to, int idToSearch)
        {
            DateTime current = from.Date;

            List<int> delta = new List<int>();

            int i;

            int sum = 0;

            int sectionSize = 7;//A week, 7 days

            int iterations = 0;

            while (current <= to)
            {
                int[] tempRows;

                tempRows = SalesRecord.SearchFor(workbook, table, true, true, false, false, current, idToSearch, "", 0);

                if (iterations == sectionSize)
                {
                    delta.Add(sum);

                    sum = 0;
                    iterations = 0;
                }

                i = 0;
                while (i < tempRows.Length)
                {
                    sum += Database.ReadSalesRecord(workbook, table, tempRows[i]).Quantity;

                    i += 1;
                }

                current = current.AddDays(1);//progress a day

                iterations += 1;
            }

            delta.Add(sum);//There is no case where this shouldn't occur

            return delta.ToArray();
        }

        public static Prediction PredictLinear(string workbook, string table, DateTime from, DateTime to, int idToSearch)
        {
            //y = mx + c
            int[] data = Predictor.GatherSalesData(workbook, table, from, to, idToSearch);

            double xSum = 0;

            double ySum = 0;

            double xySum = 0;

            double xSquaredSum = 0;

            double ySquaredSum = 0;

            double ySmallest = 0;
            double ySmallestX = 0;

            double yLargest = 0;
            double yLargestX = 0;

            if (data.Length > 0)
            {
                ySmallest = data[0];
                ySmallestX = 0;

                yLargest = data[0];
                yLargestX = 0;
            }


            int i = 0;
            while (i < data.Length)
            {
                int x = (i + 1);

                int y = data[i];

                if (y > yLargest)
                {
                    yLargest = y;
                    yLargestX = i;
                }

                if (y < ySmallest)
                {
                    ySmallest = y;
                    ySmallestX = i;
                }


                xSum += x;

                ySum += y;

                xySum = (y * x);

                xSquaredSum += x * x;

                ySquaredSum += y * y;



                i += 1;
            }            

            double xBar = xSum / data.Length;//average

            double yBar = ySum / data.Length;//average

            double smudge = 1;

            if (ySmallestX < yLargestX)
            {
                smudge *= -1;
            }

            else
            {
                smudge *= 1;
            }

            double offset = yBar+smudge;//yBar - gradient * xBar;// c term

            double gradient = (yBar-offset)/(xBar);// (yBar/xBar)/data.Length; // m term

            double residule = (data.Length*xySum - xSum * ySum) / Math.Sqrt((data.Length * xSquaredSum - (xSum * xSum))*(data.Length * ySquaredSum - (ySum * ySum)));

            double rSquared = residule * residule;

            double expected = 0;

            i = 0;
            while(i < 4)//Predict fot a month
            {
                int x = (data.Length + 1) + i;// start at the week after the last record

                expected += gradient * x + offset;

                i += 1;
            }

            Prediction result = new Prediction(((1 - rSquared) * expected), expected);

            return result;
        }
    }
}
