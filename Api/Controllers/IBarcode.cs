
using System.Web.Http.Results;
using Conexao;

namespace Api.Controllers
{
    public interface IBarCode
    {

         JsonResult<Produto> GetNomeProduto(string Ean);
        string GetEstoque(string Ean);

    }
}
