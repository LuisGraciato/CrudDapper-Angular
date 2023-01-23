using CrudTreinoApi.Models;
using CrudTreinoApi.Service;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudTreinoApi.Repository;

namespace CrudService.Service
{
    public class CadastroService : ICadastroService
    {
        private readonly ICadastroRepository _repository;
        public CadastroService(ICadastroRepository repository)
        {
            _repository = repository;
        }
        public Task<bool> AdicionaAsync(CadastroRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Mobile) || string.IsNullOrEmpty(request.Address))
                {
                    throw new Exception("Cadastro invalido");
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Task<bool> AtualizarAsync(CadastroRequest request, int contactid)
        {
            throw new NotImplementedException();
        }

        public Task<CadastroResponse> BuscaCadastroAsync(int contactid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CadastroResponse>> BuscaCadastrosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarAsync(int contactid)
        {
            throw new NotImplementedException();
        }
    }
}
