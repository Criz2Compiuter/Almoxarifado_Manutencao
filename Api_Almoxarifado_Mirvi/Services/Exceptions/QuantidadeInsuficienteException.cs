namespace Api_Almoxarifado_Mirvi.Services.Exceptions {
    public class QuantidadeInsuficienteException : Exception {
        public QuantidadeInsuficienteException() : base() { }
        public QuantidadeInsuficienteException(string message) : base(message) { }
        public QuantidadeInsuficienteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
