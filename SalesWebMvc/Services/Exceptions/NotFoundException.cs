using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
            //Controle para tratar cada tipo de exceção que ocorrer

        }
    }
}
