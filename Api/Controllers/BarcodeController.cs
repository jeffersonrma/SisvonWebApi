
using System;
using System.Web.Http;
using System.Web.Http.Results;
using Conexao;

namespace Api.Controllers
{
    public class BarcodeController : ApiController, IBarCode
    {
        public JsonResult<Produto> GetNomeProduto(string id)
        {
            Produto produto;
            try
            {
                produto = new Produto().Buscar(id);
            }
            catch (Exception e)
            {
                produto = new Produto { Erro = e };
            }

            return Json(produto);
        }

        public string GetEstoque(string id)
        {
            Produto produto;
            try
            {
                produto = new Produto().Buscar(id);
            }
            catch (Exception ex)
            {
                return null;
            }

            return produto.Estoque.ToString();
        }
    }
}
