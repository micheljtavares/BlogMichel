using BlogMichel.DB;
using BlogMichel.Web.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogMichel.Web.Controllers
{
    public class LoginController : Controller
    {

        #region index
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = "teste";
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel viewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var conexao = new conexaoBanco();
            var usuario = (from p in conexao.Usuarios
                           where p.Login.ToUpper() == viewModel.Login.ToUpper()
                                    && p.Senha == viewModel.Senha
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário e/ou senha estão incorretos.");
                return View(viewModel);
            }
            FormsAuthentication.SetAuthCookie(usuario.Login, viewModel.Lembrar);
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Blog");
        }
        #endregion
        #region Sair
        public ActionResult sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        #endregion
    }
}