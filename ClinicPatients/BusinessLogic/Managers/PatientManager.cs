using Microsoft.Extensions.Configuration;
using NicolasEmpresa.BusinessLogic.Managers.Exceptions;
using NicolasEmpresa.BusinessLogic.Models;
using Serilog;
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
        private readonly IConfiguration _configuration;

        Dictionary<int, Patient> pacientes = new Dictionary<int, Patient>();
        public PatientManager(IConfiguration configuration)
        {

            _configuration = configuration;
            try
            {
                _filePath = _configuration.GetSection("Paths").GetSection("txt").Value;
            }
            catch (Exception e)
            {
                PatientExceptions bsEx = new PatientExceptions(e.Message);
                Log.Error(bsEx.MensajeParaLogs("Configurando direccion"));

                throw bsEx;
            }
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
            if (pacientes.TryGetValue(ci, out Patient paciente))
            {
                pacientes.Remove(ci);
                escribirPacientesEnArchivo();
            }
            else
            {
                PatientExceptions bsEx = new PatientExceptions();
                Log.Error(bsEx.MensajeParaLogs("Remove by Ci"));

                throw new Exception("error al remover CI!!!");
            }
        }

        public Patient obtenerPacienteCI(int ci)
        {
            try
            {
                return pacientes[ci];
            }
            catch (Exception e)
            {
                PatientExceptions bsEx = new PatientExceptions(e.Message);
                Log.Error(bsEx.MensajeParaLogs("obtenerPacienteCI"));

                throw bsEx;
            }

        }
        public void actualizarApellido(int ci, string apellido)
        {
            try
            {
                pacientes[ci].apellido = apellido;
                escribirPacientesEnArchivo();
            }
            catch(Exception e)
            {
                PatientExceptions bsEx = new PatientExceptions(e.Message);
                Log.Error(bsEx.MensajeParaLogs("actualizarApellido"));

                throw bsEx;
            }
        }
        public void actualizarNombre(int ci, string nombre)
        {
            try
            {
                pacientes[ci].nombre = nombre;
                escribirPacientesEnArchivo();
            }
            catch(Exception e)
            {
                PatientExceptions bsEx = new PatientExceptions(e.Message);
                Log.Error(bsEx.MensajeParaLogs("actualizarNombre"));

                throw bsEx;
            }

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
