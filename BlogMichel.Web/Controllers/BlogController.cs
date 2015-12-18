using BlogMichel.DB;
using BlogMichel.DB.Classes;
using BlogMichel.Web.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMichel.Web.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index(int? pagina, string tag, string pesquisa)
        {
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registrosPorPagina = 10;


            var conexaoBanco = new conexaoBanco();

            var posts = (from p in conexaoBanco.Posts
                         where p.Visivel == true
                         select p);
            if (!string.IsNullOrEmpty(tag))
            {
                posts = (from p in posts
                         where p.TagsPost.Any(x => x.IdTag.ToUpper() == tag.ToUpper())
                         select p);
            }
            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts
                         where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                         || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                         || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p);
            }
            var qtdeRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registrosPorPagina);
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / registrosPorPagina);

            var viewModel = new ListarPostsViewModel();
            viewModel.Posts = (from p in posts
                               orderby p.DataPublicacao descending
                               select new DetalhesPostViewModel
                               {
                                   DataPublicacao = p.DataPublicacao,
                                   Autor = p.Autor,
                                   Descricao = p.Descricao,
                                   Id = p.Id,
                                   Resumo = p.Resumo,
                                   Titulo = p.Titulo,
                                   Visivel = p.Visivel,
                                   QtdeComentarios = p.Comentarios.Count
                                }).Skip(qtdeRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtdePaginas;
            viewModel.Tag = tag;
            viewModel.Tags = (from p in conexaoBanco.TagClass
                              where conexaoBanco.TagPosts.Any(x => x.IdTag == p.Tag)
                              orderby p.Tag
                              select p.Tag).ToList();
            viewModel.Pesquisa = pesquisa;

            return View(viewModel);
        }
        public ActionResult _Paginacao()
        {
            return PartialView();
        }
        #region Post
        public ActionResult Post(int id, int? pagina)
        {
            var conexaoBanco = new conexaoBanco();
            var post = (from p in conexaoBanco.Posts
                        where p.Id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }

            var viewModel = new DetalhesPostViewModel();
            preencherViewModel(post, viewModel, pagina);
            return View(viewModel);

        }

        private void preencherViewModel(Post post, DetalhesPostViewModel viewModel, int? pagina)
        {
            viewModel.Id = post.Id;
            viewModel.Autor = post.Autor;
            viewModel.DataPublicacao = post.DataPublicacao;
            viewModel.Titulo = post.Titulo;
            viewModel.Resumo = post.Resumo;
            viewModel.Visivel = post.Visivel;
            viewModel.QtdeComentarios = post.Comentarios.Count;
            viewModel.Descricao = post.Descricao;
            viewModel.Tags = post.TagsPost.Select(x => x.TagClass).ToList();
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registrosPorPagina = 10;
            var qtdeRegistros = post.Comentarios.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registrosPorPagina);
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / registrosPorPagina);
            viewModel.Comentarios = (from p in post.Comentarios
                                     orderby p.DataHora descending
                                     select p).Skip(qtdeRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtdePaginas;          
        }

        [HttpPost]
        public ActionResult Post(DetalhesPostViewModel viewModel)
        {
            var ConexaoBanco = new conexaoBanco();
            var post = (from p in ConexaoBanco.Posts
                        where p.Id == viewModel.Id
                        select p).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (post == null)
                {
                    throw new Exception(string.Format("Post código {0} não encontrado.", viewModel.Id));
                }

                var comentario = new Comentario();
                comentario.AdmPost = HttpContext.User.Identity.IsAuthenticated;
                comentario.Descriçao = viewModel.ComentarioDescricao;
                comentario.Email = viewModel.ComentarioEmail;
                comentario.IdPost = viewModel.Id;
                comentario.Nome = viewModel.ComentarioNome;
                comentario.PaginaWeb = viewModel.ComentarioPaginaWeb;
                comentario.DataHora = DateTime.Now;

                try
                {
                    ConexaoBanco.Comentarios.Add(comentario);
                    ConexaoBanco.SaveChanges();
                    return Redirect(Url.Action("Post", new
                    {
                        ano = viewModel.DataPublicacao.Year,
                        mes = viewModel.DataPublicacao.Month,
                        dia = viewModel.DataPublicacao.Day,
                        titulo = viewModel.Titulo,
                        id = viewModel.Id
                    }) + "#comentarios");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            preencherViewModel(post, viewModel, null);
            return View(viewModel);
        }
        #endregion
    }
}