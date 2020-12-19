using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public interface IBasicUserInterface
    {
        void Output(string message);

        void PauseOutput();

        Object PromptForSelection(Object[] options);

        Object GetChoiceFromUserInput(Object[] options);
    }
}
