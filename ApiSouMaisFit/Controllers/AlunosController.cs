using ApiSouMaisFit.Models;
using ApiSouMaisFit.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSouMaisFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar alunos!");
            }
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByNome(string nome)
        {
            try
            {
                var aluno = await _alunoService.GetAlunosByNome(nome);
                if (aluno == null)
                    return NotFound($"Não foi possível encontrar nenhum aluno com o nome {nome}");

                return Ok(aluno);
            }catch
            {
                return BadRequest("Request Inválida!");
            }
        }

        [HttpGet("{id:int}", Name ="GetAlunos")]
        public async Task<ActionResult<Aluno>> GetAlunosById(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAlunosById(id);
                if (aluno == null)
                    return NotFound($"Não foi possível encontrar um aluno com id {id}");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Erro ao efetuar buscar!");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAluno (Aluno aluno)
        {
            try
            {
                await _alunoService.AddAluno(aluno);
                return CreatedAtRoute(nameof(GetAlunos), new { id = aluno.Id }, aluno);
            } 
            catch {
                return BadRequest("Request Inválido");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAluno(int id, [FromBody] Aluno aluno)
        {   
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"O Aluno de id {id} foi atualizado com sucesso!");
                }
                else
                    return BadRequest("Dados Inconsistentes!");
                
                
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
    }
}
