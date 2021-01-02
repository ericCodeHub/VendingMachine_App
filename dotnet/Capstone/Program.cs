using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;



namespace Capstone
{
    public class Program
    {
        private const string MAIN_MENU_OPTION_DISPLAY_ITEMS = "Display Vending Machine Items";
        private const string MAIN_MENU_OPTION_PURCHASE = "Purchase";
        private const string MAIN_MENU_OPTION_EXIT = "Exit";
        private const string MAIN_MENU_OPTION_SALES_REPORT = "Hidden Option";
        private readonly string[] MAIN_MENU_OPTIONS = { MAIN_MENU_OPTION_DISPLAY_ITEMS, MAIN_MENU_OPTION_PURCHASE, MAIN_MENU_OPTION_EXIT, MAIN_MENU_OPTION_SALES_REPORT }; //const has to be known at compile time, the array initializer is not const in C#


        private const string PURCHASE_MENU_OPTION_UPDATEBALANCE = "Add Money To Purchase Items(1, 2, 5, 10, 20)";
        private const string PURCHASE_MENU_ITEM = "Select An Item To Purchase";
        private const string PURCHASE_MENU_OPTION_FINISH_TRANSACTION = "Finish Transaction";
        private readonly string[] PURCHASE_MENU_OPTIONS = { PURCHASE_MENU_OPTION_UPDATEBALANCE, PURCHASE_MENU_ITEM, PURCHASE_MENU_OPTION_FINISH_TRANSACTION };
        private readonly IBasicUserInterface ui = new MenuDrivenCLI();

        private readonly string[] ARRAY_OF_SLOTS = {"0", "D4", "D3", "D2", "D1", "C4", "C3", "C2", "C1", "B4", "B3", "B2", "B1", "A4", "A3", "A2", "A1" };

        public VendingMachine vm = new VendingMachine();
        string machineTopAndBottom = new string('-', 30);//can be used to separate content after menu selections

        static void Main(string[] args)
        {
            Program p = new Program();
            
            p.Run();
        }


        public void Run()
        {
            vm.CreateUpdateVendingMachineInventory();
            bool exit = false;
            while (!exit) //run this is an infinite loop. You'll need a 'finished' option and then you'll break after that option is selected
            {
                string selection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);
                if (selection == MAIN_MENU_OPTION_DISPLAY_ITEMS)//presses 1 to display the items
                {
                    vm.DisplayItems();
                }
                else if (selection == MAIN_MENU_OPTION_PURCHASE)//presses 2 to purchase
                {                    
                        PurchaseMenu();                    
                }
                else if (selection == MAIN_MENU_OPTION_EXIT)//presses 3 to Exit
                {
                    exit = true;
                }
                else if (selection == MAIN_MENU_OPTION_SALES_REPORT)//presses 4 to purchase
                {
                    vm.WriteToTotalSalesReport();
                }
            }
        }

        private void PurchaseMenu()
        {
            bool isTransactionFinished = false;
            while (!isTransactionFinished)
            {
                Console.WriteLine($"\n{machineTopAndBottom}");
                Console.WriteLine($"Balance: {vm.Balance,0:C}");
                string selection = (string)ui.PromptForSelection(PURCHASE_MENU_OPTIONS);
                if (selection == PURCHASE_MENU_OPTION_UPDATEBALANCE)
                {
                    Console.WriteLine($"{selection}");

                    string[] wholeDollarAmount = { "1", "2", "5", "10", "20" };

                    object depositCheck = null;
                    while (depositCheck == null)
                    {
                        depositCheck = ui.GetChoiceFromUserInput(wholeDollarAmount);//GetChoiceFromUserInput will make sure amount is valid
                    }

                    int deposit = int.Parse(depositCheck.ToString());
                    vm.DepositFunds(deposit);
                }
                else if (selection == PURCHASE_MENU_ITEM)
                {

                    if (vm.Balance == 0)
                    {
                        Console.WriteLine("please deposit funds using menu option 1");
                        isTransactionFinished = false;
                    }
                    else
                    {
                        
                        Console.WriteLine($"{selection} or enter 0 to cancel");

                        object validSlotCheck = "";
                        while (validSlotCheck == null || (object)validSlotCheck == "")
                        {
                            validSlotCheck = ui.GetChoiceFromUserInput(ARRAY_OF_SLOTS);//GetChoiceFromUserInput will make sure product slot selected is valid
                        }

                        if (validSlotCheck.ToString() == "0")
                        {
                            isTransactionFinished = false;
                        }
                        else
                        {
                            string input = validSlotCheck.ToString().ToUpper();

                            Console.WriteLine(vm.PurchaseProduct(input));
                        }
                    
                    }

                }
                else if (selection == PURCHASE_MENU_OPTION_FINISH_TRANSACTION)
                {
                    isTransactionFinished = vm.FinishTransaction(machineTopAndBottom);
                }
            }
        }

    }
}
