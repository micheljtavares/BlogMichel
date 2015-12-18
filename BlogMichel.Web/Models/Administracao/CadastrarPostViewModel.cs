using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMichel.Web.Models.Administracao
{
    public class CadastrarPostViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "O Título é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo título deve ser entre {2} e {1}.")]
        public string Titulo { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "O Autor é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo autor deve ser entre {2} e {1}.")]
        public string Autor { get; set; }

        [DisplayName("Resumo")]
        [Required(ErrorMessage = "O Resumo é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo resumo deve ser entre {2} e {1}.")]
        public string Resumo { get; set; }

        [DisplayName("Descricao")]
        [Required(ErrorMessage = "O Descricao é obrigatório.")]
        public string Descricao { get; set; }

        [DisplayName("Data da publicação")]
        [Required(ErrorMessage = "O Data da publicação é obrigatório.")]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Hora da Publicação")]
        [Required(ErrorMessage = "O Hora da Publicação é obrigatório.")]
        public DateTime HoraPublicacao { get; set; }

        [DisplayName("Visivel")]
        [Required(ErrorMessage = "Visivel é obrigatório.")]
        public Boolean Visivel { get; set; }

        public List<string> Tags { get; set; }
    }
}