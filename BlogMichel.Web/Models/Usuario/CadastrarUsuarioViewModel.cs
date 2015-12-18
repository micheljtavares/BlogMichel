﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMichel.Web.Models.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [DisplayName("Código")]       
        public int Id { get; set; }


        [DisplayName("Login")]
        [Required(ErrorMessage = "O Login é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo Login deve ser entre {2} e {1}.")]
        public string Login { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo Nome deve ser entre {2} e {1}.")]
        public string Nome { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O Senha é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo Senha deve ser entre {2} e {1}.")]
        public string Senha { get; set; }
          
}
}