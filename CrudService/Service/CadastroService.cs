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
        public async Task<bool> AdicionaAsync(CadastroRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))    
                {
                    throw new Exception(" Nome invalido");
                }
                if (string.IsNullOrEmpty(request.Mobile))
                {
                    throw new Exception("Numero INvalido");
                }
                if (string.IsNullOrEmpty(request.Address))
                {
                    throw new Exception("Endereço errado");
                }

                return await _repository.AdicionaAsync(request);

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<bool> AtualizarAsync(CadastroRequest request, int contactid)
        {
          
            try
            {
                if (contactid <= 0)
                {
                    throw new Exception("Cadastro Invalido");
                }
                var informacao = await _repository.BuscaCadastroAsync(contactid);

                if (informacao == null)
                {
                    throw new Exception("Cadastro não Existe");
                } 

                return await _repository.AtualizarAsync(request, contactid);
            }

            catch (Exception err)
            {

                throw err;
            }
           
        }

        public Task<CadastroResponse> BuscaCadastroAsync(int contactid)
        {
           
            return _repository.BuscaCadastroAsync(contactid);
        }

        public Task<IEnumerable<CadastroResponse>> BuscaCadastrosAsync()
        {
            return _repository.BuscaCadastrosAsync();
        }

        public async Task<bool> DeletarAsync(int contactid)
        {
            var informacao = await _repository.BuscaCadastroAsync(contactid);

            try
            {
                if (contactid <= 0)
                {
                   throw new Exception("Cadastro Invalido");
                }
                if (informacao == null)
                {
                    throw new Exception("Cadastro não Existe");
                }


                return await _repository.DeletarAsync(contactid);
            }
            catch (Exception err)
            {

                throw err;
            }
           
        }
    }
}
