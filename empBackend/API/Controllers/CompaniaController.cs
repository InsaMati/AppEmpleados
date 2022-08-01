using Core.DTO;
using Core.Entidades;
using Infraestructura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private ResponseDto _response;

        public CompaniaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._response = new ResponseDto();
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<compania>>> GetCompanias()
        {
            var lista = await _dbContext.Compania.ToListAsync();

            _response.Resultado = lista;
            _response.Mensaje = "Listado de Cliente";

            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetCompania")]

        public async Task<ActionResult<compania>> GetCompania(int id)
        {
            var comp = await _dbContext.Compania.FindAsync(id);

            if (comp == null)
            {
                _response.IsExitoso = false;
                _response.Mensaje = "Id nulo";
                return BadRequest(_response);
            }

            _response.Resultado = comp;
            _response.Mensaje = "Datos de la compañia: " + comp.Id;

            return Ok(_response); // Status code = 200
        }

        [HttpPost]

        public async Task<ActionResult<compania>> PostCompania([FromBody] compania compania)
        {
            await _dbContext.Compania.AddAsync(compania);
            await _dbContext.SaveChangesAsync();
            return CreatedAtRoute("GetCompania", new { id = compania.Id }, compania); // Status code = 201
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutCompania(int id, [FromBody] compania compania)
        {
            if (id != compania.Id)
            {
                return BadRequest("Id de compania no coincide");
            }

            _dbContext.Update(compania);
            await _dbContext.SaveChangesAsync();

            return Ok(compania);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCompania(int id)
        {

            var compania = await _dbContext.Compania.FindAsync(id);

            if (compania == null)
            {
                return NotFound();
            }

            _dbContext.Remove(compania);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }



    }
}