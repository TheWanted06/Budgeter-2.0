using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeter_2._0
{
    public class Calc_And_Display
    {
        public double general(List<double> general)
        {
            double generalTotal=0;
            double gen;
            for (int i = 1; i < general.Count; i++)
            {
                gen= general[i];
                generalTotal += gen;
            }
            return generalTotal;

        }

        public double housing(bool isRenting,List<double> housingList)
        {
            double housing;
            if (isRenting == true)
            {
                housing = housingList[0];
            }
            else
            {
                //the formular for hire purchase is: A = P(1+in)
                //A >> Accummulated amount (final)
                //P >> Purchase price / principal amount (initial)
                //i >> Interest rate written as a decimal
                //n >> number of months


                double pp = housingList[0];
                double dp = housingList[1];
                double i = housingList[2];
                double n = housingList[3];
                double A = pp - dp;

                double total = A * (1 + (i * n));

                housing = total / n;
            }
            return housing;
        }
        public double vehicle(bool isPurchasingCar, List<double> CarList)
        {
            double total;
            if(isPurchasingCar == true)
            {
                double purchaseprice = CarList[0];
                double deposit = CarList[1];

                double remaining = purchaseprice - deposit;
                double interest = ((CarList[2] / 100) / 12);
                double duration = 5 * 12;

                double topside = remaining * interest;
                double bottomside = (1 - Math.Pow((1 + interest),-duration));
                total = (topside / bottomside) + CarList[3];

                return total;
            }
            else
            {
                total = 0;
            }

            return total;
        }

        public double total(List<double> generalList,double generalExpenses, double housingTotal, double vehicleTotal)
        {
            double totalExpenses = generalExpenses + housingTotal + vehicleTotal;
            double total = generalList[0] - totalExpenses;
            return total;
        }

        public void totalDisplay(List<double> generalList,bool isRenting, List<double> housingList, bool useCar, List<double> vehicleList,double total)
        {
            //SortedList<double, string> generalSorted = new SortedList<double, string>();
            var myList = new List<KeyValuePair<string, double>>();

            for (int i = 1; i < generalList.Count; i++)
            {
                //generalSorted.Add(generalList[i], Prompts.generals[i]);
                myList.Add(new KeyValuePair<string, double>(Prompts.generals[i], generalList[i]));
            }

            if(isRenting == true)
            {
                myList.Add(new KeyValuePair<string, double>(Prompts.housing[0], housingList[0]));
            }
            else
            {
                for (int j = 0; j < housingList.Count; j++)
                {
                    myList.Add(new KeyValuePair<string, double>(Prompts.housing[j+1], housingList[j]));
                }
            }
            if(useCar == true)
            {
                for (int i = 1; i < vehicleList.Count; i++)
                {
                    myList.Add(new KeyValuePair<string, double>(Prompts.vehicle[i], vehicleList[0]));
                }
            }

            string display1 = (
                        "*******************************************\n" +
                        "                   RESULTS                 \n" +
                        "*******************************************\n");
            string display2 = $"{Prompts.generals[0]}:\t\t\t{generalList[0]}\n";
            string display3 = "";

            //sorted value
            myList.Sort((x, y) => (y.Value.CompareTo(x.Value)));
            foreach (var val in myList)
            {
                display3 += $"{val.Key}:\t\t\t{val.Value}\n";
            }

            string display4 = ("*********************************************\n" +
                        $"The monthly remaining amount is    {total}\n" +
                        $"**********************************************");
            string displayAll = display1+display2+ display3 + display4;
            Console.WriteLine(displayAll);
        }

    }
}
