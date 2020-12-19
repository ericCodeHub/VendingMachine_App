using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Capstone
{
    public class VendingMachine
    {
        
        
        const string AUDIT_REPORT_LOG_PATH = @"C:\Users\Student\workspace\module1-capstone-c-team-7\Example Files\Log1.txt";
        const string SALES_REPORT_LOG_PATH = @"C:\Users\Student\workspace\module1-capstone-c-team-7\Example Files\SalesReport1.txt";
        const string INVENTORY_LIST_PATH = @"C:\Users\Student\workspace\module1-capstone-c-team-7\Example Files\Inventory.txt";


        private Dictionary<string, int> endOfDaySalesReport = new Dictionary<string, int>();
        public Dictionary<string, ProductInventory> ProductList = new Dictionary<string, ProductInventory>();
        public decimal Balance { get; set; } = 0;
        public decimal TotalSales { get; set; } = 0;

        public string PurchaseProduct(string input)
        {
            ProductInventory product = ProductList[input];
            if (product.Price > Balance)
            {
                return "\nInsufficient Funds; Please Deposit Additional Funds";

            }
            else if (product.AmountOfProduct == 0)
            {
                return "SOLD OUT";
            }
            else
            {
                Balance -= product.Price;
                product.AmountOfProduct--;
                endOfDaySalesReport[product.Name]++;

                TotalSales += product.Price;

                WriteToAuditLog($"{DateTime.Now} {product.Name} {product.Slot} {product.Price,0:C} {Balance,0:C}");
                return product.OutPutMessage;
            }
        }
        public bool FinishTransaction()
        {
            decimal previousBalance = Balance;
            string changeCount = GetChange();

            Console.WriteLine("You are getting back a " + Balance + " in change in the following coin amounts: \n" +
                changeCount);
            
            WriteToAuditLog($"{DateTime.Now} GIVE CHANGE: {previousBalance,0:C} {Balance,0:C}");

            return true;
        }
        public void GetBalance(int deposit)
        {
            Balance += deposit;
            WriteToAuditLog($"{DateTime.Now} FEED MONEY: {deposit,0:C} {Balance,0:C}");
        }
        public string GetChange()
        {
            //Gets the change in the lowest coins possible
            decimal quarter = 0.25M;
            decimal dime = 0.10M;
            decimal nickel = 0.05M;
            decimal penny = 0.01M;

            int amountOfCoins = 0;

            decimal originalBalance = Balance;

            string coinCount = "";

            if (Balance / quarter >= 1)
            {
                amountOfCoins = (int)(Balance / quarter);
                coinCount += "Quarters: " + amountOfCoins;
                Balance -= (int)(Balance / quarter) * quarter;
            }
            if (Balance / dime >= 1)
            {
                amountOfCoins = (int)(Balance / dime);
                coinCount = coinCount + " Dimes: " + amountOfCoins;
                Balance -= (int)(Balance / dime) * dime;
            }
            if (Balance / nickel >= 1)
            {
                amountOfCoins = (int)(Balance / nickel);
                coinCount = coinCount + " Nickels: " + amountOfCoins;
                Balance -= (int)(Balance / nickel) * nickel;

            }

            if (Balance / penny >= 1)
            {
                amountOfCoins = (int)(Balance / penny);
                coinCount = coinCount + " Pennies: " + amountOfCoins;
                Balance -= (int)(Balance / penny) * penny;
            }

            return coinCount;
        }
        public void DisplayItems()
        {
            string slot = "Slot";
            string name = "Name";
            string purchasePrice = "Purchase Price";
            Console.WriteLine($"{slot}".PadLeft(3) + $"{name,0}".PadRight(22) + $"{purchasePrice}");
            foreach (KeyValuePair<string, ProductInventory> product in ProductList)
            {
                if (product.Value.AmountOfProduct == 0)
                {
                    Console.WriteLine("SOLD OUT");
                    continue;
                }
                Console.WriteLine($"{product.Value.Slot}".PadLeft(3) + $"{product.Value.Name,0}".PadRight(22) + $" {product.Value.Price,0:c}");

            }
        }
        public void CreateUpdateVendingMachineInventory()
        {
            using (StreamReader sr = new StreamReader(INVENTORY_LIST_PATH))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] productArray = line.Split("|");
                    string type = productArray[3];
                    decimal price = decimal.Parse(productArray[2]);
                    string name = productArray[1];
                    string slot = productArray[0];
                    endOfDaySalesReport[name] = 0;

                    switch (type)
                    {
                        case "Chip":
                            ProductList[slot] = new ProductInventory(slot, name, price, "Crunch Crunch, Yum!");
                            break;
                        case "Gum":
                            ProductList[slot] = new ProductInventory(slot, name, price, "Chew Chew, Yum!");
                            break;
                        case "Drink":
                            ProductList[slot] = new ProductInventory(slot, name, price, "Glug Glug, Yum!");
                            break;
                        case "Candy":
                            ProductList[slot] = new ProductInventory(slot, name, price, "Munch Munch, Yum!");
                            break;
                        default:
                            break;
                    }


                }
            }


        }
        public void WriteToAuditLog(string auditString)
        {
            using (StreamWriter sw = new StreamWriter(AUDIT_REPORT_LOG_PATH, true)) 
            {
                sw.WriteLine(auditString);
            }
        }
        public void WriteToTotalSalesReport()
        {
            using (StreamWriter sw = new StreamWriter(SALES_REPORT_LOG_PATH, false))
            {
                foreach (KeyValuePair<string, int> countProductSold in endOfDaySalesReport)
                {
                    sw.WriteLine(countProductSold.Key + " | " + countProductSold.Value);
                }
                sw.WriteLine($"\n **TOTAL SALES** {TotalSales,0:C}");
            }
        }
    }
}

    

