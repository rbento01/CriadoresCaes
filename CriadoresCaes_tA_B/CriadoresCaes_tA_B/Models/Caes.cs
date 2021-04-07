using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B.Models {

   /// <summary>
   /// descrição de cada um dos Cães do criador
   /// </summary>
   public class Caes {

      public Caes() {
         // inicializar a lista de Fotografias de cada um dos cães
         ListasDeFotografias = new HashSet<Fotografias>();
         // inicializar a lista de Criadores do cão
         ListaCriadores = new HashSet<CriadoresCaes>();
      }

      /// <summary>
      /// Identificador de cada Cão
      /// </summary>
      [Key]
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
      /// Registo do cão no Livro de Origens Português (LOP)
      /// </summary>
      public string LOP { get; set; }

      // ********************************************************

      /// <summary>
      /// FK para a Raça do Cão
      /// </summary>
      [ForeignKey(nameof(Raca))]  // esta 'anotação' indica que o atributo 'RacaFK' está a executar o mesmo que o atributo 'Raca',
                                  // e que representa uma FK para a classe Raca
      public int RacaFK { get; set; }   // atributo para ser usado no SGBD e no C#. Representa a FK para a Raça do cão
      public Racas Raca { get; set; }   // atributo para ser usado no C#. Representa a FK para a Raça do cão


      /* se estivesse-mos a criar a tabela 'Caes' em SQL
       * CREATE TABLE Caes (
       *    Id INT PRIMARY KEY,
       *    Nome VARCHAR(30) NOT NULL,
       *    ....
       *    RacaFK INT NOT NULL,
       *    LOP VARCHAR(20),
       *    FOREIGN KEY (RacaFK) REFERENCES Racas(Id)
       * ) 
       *  
       */

      // #########################################################

      // associar os cães às suas fotografias
      /// <summary>
      /// Lista das Fotografias do cão
      /// </summary>
      public ICollection<Fotografias> ListasDeFotografias { get; set; }

      // ##########################################################

      // associar os Cães aos seus Criadores
      /// <summary>
      /// Lista dos Criadores associado ao cão
      /// </summary>
      public ICollection<CriadoresCaes> ListaCriadores { get; set; }

   }
}
