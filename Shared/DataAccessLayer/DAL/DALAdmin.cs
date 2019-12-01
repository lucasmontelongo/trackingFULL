using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALAdmin
    {
        Conversiones _conv;

        public DALAdmin()
        {
            _conv = new Conversiones();
        }

        public bool add(string email)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Usuario u = en.Usuario.FirstOrDefault(x => x.email == email);
                    if(u != null)
                    {
                        u.rol = "Admin";
                        en.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
    }
}
