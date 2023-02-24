namespace ComunBACK.Models
{
    public class Email
    {
        public string De { get; set; }
        public string Para { get; set; }
        public string CC { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string Adjuntos { get; set; }
        public int EsHtml { get; set; }
        public string NombreSistema { get; set; }
        public string MetodoEnvia { get; set; }
    }
}
