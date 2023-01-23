using CrudTreinoApi.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CrudTreinoApi.Repository;

public class CadastroRepository : ICadastroRepository
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public CadastroRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("SqlConnection");
    }
    public async Task<IEnumerable<CadastroResponse>> BuscaCadastrosAsync()
    {
        string sql = @"SELECT 
	                        C.Contactid    Contactid,
	                        C.Name         Name,           
	                        C.Mobile       Mobile,
	                        C.Address      Address 
	                        FROM contact c";

        using var con = new SqlConnection(connectionString);
        return await con.QueryAsync<CadastroResponse>(sql);
    }
    public async Task<CadastroResponse> BuscaCadastroAsync(int contactid)
    {
        string sql = @"SELECT 
	                        c.contactid    contactid,
	                        c.name         Name,           
	                        c.mobile       Mobile,
	                        c.address      Address 
	                        FROM contact c
                            WHERE c.contactid = @contactid";
        using var con = new SqlConnection(connectionString);
        return await con.QueryFirstOrDefaultAsync<CadastroResponse>(sql, new { contactid = contactid });
    }
    public async Task<bool> AdicionaAsync(CadastroRequest request)
    {
        string sql = @"INSERT INTO Contact(Name, Mobile, Address)
                        VALUES (@Name, @Mobile, @address)";

        using var con = new SqlConnection(connectionString);
        return await con.ExecuteAsync(sql, request) > 0;

    }

    public async Task<bool> AtualizarAsync(CadastroRequest request, int contactid)
    {
        string sql = @"UPDATE Contact SET
                            Name = @Name,
                            Mobile = @Mobile,
                            Address = @Address
                        WHERE contactid = @contactId";

        var parametros = new DynamicParameters();
        parametros.Add("Name", request.Name);
        parametros.Add("Mobile", request.Mobile);
        parametros.Add("Address", request.Address);
        parametros.Add("ContactId", contactid);

        using var con = new SqlConnection(connectionString);
        return await con.ExecuteAsync(sql, parametros) > 0;

    }

    public async Task<bool> DeletarAsync(int contactid)
    {
        string sql = @"DELETE FROM contact
                        WHERE contactid = @contactId";

        using var con = new SqlConnection(connectionString); 
        return await con.ExecuteAsync(sql, new { contactid = contactid }) > 0;
    }
}


