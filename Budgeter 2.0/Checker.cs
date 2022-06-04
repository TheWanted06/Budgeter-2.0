using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeter_2._0
{
    public class Checker
    {
        public string[] promptGeneral ={
                "Please enter your "+Prompts.generals[0],
                "Please re-enter your "+Prompts.generals[0],
                "Please enter your estimated "+Prompts.generals[1],
                "Please re-enter your estimated "+Prompts.generals[1],
                "Please enter your monthly "+Prompts.generals[2],
                "Please re-enter your monthly "+Prompts.generals[2],
                "Please enter your monthly "+Prompts.generals[3],
                "Please re-enter your monthly "+Prompts.generals[3],
                "Please enter your monthly Travel "+Prompts.generals[4],
                "Please re-enter your monthly Travel "+Prompts.generals[4],
                "Please enter your "+Prompts.generals[5]+" costs",
                "Please re-enter your "+Prompts.generals[5]+" costs",
                "Please enter your "+Prompts.generals[6],
                "Please re-enter your "+Prompts.generals[6]
            };
        public string[] prompthHousing ={
                "Please enter your "+Prompts.housing[0],
                "Please re-enter your "+Prompts.housing[0],
                "Please enter your "+Prompts.housing[1]+" of the property.",
                "Please re-enter your "+Prompts.housing[1]+" of the property.",
                "Please enter your "+Prompts.housing[2],
                "Please re-enter your "+Prompts.housing[02],
                "Please enter your "+Prompts.housing[3],
                "Please re-enter your "+Prompts.housing[3],
                "Please enter the "+Prompts.housing[4],
                "Please re-enter the "+Prompts.housing[4]
            };
        public string[] promptVehicle ={
                "Please enter your "+Prompts.vehicle[0],
                "Please re-enter your "+Prompts.vehicle[0],
                "Please enter your "+Prompts.vehicle[1],
                "Please re-enter your "+Prompts.vehicle[1],
                "Please enter your "+Prompts.vehicle[2],
                "Please re-enter your "+Prompts.vehicle[2],
                "Please enter your "+Prompts.vehicle[3],
                "Please re-enter your "+Prompts.vehicle[3],
                "Please enter your "+Prompts.vehicle[4],
                "Please re-enter your "+Prompts.vehicle[4]
            };

        public bool InputChecking(string a)
        {
            int intnumber = 0;
            double doublenumber = 0;

            bool isParsableInt = Int32.TryParse(a, out intnumber);
            bool isParsableDouble = Double.TryParse(a, out doublenumber);

            bool isPositive = false;
            if (intnumber >= 0 || doublenumber >= 0)
            {
                isPositive = true;
            }

            if (isParsableInt || isParsableDouble && isPositive == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckHousing(string housing)
        {
            bool isChecked;
            bool rental = rentalChecking(housing);
            bool buying = buyingChecking(housing);

            if (rental == true && buying == false)
            {
                isChecked = true;
            }
            else if (buying == true && rental == false)
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            return isChecked;

        }
        public bool rentalChecking(string rent)
        {
            bool isrental = false;
            if (rent == "R" || rent == "r")
            {
                isrental = true;
            }
            return isrental;
        }
        public bool buyingChecking(string buy)
        {
            bool isbuying = false;
            if (buy == "b" || buy == "B")
            {
                isbuying = true;
            }
            return isbuying;
        }

    }

}
