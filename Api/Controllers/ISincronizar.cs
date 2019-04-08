using System.Collections.Generic;
using System.Web.Http.Results;
using Conexao;

namespace Api.Controllers
{
    public interface ISincronizar
    {

        JsonResult<Sincronizar> GetProduto(string Ean);
        JsonResult<Sincronizar> GetProdutoSinc(string ordem);
        JsonResult<ICollection<Sincronizar>> GetAllProduto();

    }
}
