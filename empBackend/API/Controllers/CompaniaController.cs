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
    public class CompaniaController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<CompaniaController> _Logger;
        private readonly IMapper _mapper;
        public CompaniaController(IUnitOfWork UnitOfWork,ILogger<CompaniaController> logger, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            this._Logger = logger;
            this._mapper = mapper;
            this._response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<compania>>> GetCompanias()
        {
            _Logger.LogInformation("Listado de compañias.");
            var lista = await _unitOfWork.compania.ObtenerTodos();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Compañias";

            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetCompania")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<compania>> GetCompania(int id)
        {

            if(id==0){
                _Logger.LogError("Debe enviar el ID.");
                _response.IsExitoso = false;
                _response.Mensaje = "Debe enviar el ID.";
                return BadRequest(_response);
            }

            var comp = await _unitOfWork.compania.ObtenerPrimero(c => c.Id == id);

            if (comp == null)
            {
                _Logger.LogError("La compañia no existe.");
                _response.IsExitoso = false;
                _response.Mensaje = "La compañia no existe.";
                return NotFound(_response);
            }

            _response.Resultado = comp;
            _response.Mensaje = "Datos de la compañia: " + comp.Id;

            return Ok(_response); // Status code = 200
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<compania>> PostCompania([FromBody] CompaniaDTO companiaDTO)
        {
            if(companiaDTO == null){
                _Logger.LogError("La informacion es incorrecta.");
                _response.Mensaje = "Informacion incorrecta.";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var companiaExiste = await _unitOfWork.compania.ObtenerPrimero(c => c.Nombre.ToLower() == companiaDTO.Nombre.ToLower());
 
            if(companiaExiste!=null){

                ModelState.AddModelError("NombreDuplicado","El nombre de la compañia ya existe.");
                return BadRequest(ModelState);
            }

            compania comp = _mapper.Map<compania>(companiaDTO);


            await _unitOfWork.compania.Agregar(comp);
            await _unitOfWork.Guardar();
            return CreatedAtRoute("GetCompania", new { id = comp.Id }, comp); // Status code = 201
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutCompania(int id, [FromBody] CompaniaDTO companiaDTO)
        {
            if (id != companiaDTO.Id)
            {
                return BadRequest("Id de compania no coincide");
            }

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var companiaExiste = await _unitOfWork.compania.ObtenerPrimero(c => c.Nombre.ToLower() == companiaDTO.Nombre.ToLower() && c.Id != companiaDTO.Id);

             if(companiaExiste != null){
                ModelState.AddModelError("NombreDuplicado","El nombre de la compañia ya existe");
                return BadRequest(ModelState);
             }

             compania comp = _mapper.Map<compania>(companiaDTO);

            _unitOfWork.compania.Actualizar(comp);
            await _unitOfWork.Guardar();

            return Ok(comp);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCompania(int id)
        {

            if(id==0){
                return BadRequest();
            }

            var compania = await _unitOfWork.compania.ObtenerPrimero(c => c.Id == id);

            if (compania == null)
            {
                return NotFound();
            }

            _unitOfWork.compania.Remover(compania);

            await _unitOfWork.Guardar();

            return NoContent();
        }



    }
}