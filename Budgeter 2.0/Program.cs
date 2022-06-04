using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Budgeter_2._0
{
    //a delegate is declaired here outside of the class
    delegate bool InputCheck(string a);
    delegate void ThePrompts();
    delegate double Calc(bool a, List<double> b);
    internal class Program
    {
        static void Main(string[] args)
        {
            Checker checker = new Checker();

            Console.WriteLine("Welcome to the Budget app");
            Console.WriteLine("To continue please enter Y ");
            var input1 = Console.ReadLine();

            if(input1 == "Y"|| input1 == "y")
            {
                MainLogic();
            }
            Console.WriteLine("Thank you for using the Budget app. Goodbye");
            Console.ReadKey();
        }
        public static void Notify()
        {
            Console.WriteLine("Your total expenses exceeds 75% of you income");
        }
        public static void MainLogic()
        {
            Calc_And_Display cnd = new Calc_And_Display();
            ThePrompts cpt = new ThePrompts(Notify);

            //Lists of Expenses
            List<double> generalExpenses = new List<double>();
            List<double> housingExpenses = new List<double>();
            List<double> vehicleExpenses = new List<double>();

            bool calculateRenting;
            bool calculateVehicle;

            bool generalCompleted = GeneralPrompts(out generalExpenses);
            bool housingCompleted = HousingPrompts(out housingExpenses, out calculateRenting);
            bool vehicleCompleted = VehiclePrompts(out vehicleExpenses, out calculateVehicle);

            if (generalCompleted == true && housingCompleted == true && vehicleCompleted == true)
            {
                double generalTotal = cnd.general(generalExpenses);
                double housingTotal = cnd.housing(calculateRenting, housingExpenses);
                double vehicleTotal = cnd.vehicle(calculateVehicle,vehicleExpenses);
                double total = cnd.total(generalExpenses,generalTotal, housingTotal, vehicleTotal);
                if(total > (generalExpenses[0]) * 0.75)
                {
                    cpt.Invoke();
                }
                else
                {
                    cnd.totalDisplay(generalExpenses,calculateRenting,housingExpenses,calculateVehicle,vehicleExpenses, total);
                }
            }
            else
            {
                Console.WriteLine("Unfortunately an Error has occured");
            }
        }
        public static bool GeneralPrompts(out List<double> m)
        {
            m = new List<double>();
            Checker checker = new Checker();
            InputCheck input = new InputCheck(checker.InputChecking);

            bool isChecked = true;
            int internalcounter = 0;
            for (int i = 0; i < checker.promptGeneral.Length; i = i + 2)
            {
                
                Console.WriteLine(checker.promptGeneral[i]);
                var oldInput = Console.ReadLine();

                //and then check
                isChecked = input(oldInput);
                int counter = 0;

                while (isChecked == false && counter < 3)
                {
                    Console.WriteLine(checker.promptGeneral[i + 1]);
                    oldInput = Console.ReadLine();
                    isChecked = input(oldInput);
                    counter++;
                }
                if (isChecked == false)
                {
                    break;
                }
                if (isChecked == true)
                {
                    double newInput = Convert.ToInt32(oldInput);
                    m.Add(newInput);
                    internalcounter++;
                }
            }
            return isChecked;
        }
        public static bool HousingPrompts(out List<double> m, out bool pickRenting)
        {
            m = new List<double>();
            //List<double> HousingList = new List<double>();
            bool isChecked = true;
            Checker checker = new Checker();

            Console.WriteLine("To calculate housing please indicate if you are renting or buying a property \n" +
            "Please enter R for renting or B for buying property");
            var housing = Console.ReadLine();
            bool houseChecked = checker.CheckHousing(housing);
            int newcounter = 0;
            while (houseChecked == false && newcounter < 3)
            {
                Console.WriteLine("invalid entry. Please re-enter your housing option.\n" +
                    "Please enter R for renting or B for buying property");
                housing = Console.ReadLine();
                houseChecked = checker.CheckHousing(housing);
                newcounter++;
            }
            if (houseChecked == false)
            {
                isChecked = false;
                pickRenting = true;
            }
            else
            {
                if (housing == "r"||housing == "R")
                {
                    isChecked = RentalPrompt(out m);
                    pickRenting = true;
                }
                else
                {
                    isChecked = BuyingPropertyPrompt(out m);
                    pickRenting=false;
                }
            }
            return isChecked;
        }

        public static bool BuyingPropertyPrompt(out List<double> rental)
        {
            rental = new List<double>();
            bool completed = true;
            Checker checker = new Checker();
            InputCheck input = new InputCheck(checker.InputChecking);

            bool checkbuying = true;
            for (int i = 2; i < checker.prompthHousing.Length; i = i + 2)
            {
                Console.WriteLine(checker.prompthHousing[i]);
                var buying = Console.ReadLine();
                checkbuying = input(buying);
                int counter3 = 0;
                while (checkbuying == false && counter3 < 3)
                {
                    Console.WriteLine(checker.prompthHousing[i + 1]);
                    buying = Console.ReadLine();
                    checkbuying = input(buying);
                    counter3++;
                }
                if (checkbuying == false)
                {
                    break;
                }
                else
                {
                    rental.Add(Convert.ToDouble(buying));
                }
            }
            if (checkbuying == false)
            {
                completed = false;
            }
            return completed;
        }

        private static bool RentalPrompt(out List<double> rental)
        {
            rental = new List<double>();

            Checker checker = new Checker();
            InputCheck input = new InputCheck(checker.InputChecking);

            Console.WriteLine(checker.prompthHousing[0]);
            var buying = Console.ReadLine();

            bool theRental = input(buying);
            int counter3 = 0;
            while (theRental == false && counter3 < 3)
            {
                Console.WriteLine(checker.prompthHousing[1]);
                buying = Console.ReadLine();
                theRental = input(buying);
                counter3++;
            }
            if (theRental == true)
            {
                rental.Add(Convert.ToInt32(buying));
            }
            return theRental;
        }

        public static bool VehiclePrompts(out List<double> m, out bool k)
        {
            
            m = new List<double>();
            //List<double> HousingList = new List<double>();
            bool isChecked = false;
            k = false;
            Console.WriteLine("Are you buying a vehicle? type Y for yes and N for No");
            var isBuyingVehicle = Console.ReadLine();

            if (isBuyingVehicle == "Y" || isBuyingVehicle == "y")
            {
                isChecked = CarPrompt(out m);
                k = true;
            }
            else if(isBuyingVehicle =="N" ||isBuyingVehicle == "n"){
                isChecked = true;
            }
            return isChecked;
        }

        private static bool CarPrompt(out List<double> m)
        {
            m = new List<double>();

            Checker checker = new Checker();
            InputCheck input = new InputCheck(checker.InputChecking);

            bool isChecked = true;
            Console.WriteLine(checker.promptVehicle[0]);
            var buyingVehicle = Console.ReadLine();
            int VhcModel = 0;
            if (buyingVehicle == null || buyingVehicle.Length <= 4 && VhcModel < 3)
            {
                Console.WriteLine(checker.promptVehicle[1]);
                buyingVehicle = Console.ReadLine();
                VhcModel++;
            }

            for (int i = 2; i < checker.promptVehicle.Length; i = i + 2)
            {
                Console.WriteLine(checker.promptVehicle[i]);
                var buying = Console.ReadLine();
                isChecked = input(buying);
                int counter3 = 0;
                while (isChecked == false && counter3 < 3)
                {
                    Console.WriteLine(checker.promptVehicle[i + 1]);
                    buying = Console.ReadLine();
                    isChecked = input(buying);
                    counter3++;
                }
                if (isChecked == false)
                {
                    break;
                }
                else
                {
                    m.Add(Convert.ToDouble(buying));
                }
            }
            return isChecked;
        }
    }
}
