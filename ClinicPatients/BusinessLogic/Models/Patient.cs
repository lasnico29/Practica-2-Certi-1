using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicolasEmpresa.BusinessLogic.Models
{
    public class Patient
    {
        
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int Ci { get; set; }
        public string tipoSangre { get; set; }
        public Patient()
        {

        }
        public void GetRandomBloodGroup()
        {
            var bloodGroups = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            var random = new Random();
            tipoSangre = bloodGroups[random.Next(bloodGroups.Length)];
        }

    }
}

