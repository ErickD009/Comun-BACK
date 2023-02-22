using ComunBack.Datos;
using ComunBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using ComunBack.Utils;
using Microsoft.AspNetCore.Cors;
using ComunBACK.Models;
using ComunBACK.Datos;

namespace ComunBack.Controllers;

    [Route("Usuario")]
    public class ComunController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly int tiempoEspera;
        public ComunController(IConfiguration configuration)
        {
            _configuration = configuration;
            tiempoEspera = configuration.GetValue<int>("tiempoEspera");
        }

        [HttpPost]
        [Route("Usuario_Traer_Sistemas")]
        public IActionResult Usuario_Traer_Sistemas([FromBody] Usuario usr)
        {
            try
            {
                List<Usuario_X_Sistema> lista = new List<Usuario_X_Sistema>();
                DataSet ds = new UsuarioDA(_configuration).Usuario_Traer_Sistemas(usr.USR_LOGIN, usr.USR_PASSWORD);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Usuario_X_Sistema UxS = new Usuario_X_Sistema();
                    UxS.UXS_AUTOID = long.Parse((string)row["UXS_AUTOID"].ToString());
                    UxS.SIS_AUTOID = long.Parse((string)row["SIS_AUTOID"].ToString());
                    UxS.SIS_DESCRIPCION= row["SIS_DESCRIPCION"].ToString();
                    UxS.SIS_URL = row["SIS_URL"].ToString();
                    UxS.SIS_TOOLTIP = row["SIS_TOOLTIP"].ToString();
                    UxS.SIS_ICON_URL = row["SIS_ICON_URL"].ToString();
                    lista.Add(UxS);
                }

                return Ok(lista);

            }
            catch (Exception ex)
            {
                return BadRequest("Se produjo un error al intentar traer los sistemas del usuario. Detalle: " + ex.Message);
            }

        }


    [HttpPost]
    [Route("Usuario_Traer_Empresas")]
    public async Task<IActionResult> Usuario_Traer_Empresas([FromBody] UsuarioRut usrt)
    {
        try
        {
            List<Usuario_X_Empresa> lista = new List<Usuario_X_Empresa>();
            DataSet ds = await Task.Run(() => new UsuarioDA(_configuration).TRABAJADOR_TraeListadoEmpresas(usrt.usrlogin));
            if (ds.Tables[1].Rows.Count == 0)
            {
                return BadRequest("No se encontraron empresas para este usuario.");
            }
            else
            {
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    Usuario_X_Empresa UxE = new Usuario_X_Empresa();
                    UxE.cli_autoid = long.Parse((string)row["cli_autoid"].ToString());
                    UxE.nombreCliente = row["nombreCliente"].ToString();
                    UxE.marcaRespuestaCorrecta = row["marcaRespuestaCorrecta"].ToString();
                    lista.Add(UxE);
                }

                return Ok(lista);
            }

        }
        catch (Exception ex)
        {
            return BadRequest("Se produjo un error al intentar traer la lista de empresas del usuario. Detalle: " + ex.Message);
        }
    }


    //[HttpPost]
    //[Route("GeneraToken")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    //public String GeneraToken()
    //{
    //    Encripta encripta = new Encripta();
    //    string token = encripta.Encrypt(DateTime.Now.ToString(), "");
    //    return token;
    //}

}





