/* 
 * INB270 - ATM Simulation Assignment
 * Simulates an ATM with 4 accounts and Balance, Withdraw and Transfer Functions.
 * 
 * Coder: Damon Jones
 * Student Number: N8857954
 * Date: August-September, 2013
 */
using System;

namespace ATM_Simulation
{
    class ATM
    {

        const int SAVING_ACCOUNT = 1;
        const int DEBIT_CARD = 2;
        const int CREDIT_CARD = 3;
        const int INVESTMENT_ACCOUNT = 4;
        const int MAINMENU = 0;
        const int ACCOUNTSMENU = 1;
        const int CANCELEXIT = 0;
        const int BALANCE = 1;
        const int WITHDRAWAL = 2;
        const int TRANSFER = 3;

        static double[] accountBalances = { 0.0, 1001.45, 850.0, -150.0, 10000.0 };

        static string[] accountNames = { "", "Savings Account", "Debit Card", 
                                           "Credit Card", "Investment Account" };
        static int numAccounts = accountNames.Length - 1;// '1' adjusts for the empty space in the array 

        static void Main()
        {
            int menu_choice;
            const int MAINMENUCHOICES = 3;
            do
            {
                DisplayMenu(MAINMENU);// Zero is main menu
                menu_choice = MenuOptions(MAINMENUCHOICES);
                if (menu_choice != CANCELEXIT)
                {
                    RunTransaction(menu_choice);
                }
            } while (menu_choice != CANCELEXIT);
            Exiting();
        } //end Main


        /// <summary>
        /// Displays Exit message
        /// Pre: True
        /// Post: Displays closing message and waits for user acknowledgement
        /// </summary>
        private static void Exiting()
        {
            AtmLogo();
            Console.WriteLine("\nThank-you for using a Dtech ATM!");
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadKey();
        }//End Exiting


        /// <summary>
        /// Displays Main Menus
        /// Pre: True
        /// Post: Dislays Main Menu
        /// </summary>
        static void DisplayMenu(int MenuNo)
        {
            string newMenu = "";
            AtmLogo();
            if (MenuNo == MAINMENU)
            {//Main Menu
                newMenu = "\n\t Transaction Menu"
                    + "\n\t------------------"
                    + "\n\t1. Balence Enquiry"
                    + "\n\t2. Withdrawal"
                    + "\n\t3. Transfer Funds";
            } else if (MenuNo >= ACCOUNTSMENU){
				//Displays Accounts List
                newMenu = "\n\t  Account List"
                     + "\n\t----------------"
                     + "\n\t1. Savings Account"
                     + "\n\t2. Debit Card"
                     + "\n\t3. Credit Card"
                     + "\n\t4. Investment Account";
            };
            newMenu = newMenu + "\n\t0. Cancel";
            switch (MenuNo)
            {
                case BALANCE:
                    newMenu = newMenu + "\n\nCheck which Account Balance: ";
                    break;
                case WITHDRAWAL:
                    newMenu = newMenu + "\n\nWithdraw from which Account: ";
                    break;
                case TRANSFER:
                    newMenu = newMenu + "\n\nTransfer from which Account: ";
                    break;
                default:
                    newMenu = newMenu + "\n\nSelect your Action: ";
                    break;
            }
            Console.Write(newMenu);
        }//end Displaynmenu


        /// <summary>
        /// Resets screen and displays machine header
        /// Pre: True
        /// Post: Clears screen and Displays ATM title|header
        /// </summary>
        static void AtmLogo()
        {
            Console.Clear();
            Console.WriteLine("\n\t******************"
                            + "\n\t* Dtech ATM #001 *"
                            + "\n\t******************");
        }//End AtmLogo


        /// <summary>
        /// Detects which numeric option a user chooses
        /// Pre: Number of options is greater than 0
        /// Post: Returns users choice between zero and the max
        /// </summary>
        /// <param name="Num_of_Options">Number of options on menu excluding exit</param>
        /// <returns>Numeric menu choice</returns>
        static int MenuOptions(int Num_options)
        {
            bool VaildOption = false;
            int option;
            do
            {
                option = int.Parse(Console.ReadLine());
                if ((0 <= option) && (option <= Num_options))
                {
                    VaildOption = true;
                } else 
                {
                    Console.Write("\nYou must Choose an option from 0 to {0}: ", Num_options);
                };
            } while (!VaildOption);
            return (option);
        }//end Menuoption


        /// <summary>
        /// Takes user yes or no answer and returns it as a bool. Can be skipped with parameter 0.
        /// Pre: True
        /// Post: Returns True for yes, False for no. 
        /// </summary>
        /// <param name="biPass">0~ false|skip, 1(defualt)~ user</param>
        /// <returns>True for yes</returns>
        static bool RepeatYesNo(int biPass = 1)
        {
            bool validAnwser = false;
            if (biPass != CANCELEXIT)
            {
                string response;
                do
                {
                    response = Console.ReadLine();
                    if ((response == "y") || (response == "Y"))
                    {
                        validAnwser = true;
                    }
                    else if ((response == "n") || (response == "N"))
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("\nY or N?");
                    }
                } while (!validAnwser);
            }
            else if (biPass == CANCELEXIT)
            {
                validAnwser = false;
            }
            else
            {
                Console.WriteLine("Error in RepeatYesNo Parameter");
            }
            return validAnwser;
        }//End RepeatYesNo


        /// <summary>
        /// Runs User's transaction of choice
        /// Pre: whichTrans is a number corresponding to an existing transaction type.
        /// Post: the appropiot operation will have been run and the results displayed.
        /// </summary>
        /// <param name="whichTrans">Which Transaction to Perform</param>
        static void RunTransaction(int whichTrans)
        {
            int choosenAccount;
            int askToRepeat = 1;
            do{
                DisplayMenu(whichTrans);
                choosenAccount = MenuOptions(numAccounts);
                if (choosenAccount < 1) {
                    Cancelled();
                    break;
                }
                switch (whichTrans)
                {
                    case BALANCE:
                        
                        AtmLogo();
                        DisplayBalance(choosenAccount);
                        Console.Write("\nCheck another Balance? (Y/N): ");
                        break;
                    case WITHDRAWAL:
                        WithdrawAmount(choosenAccount);
                        Console.Write("\nPerform Another Withdrawal? (Y/N): ");
                        break;
                    case TRANSFER:
                        Console.Write("\nTransfer To Which Account: ");
                        int toAccount = MenuOptions(numAccounts);
                        if (toAccount == CANCELEXIT)
                        {
                            Cancelled();
                            askToRepeat = CANCELEXIT;
                        }
                        else if (toAccount != choosenAccount) 
                        {
                            TransferAmount(choosenAccount, toAccount);
                            Console.Write("\nPerform Another Transfer? (Y/N): ");
                        }
                        else
                        {
                            AtmLogo();
                            Console.WriteLine("\nTransaction Cancelled:"
                                + "\n - An Account May Not Transfer to itself."
                                + "\n\nPress Any Key to Continue.");
                            askToRepeat = CANCELEXIT;
                            Console.ReadKey();
                        }
                        break;
                    default:
                        Console.WriteLine("Error: No transaction type not found!");
                        break;
            }}while(RepeatYesNo(askToRepeat));
        }//End RunTransaction


        /// <summary>
        /// Displays Cancelled by User Screen
        /// pre: true
        /// post: Cancellation Message Displayed
        /// </summary>
        private static void Cancelled()
        {
            AtmLogo();
            Console.WriteLine("\nTransaction Cancelled By User." +
                "\n\nPress any key to continue...");
            Console.ReadKey();
        }// End Cancelled


        /// <summary>
        /// Displays balance of "whichAccount"
        /// pre:  whichAccount is either 1, 2, 3 or 4
        /// post: balance of "whichAccount" has been displayed
        /// </summary>
        /// <param name="whichAccount"> the account whose balance is to be displayed</param>
        static void DisplayBalance(int whichAccount)
        {
            string output = "\n\t Account Balance "
                + "\n\t============================="
                + "\n\t| Time: " + DateTime.Now
                + "\n\t|\n\t| Account: " + accountNames[whichAccount]
                + "\n\t|\n\t| Balance: $" + accountBalances[whichAccount]
                + "\n\t|";
            Console.WriteLine(output);
        }//end DisplayBalance


        /// <summary>
        /// Dispenses the specified amount in the minimum number of $20 and $50 dollar notes
        /// pre: amount is as a combination of $20 and $50 only.
        /// post minimum number of notes has been dispensed.
        /// </summary>
        /// <param name="amount">the amount to be dispensed</param>
        static void DispenseCash(double amount)
        {
            int requestedAmount = int.Parse(amount.ToString());
            int modFifty = requestedAmount % 50;
            int fiftys;
            int twentys;
            if (modFifty % 20 == 0)
            {//Numbers made entirely of $20 and or an even number of $20 notes
                fiftys = requestedAmount / 50;
                twentys = modFifty / 20;
                Console.WriteLine("\nDispence {0} $50 Notes and {1} $20 Notes.", fiftys, twentys);
            }
            else
            {//Numbers that require an odd number of $20 notes
                fiftys = (requestedAmount / 50) - 1; // swap $50 so the result can be odd 10 intervals
                twentys = (modFifty + 50) / 20;
                Console.WriteLine("\nDispence {0} $50 Notes and {1} $20 Notes.", fiftys, twentys);
            }
        }//end DispenseCash


        /// <summary>
        /// Provided that the withdrawal amount does not leave whichAccount with less than the specified 
        /// minimum balance, the aount is withdrawn from whichAccount and if whichAccount is the Investment
        /// Account an additional fee of $1 is deducted.
        /// 
        /// pre: whichAccount is a valid account
        /// post: either acceptable amount is withdrawn from whichAccount or no withdrawal is performed
        /// </summary>
        /// <param name="whichAccount"> the account form which an amount is to be withdrawn</param>
        static void WithdrawAmount(int whichAccount)
        {
            bool validAmount = false;
            int amount;
            int investmentAccountFee = 1;
            do
            {
                Console.Write("Please enter ammount: $");
                amount = int.Parse(Console.ReadLine());
                if (amount == CANCELEXIT)
                {
                    break;
                }
                else if (((amount % 50 % 20) == 0) || ((((amount - 50) % 50) + 50) % 20) == 0) {
                    // amount must be constructable from 50|20 $notes
                    validAmount = true;
                }
                else
                {
                    Console.WriteLine("\nSorry, This Machine Cant dispence ${0}", amount);
                }
            } while (!validAmount);
            if ((validAmount) && FundsOutAllowed(whichAccount, amount, WITHDRAWAL))
            {
                accountBalances[whichAccount] = accountBalances[whichAccount] - amount;
                if (whichAccount == INVESTMENT_ACCOUNT)
                {
                    accountBalances[whichAccount] = accountBalances[whichAccount] - investmentAccountFee;
                }
                
                AtmLogo();
                DisplayBalance(whichAccount);
                DispenseCash(amount);
            }
        }//end WithdrawAmount


        /// <summary>
        /// Returns a Boolean value based off if the request amount can be released form the account.
        /// pre: whichAccount, amount, withOrTrans are resptively a valid account, amount and TransactionType
        /// post: true is return if the conditions are meet otherwise false
        /// </summary>
        /// <param name="whichAccount">Account Number</param>
        /// <param name="amount">Requested Funds|Amount</param>
        /// <param name="withOrTrans">Which Transaction type: WITHDRAWAL or TRANSFER</param>
        /// <returns>true|false</returns>
        static bool FundsOutAllowed(int whichAccount, double amount, int withOrTrans)
        {
            int minimumBalance;
            if (withOrTrans == WITHDRAWAL)
            {
                minimumBalance = 1;
            }
            else
            {
                minimumBalance = 0;
            }
            int minimumCreditBalance = -2000;
            if ((whichAccount != CREDIT_CARD) && ((accountBalances[whichAccount] - amount) >= minimumBalance))
            {
                return true;
            }
            else if ((whichAccount == CREDIT_CARD) && ((accountBalances[whichAccount] - amount) >= minimumCreditBalance))
            {
                return true;
            }
            else if (whichAccount != CREDIT_CARD)
            {
                Console.WriteLine("\nSorry but this Transaction would exceed the Minimum Balance for your account.");
                return false;
            }
            else
            {
                Console.WriteLine("\nSorry but this Transaction would exceed the Maximum Balance for your CreditCard.");
                return false;
            }
        }// End FundsAllowedOut


        /// <summary>
        ///  providing fromAccount has sufficient funds, an amount of money is transferred
        ///  to toAccount
        ///  pre: fromAccount and to account are different  valid accounts
        ///  post: if fromAccount has sufficient funds a transfer has occurred 
        ///         else no transfer occurred
        /// </summary>
        /// <param name="fromAccount">account from which money is to withdrawn</param>
        /// <param name="toAccount">account to which money is to be transferred</param>
        static void TransferAmount(int fromAccount, int toAccount)
        {
            double amount;
            bool positiveAmount = false;
            do
            {
                Console.Write("\nPlease enter ammount: $");
                amount = double.Parse(Console.ReadLine());
                if (amount == CANCELEXIT)
                {
                    Cancelled();
                    break;
                }
                else if ((amount > 0.0) && (FundsOutAllowed(fromAccount, amount, TRANSFER)))
                {
                    accountBalances[fromAccount] = Math.Round(accountBalances[fromAccount] - amount,2);
                    accountBalances[toAccount] = Math.Round(accountBalances[toAccount] + amount, 2);
                    //Math.Round counteracts errors assosiated with Decimal numbers in binary.
                    AtmLogo();
                    DisplayBalance(fromAccount);
                    DisplayBalance(toAccount);
                    positiveAmount = true;
                }
                else if (amount < 0.0)
                {
                    Console.WriteLine("\nPositive Numbers only Please.");
                }
            } while (!positiveAmount);
        }//end TransferAmount

    }
}
