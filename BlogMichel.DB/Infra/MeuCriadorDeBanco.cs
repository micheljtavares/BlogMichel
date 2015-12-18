using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMichel.DB.Infra
{
    public class MeuCriadorDeBanco : DropCreateDatabaseIfModelChanges<conexaoBanco>
    {
        protected override void Seed(conexaoBanco context)
        {
            context.Usuarios.Add(new Classes.Usuario { Login = "ADM", Nome = "Administrador", Senha = "Amin" });

            Database.SetInitializer<conexaoBanco>(new MeuCriadorDeBanco());

            base.Seed(context);
            
        }
    }
}
