using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Infraestructura.Data;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<EmpleadoController> _Logger;
        private readonly IMapper _mapper;
        public EmpleadoController(IUnitOfWork UnitOfWork,ILogger<EmpleadoController> logger, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            this._Logger = logger;
            this._mapper = mapper;
            this._response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<EmpleadoReadDto>>> GetEmpleados()
        {
            _Logger.LogInformation("Listado de Empleados.");
            var lista = await _unitOfWork.empleado.ObtenerTodos(incluirPropiedades: "compania");
            _response.Resultado = _mapper.Map<IEnumerable<empleado>, IEnumerable<EmpleadoReadDto>>(lista);

            _response.Mensaje = "Listado de Empleados";

            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetEmpleado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpleadoReadDto>> GetEmpleado(int id)
        {
            if(id==0){
                _Logger.LogError("Debe enviar el ID.");
                _response.IsExitoso = false;
                _response.Mensaje = "Debe enviar el ID.";
                return BadRequest(_response);
            }

            var emp = await _unitOfWork.empleado.ObtenerPrimero(x => x.Id == id,incluirPropiedades: "compania");
           
            if (emp == null)
            {
                _Logger.LogError("El empleado no existe.");
                _response.IsExitoso = false;
                _response.Mensaje = "El empleado no existe.";
                return NotFound(_response);
            }

            _response.Resultado = _mapper.Map<empleado,EmpleadoReadDto>(emp);
            _response.Mensaje = "Datos del empleado: " + emp.Id;

            return Ok(_response); // Status code = 200
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<compania>> PostEmpleado([FromBody] EmpleadoUpsertDto empleadoDTO)
        {
            if(empleadoDTO == null){
                _Logger.LogError("La informacion es incorrecta.");
                _response.Mensaje = "Informacion incorrecta.";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var empleadoExiste = await _unitOfWork.empleado.ObtenerPrimero(c => c.Nombres.ToLower() == empleadoDTO.Nombres.ToLower()
                                                && c.Apellidos.ToLower() == empleadoDTO.Apellidos.ToLower());

            if(empleadoExiste!=null){

                ModelState.AddModelError("NombreDuplicado","El nombre/apellido del empleado ya existe.");
                return BadRequest(ModelState);
            }

            empleado emp = _mapper.Map<empleado>(empleadoDTO);

            await _unitOfWork.empleado.Agregar(emp);
            await  _unitOfWork.Guardar();
            return CreatedAtRoute("GetCompania", new { id = emp.Id }, emp); // Status code = 201
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutEmpleado(int id, [FromBody] EmpleadoUpsertDto EmpleadoDto)
        {
            if (id != EmpleadoDto.Id)
            {
                return BadRequest("Id de empleado no coincide");
            }

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var empleadoExiste = await _unitOfWork.empleado.ObtenerPrimero(c => c.Nombres.ToLower() == EmpleadoDto.Nombres.ToLower() &&
                                                    c.Apellidos.ToLower() == EmpleadoDto.Apellidos.ToLower() &&
                                                    c.Id != EmpleadoDto.Id);

             if(empleadoExiste != null){
                ModelState.AddModelError("NombreDuplicado","El nombre del empleado ya existe");
                return BadRequest(ModelState);
             }

             empleado emp = _mapper.Map<empleado>(EmpleadoDto);

            _unitOfWork.empleado.Actualizar(emp);
            await _unitOfWork.Guardar();

            return Ok(emp);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteEmpleado(int id)
        {

            if(id==0){
                return BadRequest();
            }

            var compania = await _unitOfWork.empleado.ObtenerPrimero(c => c.Id == id);

            if (compania == null)
            {
                return NotFound();
            }

            _unitOfWork.empleado.Remover(compania);

            await _unitOfWork.Guardar();
            
            return NoContent();
        }

        [HttpGet]
        [Route("EmpleadosPorCompania/{companiaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        // El nombre del parametro tiene que ser igual al que esta en la ruta
        public async Task<ActionResult<IEnumerable<EmpleadoReadDto>>> GetEmpleadoCompania(int companiaId)
        {
            if(companiaId==0) return BadRequest();
            
            var Lista = await _unitOfWork.empleado.ObtenerTodos(e => e.CompaniaId == companiaId, incluirPropiedades: "compania");

            if(Lista == null) 
            {
                return NotFound();
            }

            _response.Resultado = _mapper.Map<IEnumerable<empleado>,IEnumerable<EmpleadoReadDto>>(Lista);
            _response.Mensaje = "Empleados de la compañia.";
            _response.IsExitoso = true;

            return Ok(_response);

        }



    }
}