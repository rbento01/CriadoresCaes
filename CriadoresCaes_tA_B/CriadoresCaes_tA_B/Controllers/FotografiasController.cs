using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CriadoresCaes_tA_B.Data;
using CriadoresCaes_tA_B.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CriadoresCaes_tA_B.Controllers {

   public class FotografiasController : Controller {

      /// <summary>
      /// este atributo representa a base de dados do projeto
      /// </summary>
      private readonly CriadoresCaesDB _context;

      /// <summary>
      /// este atributo contém os dados da app web no servidor
      /// </summary>
      private readonly IWebHostEnvironment _caminho;

      public FotografiasController(
         CriadoresCaesDB context,
         IWebHostEnvironment caminho) {
         _context = context;
         _caminho = caminho;
      }

      // GET: Fotografias
      public async Task<IActionResult> Index() {

         /* criação de uma variável que vai conter um conjunto de dados
         * vindos da base de dados
         * se fosse em SQL, a pesquisa seria:
         *     SELECT *
         *     FROM Fotografias f, Caes c
         *     WHERE f.CaoFK = c.Id
         *  exatamente equivalente a _context.Fotografias.Include(f => f.Cao), feita em LINQ
         *  f => f.Cao  <---- expressão 'lambda'
         *  ^ ^  ^
         *  | |  |
         *  | |  representa cada um dos registos individuais da tabela das Fotografias
         *  | |  e associa a cada fotografia o seu respetivo Cão
         *  | |  equivalente à parte WHERE do comando SQL
         *  | |
         *  | um símbolo que separa os ramos da expressão
         *  |
         *  representa todos registos das fotografias
         */
         var fotografias = _context.Fotografias.Include(f => f.Cao);

         // invoca a View, entregando-lhe a lista de registos
         return View(await fotografias.ToListAsync());
      }




      // GET: Fotografias/Details/5
      /// <summary>
      /// Mostra os detalhes de uma fotografia
      /// </summary>
      /// <param name="id">Identificador da Fotografia</param>
      /// <returns></returns>
      public async Task<IActionResult> Details(int? id) {

         if (id == null) {
            // entro aqui se não foi especificado o ID

            // redirecionar para a página de início
            return RedirectToAction("Index");

            //return NotFound();
         }

         // se chego aqui, foi especificado um ID
         // vou procurar se existe uma Fotografia com esse valor
         var fotografia = await _context.Fotografias
                                        .Include(f => f.Cao)
                                        .FirstOrDefaultAsync(f => f.Id == id);

         if (fotografia == null) {
            // o ID especificado não corresponde a uma fotografia

            // return NotFound();
            // redirecionar para a página de início
            return RedirectToAction("Index");
         }

         // se cheguei aqui, é pq a foto existe e foi encontrada
         // então, mostro-a na View
         return View(fotografia);
      }





      // GET: Fotografias/Create
      // [HttpGet]    não preciso desta definição, pois por omissão ele responde sempre em GET
      /// <summary>
      /// invoca, na primeira vez, a View com os dados de criação de uma fotografia
      /// </summary>
      /// <returns></returns>
      public IActionResult Create() {

         /* geração da lista de valores disponíveis na DropDown
          * o ViewData transporta dados a serem associados ao atributo 'CaoFK'
          * o SelectList é um tipo de dados especial que serve para armazenar a lista 
          * de opções de um objeto do tipo <SELECT> do HTML
          * Contém dois valores: ID + nome a ser apresentado no ecrã
          * 
          * _context.Caes : representa a fonte dos dados
          *                 na prática estamos a executar o comando SQL
          *                 SELECT * FROM Caes
          * 
          * vamos alterar a pesquisa para significar
          * SELECT * FROM Caes ORDER BY Nome
          * e, a minha expressão fica: _context.Caes.OrderBy(c=>c.Nome)
          * 
         */
         ViewData["CaoFK"] = new SelectList(_context.Caes.OrderBy(c => c.Nome), "Id", "Nome");


         return View();
      }




      // POST: Fotografias/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("DataFoto,Local,CaoFK")] Fotografias foto, IFormFile fotoCao) {

         // avaliar se  o utilizador escolheu uma opção válida na dropdown do Cão
         if (foto.CaoFK < 0) {
            // não foi escolhido um cão válido 
            ModelState.AddModelError("", "Não se esqueça de escolher um cão...");
            // devolver o controlo à View
            ViewData["CaoFK"] = new SelectList(_context.Caes.OrderBy(c => c.Nome), "Id", "Nome");
            return View(foto);
         }


         /* processar o ficheiro
          *   - existe ficheiro?
          *     - se não existe, o q fazer?  => gerar uma msg erro, e devolver controlo à View
          *     - se continuo, é pq ficheiro existe
          *       - mas, será q é do tipo correto?
          *         - avaliar se é imagem,
          *           - se sim: - especificar o seu novo nome
          *                     - associar ao objeto 'foto', o nome deste ficheiro
          *                     - especificar a localização                     
          *                     - guardar ficheiro no disco rígido do servidor
          *           - se não  => gerar uma msg erro, e devolver controlo à View
         */

         // var auxiliar
         string nomeImagem = "";

         if (fotoCao == null) {
            // não há ficheiro
            // adicionar msg de erro
            ModelState.AddModelError("", "Adicione, por favor, a fotografia do cão");
            // devolver o controlo à View
            ViewData["CaoFK"] = new SelectList(_context.Caes.OrderBy(c => c.Nome), "Id", "Nome");
            return View(foto);
         }
         else {
            // há ficheiro. Mas, será um ficheiro válido?
            // https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Basics_of_HTTP/MIME_types
            if (fotoCao.ContentType == "image/jpeg" || fotoCao.ContentType == "image/png") {
               // definir o novo nome da fotografia     
               Guid g;
               g = Guid.NewGuid();
               nomeImagem = foto.CaoFK + "_" + g.ToString(); // tb, poderia ser usado a formatação da data atual
               // determinar a extensão do nome da imagem
               string extensao = Path.GetExtension(fotoCao.FileName).ToLower();
               // agora, consigo ter o nome final do ficheiro
               nomeImagem = nomeImagem + extensao;

               // associar este ficheiro aos dados da Fotografia do cão
               foto.Fotografia = nomeImagem;

               // localização do armazenamento da imagem
               string localizacaoFicheiro = _caminho.WebRootPath;
               nomeImagem = Path.Combine(localizacaoFicheiro, "fotos", nomeImagem);
            }
            else {
               // ficheiro não é válido
               // adicionar msg de erro
               ModelState.AddModelError("", "Só pode escolher uma imagem para a associar ao cão");
               // devolver o controlo à View
               ViewData["CaoFK"] = new SelectList(_context.Caes.OrderBy(c => c.Nome), "Id", "Nome");
               return View(foto);
            }
         }


         if (ModelState.IsValid) {
            try {
               // adicionar os dados da nova fotografia à base de dados
               _context.Add(foto);
               // consolidar os dados na base de dados
               await _context.SaveChangesAsync();

               // se cheguei aqui, tudo correu bem
               // vou guardar, agora, no disco rígido do Servidor a imagem
               using var stream = new FileStream(nomeImagem, FileMode.Create);
               await fotoCao.CopyToAsync(stream);


               return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
               ModelState.AddModelError("", "Ocorreu um erro...");

            }
         }


         ViewData["CaoFK"] = new SelectList(_context.Caes.OrderBy(c => c.Nome), "Id", "Nome", foto.CaoFK);

         return View(foto);
      }




      // GET: Fotografias/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null) {
            return NotFound();
         }

         var fotografia = await _context.Fotografias.FindAsync(id);
         if (fotografia == null) {
            return NotFound();
         }

         ViewData["CaoFK"] = new SelectList(_context.Caes.OrderBy(c => c.Nome), "Id", "Nome", fotografia.CaoFK);

         // guardar o ID do objeto enviado para o browser
         // através de uma variável de sessão
         HttpContext.Session.SetInt32("NumFotoEmEdicao", fotografia.Id);
         //  Session["idFoto"] = fotografias.Id;

         return View(fotografia);
      }




      // POST: Fotografias/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Fotografia,DataFoto,Local,CaoFK")] Fotografias foto) {
         if (id != foto.Id) {
            return NotFound();
         }

         // recuperar o ID do objeto enviado para o browser
         var numIdFoto = HttpContext.Session.GetInt32("NumFotoEmEdicao");

         // e compará-lo com o ID recebido
         // se forem iguais, continuamos
         // se forem diferentes, não fazemos a alteração

         if (numIdFoto==null || numIdFoto!=foto.Id) {
            // se entro aqui, é pq houve problemas

            // redirecionar para a página de início
            return RedirectToAction("Index");
         }



         if (ModelState.IsValid) {
            try {
               _context.Update(foto);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!FotografiasExists(foto.Id)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         ViewData["CaoFK"] = new SelectList(_context.Caes, "Id", "Id", foto.CaoFK);
         return View(foto);
      }



      // GET: Fotografias/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null) {
            return NotFound();
         }

         var fotografias = await _context.Fotografias
             .Include(f => f.Cao)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (fotografias == null) {
            return NotFound();
         }

         return View(fotografias);
      }




      // POST: Fotografias/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {

         var fotografias = await _context.Fotografias.FindAsync(id);
         try {
            // Proteger a eliminação de uma foto
            _context.Fotografias.Remove(fotografias);
            await _context.SaveChangesAsync();

            // não esquecer, remover o ficheiro da Fotografia do disco rígido


         }
         catch (Exception) {

            throw;
         }

         return RedirectToAction(nameof(Index));

      }

      private bool FotografiasExists(int id) {
         return _context.Fotografias.Any(e => e.Id == id);
      }
   }
}
