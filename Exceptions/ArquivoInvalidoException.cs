using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UploadImagemR2.Exceptions
{
    public class ArquivoInvalidoException : AppException
    {
        public ArquivoInvalidoException() : base("Formato do arquivo inválido, carregue um arquivo do tipo .jpg/.jpeg", System.Net.HttpStatusCode.BadRequest)
        {    
        }
    }
}