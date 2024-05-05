using Microsoft.AspNetCore.Mvc;
using NicolasEmpresa.BusinessLogic.Managers;
using NicolasEmpresa.BusinessLogic.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicPatients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientManager _patientManager ;
        public PatientsController(PatientManager patientManager)
        {
            _patientManager = patientManager;
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return _patientManager.devolverPacientes().Values;
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public Patient Get(int id)
        {
            return _patientManager.obtenerPacienteCI(id);
        }

        // POST api/<PatientsController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            _patientManager.añadirPaciente(value);
        }

        // PUT api/<PatientsController>
        [HttpPut("nombre/{id}")]
        public void PutName(int id, string nombre)
        {
            _patientManager.actualizarNombre(id, nombre);
        }

        [HttpPut("apellido/{id}")]
        public void PutLastName(int id, string apellido)
        {
            _patientManager.actualizarApellido(id, apellido);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _patientManager.removerPaciente(id);
        }
    }
}
