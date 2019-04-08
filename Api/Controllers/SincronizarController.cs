
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using System.Web.Http.Results;
using Conexao;


namespace Api.Controllers
{
    public class SincronizarController : ApiController, ISincronizar
    {
        public JsonResult<Sincronizar> GetProduto(string id)
        {
            try
            {
                using (Conectar conectar = new Conectar())
                {
                    return Json(new SincronizarDao(conectar).BuscarPorCodigo(id));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public JsonResult<Sincronizar> GetProdutoSinc(string id)
        {
            try
            {
                using (Conectar conectar = new Conectar())
                {
                    return Json(new SincronizarDao(conectar).BuscarPorOrden(Convert.ToInt32(id)));
                }
            }
            catch (Exception e)
            {
                return Json(new Sincronizar { Erro = e });
            }
        }

        public JsonResult<ICollection<Sincronizar>> GetAllProduto()
        {
            try
            {
                using (Conectar conectar = new Conectar())
                {
                    return Json(new SincronizarDao(conectar).BuscarTodos());
                }
            }
            catch (Exception ex)
            {
                ICollection<Sincronizar> list = new Collection<Sincronizar> { new Sincronizar { Erro = ex } };
                return Json(list);
            }

        }
    }
}
