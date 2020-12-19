using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone
{
    public class MenuDrivenCLI : IBasicUserInterface
    {
        public void Output(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }

        public void PauseOutput()
        {
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays the options and prompts the user to select until a valid option is selected.
        /// </summary>
        /// <param name="options">Everything is an object so an array of anything is ok. But it must have ToString</param>
        /// <returns></returns>
        public Object PromptForSelection(Object[] options)
        {
            Object choice = null;
            while(choice==null)
            {
                DisplayMenuOptions(options);
                choice = GetChoiceFromUserInput(options);
            }

            return choice;
        }

        /// <summary>
        /// Reads in what the user entered from the console. Returns null for an invalid selection.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>the object selected or null</returns>
        public Object GetChoiceFromUserInput(Object[] options)
        {
            Object choice = null;
            String userInput = Console.ReadLine();
            try
            {
                
                if (options.Length > 4)
                {
                    choice = (options.Contains(userInput.ToUpper())) ? userInput: null;
                }
                else if (int.Parse(userInput) > 0 && int.Parse(userInput) <= options.Length)
                {
                    choice = options[int.Parse(userInput) - 1];
                }
            }
            catch (Exception e)
            {
                // eat the exception, an error message will be displayed below since choice will be null
            }
            if (choice == null)
            {
			    Console.WriteLine("\n*** " + userInput + " is not a valid option ***\n");
            }
            return choice;
        }

        /// <summary>
        /// Display all of the menu options with option numbers starting at 1
        /// </summary>
        /// <param name="options">The options to display. Must have ToString overriden or be strings</param>
        private void DisplayMenuOptions(Object[] options)
        {

            Console.WriteLine();
            for (int i = 0; i < options.Length; i++)
            {
                if (i == 3)
                    continue;
                int optionNum = i + 1;
                Console.WriteLine(optionNum + ") " + options[i]);
            }
            Console.WriteLine("Please choose an option >>>");
        }

    }
}
