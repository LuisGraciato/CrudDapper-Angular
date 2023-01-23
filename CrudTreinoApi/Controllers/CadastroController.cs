using CrudTreinoApi.Models;
using CrudTreinoApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CrudTreinoApi.Controllers;
[ApiController]
[Route("api/[controller]")]
 public class CadastroController : ControllerBase
    {
        private readonly ICadastroRepository _repository;
        public CadastroController(ICadastroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var informacoes = await _repository.BuscaCadastrosAsync();

            return informacoes.Any()
                ? Ok(informacoes)
                : NoContent();
        }

        [HttpGet("ContactID")]
        public async Task<IActionResult> Get(int contactid)
        {

            var informacao = await _repository.BuscaCadastroAsync(contactid);

            return informacao != null
                ? Ok(informacao)
                : NotFound("Cadastro nao encontrado");
        }

    [HttpPost]
    public async Task<IActionResult> Post(CadastroRequest request)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty (request.Mobile) || string.IsNullOrEmpty(request.Address))
        {
            return BadRequest("Cadastro invalido");
        }

        var adicionado = await _repository.AdicionaAsync(request);

        return adicionado
            ? Ok("Cadastro adicionadao com sucesso")
            : BadRequest("Erro ao adionar Cadastro");
    }
    
    [HttpPut("ContactID")]
    public async Task<IActionResult> Put(CadastroRequest request, int contactid)
    {
        if (contactid <= 0) return BadRequest("Cadastro Invalido");

        var informacao = await _repository.BuscaCadastroAsync(contactid);

        if (informacao == null) return NotFound("Cadastro não Existe");

        if (string.IsNullOrEmpty(request.Name)) request.Name = informacao.Name;
        if (string.IsNullOrEmpty(request.Mobile)) request.Mobile = informacao.Mobile;
        if (string.IsNullOrEmpty(request.Address)) request.Address = informacao.Address;
        
        var atualizado = await _repository.AtualizarAsync(request, contactid);

        return atualizado
           ? Ok("Cadastro atualizado com sucesso")
           : BadRequest("Erro ao atualizar cadastro");
    }

    [HttpDelete("ContactID")] 
    public async Task<IActionResult> Delete(int contactid)
    {
        if (contactid <= 0) return BadRequest("Cadastro Invalido"); 

        var informacao = await _repository.BuscaCadastroAsync(contactid);

        if (informacao == null) return NotFound("Cadastro não Existe"); 

        var deletado = await _repository.DeletarAsync(contactid);

        return deletado
          ? Ok("Cadastro deletado com sucesso")
          : BadRequest("Erro ao deletar cadastro");
    }
}



   