using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B.Models {

   /// <summary>
   /// Descreve os Criadores de cães
   /// </summary>
   public class Criadores {


      /// <summary>
      /// identificador do Criador
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// Nome do criador
      /// </summary>
      public string Nome { get; set; }

      /// <summary>
      /// nome do Sufixo associado aos nomes dos cães do criador
      /// </summary>
      public string NomeComercial { get; set; }

      /// <summary>
      /// Morada
      /// </summary>
      public string Morada { get; set; }

      /// <summary>
      /// Código Postal
      /// </summary>
      public string CodPostal { get; set; }

      /// <summary>
      /// Telemóvel
      /// </summary>
      public string Telemovel { get; set; }

      /// <summary>
      /// Email
      /// </summary>
      public string Email { get; set; }

   }
}
