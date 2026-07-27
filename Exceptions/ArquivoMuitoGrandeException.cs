using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadImagemR2.Exceptions
{
    public class ArquivoMuitoGrandeException : AppException
    {
        public ArquivoMuitoGrandeException() : base("O limite de tamanho do arquivo é de 10mb", System.Net.HttpStatusCode.BadRequest)
        {  
        } 
    }
}