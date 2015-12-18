using BlogMichel.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMichel.Web.Models.Blog
{
    public class DetalhesPostViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
        public bool Visivel { get; set; }
        public string Resumo { get; set; }
        public int QtdeComentarios { get; set; }
        public string Descricao { get; set; }
        public IList<TagClass> Tags { get; set; }
        
        /*CADASTRAR COMENTARIO*/

        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string ComentarioNome { get; set;}
        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo E-mail deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo E-mail é obrigatório!")]
        public string ComentarioEmail { get; set; }
        [DisplayName("Descrição")]
        [StringLength(100, ErrorMessage = "O campo Descrição deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        public string ComentarioDescricao { get; set; }
        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O campo Página Web deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Página Web é obrigatório!")]
        public string ComentarioPaginaWeb { get; set; }

        /*LISTAR COMENTARIOS*/
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public IList<Comentario> Comentarios { get; set; }

    }
}