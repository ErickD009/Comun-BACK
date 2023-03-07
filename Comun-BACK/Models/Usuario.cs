namespace ComunBACK.Models;

    public class Usuario
    {
        public long USR_AUTOID { get; set; }
        public string? USR_LOGIN { get; set; }
        public string? USR_PASS { get; set; }
        public string? USR_NOMBRE { get; set; }
        public string? USR_RUT { get; set;}
        public bool USR_ESTADO { get; set; }
        public string? USR_ID { get; set; }
        public string? USR_MAIL { get; set; }
        public string? USR_ANEXO { get; set; }
        public string? USR_TELEFONO { get; set; }
        public string? USR_ID_NEXUS { get; set; }
        public bool? USR_EXPORTADO_INTRANET { get; set; }
        public string? USR_INICIALES { get; set; }
        public string? USR_TOKEN { get; set; }
        public DateTime? USR_FECHA_TOKEN { get; set; }
        public int? USR_AUTO_APRUEBA { get; set; }
        public int? USR_EST_CUENTA { get; set; }
        public string? USR_ID_BPO { get; set; }
        public string? USR_PASSWORD { get; set; }

    }

