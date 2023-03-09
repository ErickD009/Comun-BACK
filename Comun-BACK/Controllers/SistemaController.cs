using ComunBack.Datos;
using ComunBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using ComunBack.Utils;
using Microsoft.AspNetCore.Cors;

namespace ComunBack.Controllers
{
    [Route("Sistema")]
    public class SistemaController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly int tiempoEspera;
        public SistemaController(IConfiguration configuration)
        {
            _configuration = configuration;
            tiempoEspera = configuration.GetValue<int>("tiempoEspera");
        }

        [HttpPost]
        [Route("TraerVersiones")]
        public IActionResult TraerVersiones([FromBody] VersionSIS SIS)
        {
            //try
            //{
            //    string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            //    DateTime tok;

            //    //Si viene con el token fijo (Sólo para traer Versiones) pasa la fecha actual para la comparación
            //    String tokenValueLocal = new Encripta().Encrypt("#Cygnus2023!..", "");
            //    if (new Encripta().Decrypt(token, "") == "#Cygnus2023!..")
            //    {
            //        tok = DateTime.Now;
            //    }
            //    else
            //    {
            //        tok = DateTime.Parse(new Encripta().Decrypt(token, ""));
            //    }

            //    DateTime ahora = DateTime.Now;
            //    if (tok >= ahora.AddSeconds(tiempoEspera) || tok < ahora.AddSeconds(-tiempoEspera))
            //    {
            //        return BadRequest("Token no autorizado. MasterBack->Sistema->TraerVersiones");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest("Se produjo un error al intentar autenticar. Detalle: " + ex.Message);
            //}

            try
            {
                List<VersionSIS> lista = new List<VersionSIS>();
                DataSet ds = new VersionSISDA(_configuration).Version_Traer(Int32.Parse(SIS.sis_autoid));
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    VersionSIS versionSistema = new VersionSIS();
                    versionSistema.sis_autoid = row["SIS_AUTOID"].ToString();
                    versionSistema.verdet_detalle_version = row["VERDET_DETALLE_VERSION"].ToString();
                    versionSistema.ver_version = row["VER_VERSION"].ToString();
                    versionSistema.ver_descripcion = row["VER_DESCRIPCION"].ToString();
                    versionSistema.ver_fecha = row["VER_FECHA"].ToString();
                    versionSistema.sis_descripcion = row["SIS_DESCRIPCION"].ToString();
                    lista.Add(versionSistema);
                }

                return Ok(lista);

            }
            catch (Exception ex)
            {
                return BadRequest("Se produjo un error al intentar traer las versiones. Detalle: " + ex.Message);
            }
        }

    }
}
