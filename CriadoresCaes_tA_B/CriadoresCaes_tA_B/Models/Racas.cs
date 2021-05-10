using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B.Models {

   /// <summary>
   /// Raça atribuída a um cão
   /// </summary>
   public class Racas {

      public Racas() {
         // procurar os Cães de cada Raça e criar, para cada Raça, uma lista com os seus cães
         ListaDeCaes = new HashSet<Caes>();
      }

      /// <summary>
      /// Identificador de cada uma das Raças
      /// </summary>
      [Key] // identifica este atributo como PK
      public int Id { get; set; }

      /// <summary>
      /// identifica o nome da Raça
      /// </summary>
      public string Designacao { get; set; }

      //*********************************************************

      // uma raça está associada a muitos cães
      // uma raca tem uma lista de cães
      /// <summary>
      /// Lista dos Cães que são da raça
      /// </summary>
      public ICollection<Caes> ListaDeCaes { get; set; }
      /* SELECT *
       * FROM Caes c
       * WHERE c.RacaFK = ?? 
       *
       */
   }
}
