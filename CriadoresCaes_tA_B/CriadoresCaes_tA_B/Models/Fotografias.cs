using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B.Models {


   /// <summary>
   /// Fotografias dos cães
   /// </summary>
   public class Fotografias {

      /// <summary>
      /// Identificador da fotografia
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// Nome do ficheiro com a fotografia do cão
      /// </summary>
      public string Fotografia { get; set; }

      /// <summary>
      /// Data da fotografia
      /// </summary>
      public DateTime DataFoto { get; set; }

      /// <summary>
      /// Local onde a fotografia foi tirada
      /// </summary>
      public string Local { get; set; }

   }
}
