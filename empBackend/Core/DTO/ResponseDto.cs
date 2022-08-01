namespace Core.DTO
{
    public class ResponseDto
    {
        public bool IsExitoso { get; set; } = true;

        // Tipo object para que reciba cualquiera cosa de tipo objeto, se "adapta"
        public object Resultado { get; set; }
        public string Mensaje { get; set; }
    }
}