using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApp.Core.Models
{
    public class ConfirmPromptModel
    {
        public string Prompt { get; set; }
        public string? YesLabel { get; set; }
        public string? NoLabel { get; set; }
    }
}
