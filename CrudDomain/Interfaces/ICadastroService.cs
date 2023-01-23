using CrudTreinoApi.Models;

namespace CrudTreinoApi.Service;

    public interface ICadastroService
    {
        Task<IEnumerable<CadastroResponse>> BuscaCadastrosAsync();
        Task<CadastroResponse> BuscaCadastroAsync(int contactid);
        Task<bool> AdicionaAsync(CadastroRequest request);
        Task<bool> AtualizarAsync(CadastroRequest request, int contactid);
        Task<bool> DeletarAsync(int contactid);
    }

