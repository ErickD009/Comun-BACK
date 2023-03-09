using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace ComunBACK.Datos
{
    public class UsuarioDA
    {
        private readonly IConfiguration configuration;
        private readonly int minutosRecuperacionPass;

        public UsuarioDA(IConfiguration config)
        {
            this.configuration = config;
            this.minutosRecuperacionPass = config.GetValue<int>("minutosRecuperacionPass");
        }

        public DataSet Usuario_Traer_Sistemas(long USR_AUTOID)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("USUARIO_Traer_Sistemas_V2", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

                    cmd.SelectCommand.Parameters.AddWithValue("@USR_AUTOID", USR_AUTOID);

                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet TRABAJADOR_TraeListadoEmpresas(string USR_LOGIN, int minutosRecuperacionPass)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("LOGIN_recuperacionClave_alternativas_V2", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_LOGIN", USR_LOGIN);
                    cmd.SelectCommand.Parameters.AddWithValue("@minutosRecuperacionPass", minutosRecuperacionPass);
                    cmd.Fill(Resultados);

                    int estadoBloqueo = int.Parse(Resultados.Tables[0].Rows[0]["ESTADO_BLOQUEO"].ToString());
                    if (estadoBloqueo == 0)
                    {
                        throw new Exception("Usuario bloqueado");
                    }
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet Usuario_Traer_Email(string USR_LOGIN)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("USUARIO_MAIL_Traer", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_LOGIN", USR_LOGIN);
                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet Usuario_Actualizar_Password(string USR_PASSWORD, string USR_DRUT)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("USUARIO_ActualizaClave", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_PASSWORD", USR_PASSWORD);
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_RUT", USR_DRUT);
                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet EMAIL_Solicitud_Nueva_Contraseña(string de, string para, string cc, string asunto, string cuerpo, string adjuntos, int eshtml, string nombreSistema = "", string metodoEnvio = "")
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("EMAIL_Insertar", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@DE", de);
                    cmd.SelectCommand.Parameters.AddWithValue("@PARA", para);
                    cmd.SelectCommand.Parameters.AddWithValue("@CC", cc);
                    cmd.SelectCommand.Parameters.AddWithValue("@ASUNTO", asunto);
                    cmd.SelectCommand.Parameters.AddWithValue("@CUERPO", cuerpo);
                    cmd.SelectCommand.Parameters.AddWithValue("@ADJUNTOS", adjuntos);
                    cmd.SelectCommand.Parameters.AddWithValue("@ESHTML", eshtml);
                    cmd.SelectCommand.Parameters.AddWithValue("@NOMBRE_SISTEMA", nombreSistema);
                    cmd.SelectCommand.Parameters.AddWithValue("@METODO_ENVIA", metodoEnvio);
                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet Log_Recuperar_Password(DateTime fecha, string usr_rut)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("LOG_Correo_Inserta", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@FECHA", fecha);
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_RUT", usr_rut);
                    cmd.Fill(Resultados);
                }
                return Resultados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet Validar_Usuario(string USR_RUT, string USR_PASSWORD)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("VALIDA_USUARIO_V2", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_LOGIN", USR_RUT);
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_PASSWORD", USR_PASSWORD);
                    cmd.Fill(Resultados);
                }
                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

    }
}

