using NicolasEmpresa.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicolasEmpresa.BusinessLogic.Managers
{
    public class PatientManager
    {
        private readonly string _filePath;
        private readonly string _fileLog;

        Dictionary<int, Patient> pacientes = new Dictionary<int, Patient>();
        public PatientManager()
        {
            _filePath = "C:\\NICO TODA MI INFORMACION 2\\UPB\\CERTIFICACION I\\PRACTICE-2-CERTI-I\\listaPacienteQA.txt";

            ObtenerPacientesDeArchivo();

        }
        public void añadirParametros(string nombre, string apellido, int ci)
        {
            pacientes.Add(ci, new Patient()
            {
                nombre = nombre,
                apellido = apellido,
                Ci = ci,

            }
            );
            escribirPacientesEnArchivo();
        }
        public void añadirPaciente(Patient paciente)
        {
            paciente.GetRandomBloodGroup();
            pacientes.Add(paciente.Ci, paciente);
            escribirPacientesEnArchivo();
        }
        public void removerPaciente(int ci)
        {

            pacientes.Remove(ci);
            escribirPacientesEnArchivo() ;

        }

        public Patient obtenerPacienteCI(int ci)
        {
          
            return pacientes[ci];


        }
        public void actualizarApellido(int ci, string apellido)
        {
            
            pacientes[ci].apellido = apellido;
            escribirPacientesEnArchivo();
        }
        public void actualizarNombre(int ci, string nombre)
        {
            
            pacientes[ci].nombre = nombre;
            escribirPacientesEnArchivo();

        }
        public Dictionary<int, Patient> devolverPacientes()
        {
            return pacientes;
        }

        public void ObtenerPacientesDeArchivo()
        {

            try
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var patient = new Patient()
                    {
                        nombre = parts[0],
                        apellido = parts[1],
                        Ci = int.Parse(parts[2]),
                        tipoSangre = parts[3],
                    };
                    pacientes.Add(int.Parse(parts[2]), patient);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void escribirPacientesEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(_filePath, false)) // El segundo parámetro 'false' significa que sobreescribirá el archivo si existe
            {
                foreach (Patient paciente in pacientes.Values)
                {
                    string linea = $"{paciente.nombre},{paciente.apellido},{paciente.Ci},{paciente.tipoSangre}";
                    writer.WriteLine(linea);
                }
            }
        }
    }
}
