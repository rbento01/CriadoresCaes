using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B.Models {

   /// <summary>
   /// dados dos veterinarios que tratam os cães
   /// um Cão pode ser tratado por muitos veterinários
   /// um Veterinário pode, naturamente, tratar muitos cães
   /// </summary>
   public class Veterinarios {

      public Veterinarios() {
         ListaCaesTratadosPeloVeterinario = new HashSet<Caes>();
      }


      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public string Id { get; set; }


      public string Nome { get; set; }

      //**********************************************************
      // identificar os cães que são tratados pelo Veterinário
      // vamos ignorar a tabela do relacionamento. É como se ela não existisse...
      public ICollection<Caes> ListaCaesTratadosPeloVeterinario { get; set; }
   }
}
