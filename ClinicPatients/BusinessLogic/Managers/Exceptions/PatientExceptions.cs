using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicolasEmpresa.BusinessLogic.Managers.Exceptions
{
    internal class PatientExceptions : Exception
    {
        public PatientExceptions() { }
        public PatientExceptions(string message) : base(message) { }

        public string MensajeParaLogs(string method)
        {
            return $"{method} Exception: {Message}";
        }

    }
}
