using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B.Models {

   /// <summary>a
   /// dados dos veterinarios que tratam os cães
   /// um Cão pode ser tratado por muitos veterinários
   /// um Veterinário pode, naturamente, tratar muitos cães
   /// </summary>
   public class Veterinarios {

      public Veterinarios() {
         ListaCaesTratadosPeloVeterinario = new HashSet<Caes>();
      }


      /// <summary>
      /// Identificador do Veterinário
      /// </summary>
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public string Id { get; set; }

      /// <summary>
      /// nome do Veterinário
      /// </summary>
      public string Nome { get; set; }


      /// <summary>
      /// montante cobrado pelo Veterinário numa consulta
      /// </summary>
      public decimal? Honorarios { get; set; }

      /// <summary>
      /// atributo auxiliar para receber o valor dos homorários do Veterinário
      /// </summary>
      [NotMapped] // este atributo não vai ser adicionado à BD
      [Required]
      [Display(Name ="Honorários")]
      [RegularExpression("[0..9]+[.,]?[0-9]{0,2}")] // formata a textbox para só aceitar valores decimais
   //   [RegularExpression("[0..9]+([.,][0-9]{2})?")]  ---> outra alternativa
      public string HonorarioAux { get; set; }

      //**********************************************************
      // identificar os cães que são tratados pelo Veterinário
      // vamos ignorar a tabela do relacionamento. É como se ela não existisse...
      public ICollection<Caes> ListaCaesTratadosPeloVeterinario { get; set; }
   }
}
