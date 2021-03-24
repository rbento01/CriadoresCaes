using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B.Models {

   /// <summary>
   /// descrição de cada um dos Cães do criador
   /// </summary>
   public class Caes {

      /// <summary>
      /// Identificador de cada Cão
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// Nome do cão
      /// </summary>
      public string Nome { get; set; }

      /// <summary>
      /// Sexo do cão
      /// M - Masculino
      /// F - Feminino
      /// </summary>
      public string Sexo { get; set; }

      /// <summary>
      /// Data de Nascimento
      /// </summary>
      public DateTime DataNasc { get; set; }

      /// <summary>
      /// Data de Compra
      /// </summary>
      public DateTime DataCompra { get; set; }

      /// <summary>
      /// Registo do cão no Livro de Origens Português (LOP)
      /// </summary>
      public string LOP { get; set; }


   }
}
