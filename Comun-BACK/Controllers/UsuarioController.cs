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
using System.Data.SqlClient;


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
    public async Task<IActionResult> Usuario_Traer_Sistemas([FromBody] Usuario usr)
    {
    try
    {
        List<Usuario_X_Sistema> lista = new List<Usuario_X_Sistema>();
        DataSet ds = await Task.Run(() => new UsuarioDA(_configuration).Usuario_Traer_Sistemas(usr.USR_LOGIN, usr.USR_PASSWORD));
        foreach (DataRow row in ds.Tables[0].Rows)
        {
        Usuario_X_Sistema UxS = new Usuario_X_Sistema();
        UxS.UXS_AUTOID = long.Parse((string)row["UXS_AUTOID"].ToString());
        UxS.SIS_AUTOID = long.Parse((string)row["SIS_AUTOID"].ToString());
        UxS.SIS_DESCRIPCION = row["SIS_DESCRIPCION"].ToString();
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

    [HttpPost]
    [Route("Usuario_Enviar_Correo_Recuperacion")]
    public async Task<IActionResult> Usuario_Enviar_Correo_Recuperacion([FromBody] Email correo, string rut)
    {

        try
        {
            // Encriptar

            Encripta encripta = new Encripta();
            string tokenHash = encripta.Encrypt(DateTime.Now.ToString(), "");
            string rutHash = encripta.Encrypt(rut, "");
            string urlRecupera = "https://sitios.cygnus-est.cl/login/";

            UsuarioDA emailDA = new UsuarioDA(_configuration);
            Email correoNuevo = new Email
            {
                Para = correo.Para,
                De = "sistemas@cygnus.cl",
                CC = "",
                Asunto = "Recuperación Contraseña Sistemas Cygnus prueba4",
                Cuerpo = "Estimad@, <br /><br /> Se solicitó la recuperación de la contraseña desde el sitio web de Cygnus.<br /> <br />Por favor ingrese al siguiente link para poder actualizar su contraseña: <br> " + urlRecupera + "recuperaPassword.aspx?tok=" + tokenHash + "&rut=" + rutHash + "<br /><br /> Este link será válido solamente por una hora.<br>Una vez cumplido este tiempo, deberá solicitar nuevamente un cambio de contraseña. <br /><br /> Atte equipo Informatica",
                Adjuntos = "",
                EsHtml = 1,
                NombreSistema = "Login",
                MetodoEnvia = "SQL"
            };

            DataSet ds = await Task.Run(() => emailDA.EMAIL_Solicitud_Nueva_Contraseña(correoNuevo.De, correoNuevo.Para, correoNuevo.CC, correoNuevo.Asunto, correoNuevo.Cuerpo, correoNuevo.Adjuntos, correoNuevo.EsHtml, correoNuevo.NombreSistema, correoNuevo.MetodoEnvia));
            return Ok("Correo enviado!");
        }
        catch (Exception ex)
        {
            return BadRequest("Se produjo un error al enviar el correo." + ex.Message);
        }
    }

    [HttpPost]
    [Route("Usuario_Traer_Correo")]
    public async Task<IActionResult> Usuario_Traer_Email([FromBody] UsuarioRut usrt)
    {
        try
        {
            DataSet ds = await Task.Run(() => new UsuarioDA(_configuration).Usuario_Traer_Email(usrt.usrlogin));
            UsuarioEmail usuario = new UsuarioEmail();
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                usuario.usr_mail = ds.Tables[0].Rows[0]["USR_MAIL"].ToString();
            return Ok(usuario);
           
        }
        catch (Exception ex)
        {
            return BadRequest("Se produjo un error al intentar traer el correo del usuario. Detalle: " + ex.Message);
        }
    }

    [HttpPost]
    [Route("Usuario_Actualizar_Clave")]
    public async Task<IActionResult> Usuario_Actualizar_Clave([FromBody] UsuarioClave usrp)
    {
        try
        {
            DataSet ds = await Task.Run(() => new UsuarioDA(_configuration).Usuario_Actualizar_Password(usrp.USR_PASS,usrp.USR_RUT));
            return Ok("Contraseña Actualizada Correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest("Se produjo un error al intentar actualizar la contraseña. Detalle: " + ex.Message);
        }

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

//[HttpPost]
//[Route("Usuario_Enviar_Correo_Recuperacion")]
//public async Task<IActionResult> Usuario_Enviar_Correo_Recuperacion([FromBody] Email correo)
//{

//    try
//    {

//        string rut = "";

//        // Encriptar

//        Encripta encripta = new Encripta();
//        string tokenHash = encripta.Encrypt(DateTime.Now.ToString(), "");
//        string rutHash = encripta.Encrypt(rut, "");

//        UsuarioDA emailDA = new UsuarioDA(_configuration);
//        Email correoNuevo = new Email
//        {
//            Para = correo.Para,
//            De = "sistemas@cygnus.cl",
//            CC = "",
//            Asunto = "Recuperación Contraseña Sistemas Cygnus prueba4",
//            Cuerpo = "Estimad@, <br /><br /> Se solicitó la recuperación de la contraseña desde el sitio web de Cygnus.<br /> <br />Por favor ingrese al siguiente link para poder actualizar su contraseña: <br>",
//            Adjuntos = "",
//            EsHtml = 1,
//            NombreSistema = "Login",
//            MetodoEnvia = "SQL"
//        };

//        DataSet ds = await Task.Run(() => emailDA.EMAIL_Solicitud_Nueva_Contraseña(correoNuevo.De, correoNuevo.Para, correoNuevo.CC, correoNuevo.Asunto, correoNuevo.Cuerpo, correoNuevo.Adjuntos, correoNuevo.EsHtml, correoNuevo.NombreSistema, correoNuevo.MetodoEnvia));
//        return Ok("Correo enviado!");
//    }
//    catch (Exception ex)
//    {
//        return BadRequest("Se produjo un error al enviar el correo." + ex.Message);
//    }
//}






