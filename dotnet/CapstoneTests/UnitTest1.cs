using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CapstoneTests
{
    [TestClass]
    public class UnitTest1
    {
        //this would check if the productList has all the types
        //ensure that all types can be purchased: Gum, Drinks, Chips, Candy

        //Testing all the types 
        //create inventory file
        //create inventory Dictionary from file
        //check that Dictionary has items from inventory file

        //send product code to:
        //result goes to: 

        //string product =
        //Check each drink, gum, chip, and candy type

        //testing money inputs out of range
        //testing purchase and main menu options out of range
        //test different amounts of change
        //check customer balance
        //check total sales report
        //check audits report



        [TestMethod]
        public void TestPurchaseProduct() //testing slots out of range
        {
            //Assert.AreEqual()
            //initialize productList
            VendingMachine prodNumberTest = new VendingMachine();
            prodNumberTest.CreateUpdateVendingMachineInventory();
            prodNumberTest.Balance = 10;

            prodNumberTest.PurchaseProduct("A1");
            prodNumberTest.PurchaseProduct("A1");
            prodNumberTest.PurchaseProduct("A1");

            Assert.AreEqual(2, prodNumberTest.ProductList["A1"].AmountOfProduct);
            

        //inventory for A1 changed
        //balance has adjusted for A1's price
        }
        [TestMethod]
        public void TestInit()
        {
            VendingMachine vm = new VendingMachine();

            vm.CreateUpdateVendingMachineInventory();

            int result = vm.ProductList["A1"].AmountOfProduct;

            Assert.AreEqual(5, result);

            //the more things that you need to create for a test = coupling
        }

        [TestMethod]
        public void TestGetChange()
        {
            VendingMachine vm = new VendingMachine();
            vm.Balance = 25.00M;

            string finalChange = vm.GetChange();

            Assert.AreEqual("Quarters: 100", finalChange);
        }
    
        [TestMethod]
        public void TestSoldOut()
        {
            VendingMachine vm = new VendingMachine();
            vm.CreateUpdateVendingMachineInventory();

            vm.Balance = 30.00M;

            vm.PurchaseProduct("A1");
            vm.PurchaseProduct("A1");
            vm.PurchaseProduct("A1");
            vm.PurchaseProduct("A1");
            vm.PurchaseProduct("A1");

            Assert.AreEqual("SOLD OUT", vm.PurchaseProduct("A1"));
        }
        //test feedmoney

        [TestMethod]
        public void TestFeedMoney()
        {
            VendingMachine vm = new VendingMachine();
            vm.CreateUpdateVendingMachineInventory();

            vm.DepositFunds(2);
            vm.DepositFunds(1);
            vm.DepositFunds(10);

            Assert.AreEqual(13, vm.Balance);
        }
        //checks initial balance
        //deposit valid money and invalid money
        //check balance
        

        [TestMethod]
        public void TestBalanceAfterPurchase()
        {
            VendingMachine vm = new VendingMachine();
            vm.CreateUpdateVendingMachineInventory();
            vm.Balance = 10;

            vm.PurchaseProduct("A1"); //3.05
            decimal result = vm.Balance;
            decimal expected = 6.95M;
            Assert.AreEqual(expected, result);
        }
        //test purchased item sets up balance
        //
    }
}
