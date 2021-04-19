using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
      [Key]
      public int Id { get; set; }

      /// <summary>
      /// Nome do ficheiro com a fotografia do cão
      /// </summary>
      public string Fotografia { get; set; }

      /// <summary>
      /// Data da fotografia
      /// </summary>
      [Display(Name = "Data da fotografia")]
      public DateTime DataFoto { get; set; }

      /// <summary>
      /// Local onde a fotografia foi tirada
      /// </summary>
      public string Local { get; set; }

      //****************************************

      // criação da FK que referencia o Cão a quem a Foto pertence
      [ForeignKey(nameof(Cao))]
      [Display(Name ="Cão")]
      public int CaoFK { get; set; }
      public Caes Cao { get; set; }
   }
}
