using BlogMichel.DB;
using BlogMichel.DB.Classes;
using BlogMichel.Web.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMichel.Web.Controllers
{
    public class UsuariosController : Controller
    {

        public ActionResult Index()
        {
            var conexao = new conexaoBanco();

            var usuario = (from x in conexao.Usuarios
                           orderby x.Nome
                           select x).ToList();

            return View(usuario);
        }
        #region CadastrarUsuario
        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        // GET: Usuario
        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new conexaoBanco();
                var usuario = new Usuario();

                usuario.Login = viewmodel.Login.ToUpper();
                usuario.Nome = viewmodel.Nome;
                usuario.Senha = viewmodel.Senha;               

                conexao.Usuarios.Add(usuario);
                try
                {
                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Usuario com código {0} não encontrado", viewmodel.Id));
                    }

                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Ocorreu um erro no banco ao Incluir", exp.Message); ;
                }
            }
            return View(viewmodel);
        }

        public ActionResult EditarUsuario(int id)
        {
            var conexao = new conexaoBanco();

            var usuario = (from x in conexao.Usuarios
                           where x.Id == id
                           select x).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuario com código {0} não encontrado.", id));
            }
            var viewmodel = new CadastrarUsuarioViewModel();
            viewmodel.Id = usuario.Id;
            viewmodel.Login = usuario.Login;
            viewmodel.Nome = usuario.Nome;
            viewmodel.Senha = usuario.Senha;
            return View(viewmodel);
        }
        #endregion
        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new conexaoBanco();
                var usuario = (from x in conexao.Usuarios
                               where x.Id == viewmodel.Id
                               select x).FirstOrDefault();
                viewmodel.Id = usuario.Id;
                usuario.Login = viewmodel.Login.ToUpper();
                usuario.Nome = viewmodel.Nome;
                usuario.Senha = viewmodel.Senha;
                             
                try
                {
                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    && p.Id != usuario.Id
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Ja existe Login Cadastrado com login {0}.", usuario.Login));
                    }

                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Ocorreu um erro no banco ao Salvar", exp.Message); ;
                }
            }
            return View(viewmodel);
        }


        #region ExcluirUsuario
        public ActionResult ExcluirUsuario(int id)
        {
            var conexaoBanco = new conexaoBanco();
            var usuario = (from p in conexaoBanco.Usuarios
                           where p.Id == id
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuario código {0} não foi encontrado.", id));
            }
            conexaoBanco.Usuarios.Remove(usuario);
            conexaoBanco.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
        #endregion
    



