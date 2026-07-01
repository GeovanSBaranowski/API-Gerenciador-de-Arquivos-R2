using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadImagemR2.Models
{
    public class ListResult
    {
        public string Nome { get; set; } = string.Empty;
        public long? Tamanho { get; set; }
        public DateTime? UltimaModificacao { get; set; }
    }
}