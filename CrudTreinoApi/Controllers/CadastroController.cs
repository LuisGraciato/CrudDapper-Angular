using CrudTreinoApi.Models;
using CrudTreinoApi.Repository;
using CrudTreinoApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CrudTreinoApi.Controllers;
[ApiController]
[Route("api/[controller]")]
 public class CadastroController : ControllerBase
    {
        private readonly ICadastroService _service;
        public CadastroController(ICadastroService service)
        {
            _service= service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var informacoes = await _service.BuscaCadastrosAsync();

            return informacoes.Any()
                ? Ok(informacoes)
                : NoContent();
        }

        [HttpGet("ContactID")]
        public async Task<IActionResult> Get(int contactid)
        {

            var informacao = await _service.BuscaCadastroAsync(contactid);

            return informacao != null
                ? Ok(informacao)
                : NotFound("Cadastro nao encontrado teste");
        }

    [HttpPost]
    public async Task<IActionResult> Post(CadastroRequest request)
    {
        try
        {
            var adicionado = await _service.AdicionaAsync(request);

            return adicionado
           ? Ok("Cadastro adicionadao com sucesso")
           : BadRequest("Erro ao adionar Cadastro");
        }
        catch (Exception err)
        {

            return BadRequest(err.Message);
        }

    }
    
    [HttpPut("ContactID")]
    public async Task<IActionResult> Put(CadastroRequest request, int contactid)
    {
        if (contactid <= 0) return BadRequest("Cadastro Invalido");

        var informacao = await _service.BuscaCadastroAsync(contactid);

        if (informacao == null) return NotFound("Cadastro não Existe");

        if (string.IsNullOrEmpty(request.Name)) request.Name = informacao.Name;
        if (string.IsNullOrEmpty(request.Mobile)) request.Mobile = informacao.Mobile;
        if (string.IsNullOrEmpty(request.Address)) request.Address = informacao.Address;
        
        var atualizado = await _service.AtualizarAsync(request, contactid);

        return atualizado
           ? Ok("Cadastro atualizado com sucesso")
           : BadRequest("Erro ao atualizar cadastro");
    }

    [HttpDelete("ContactID")] 
    public async Task<IActionResult> Delete(int contactid)
    {
        if (contactid <= 0) return BadRequest("Cadastro Invalido"); 

        var informacao = await _service.BuscaCadastroAsync(contactid);

        if (informacao == null) return NotFound("Cadastro não Existe"); 

        var deletado = await _service.DeletarAsync(contactid);

        return deletado
          ? Ok("Cadastro deletado com sucesso")
          : BadRequest("Erro ao deletar cadastro");
    }
}



   