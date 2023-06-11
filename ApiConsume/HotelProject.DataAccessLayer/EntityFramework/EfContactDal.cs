using HotelProject.DataAccessLayer.Abstract;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.DataAccessLayer.Repositories;
using HotelProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DataAccessLayer.EntityFramework
{
    public class EfContactDal : GenericRepository<Contact>, IContactDal
    {
        public EfContactDal(Context context) : base(context)
        {

        }

        /// <summary>
        /// istatistik işlem sonuçlarını getirir
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public int GetContactCount()
        {
            var context = new Context();

            var value = context.Contacts.Count();

            return value;
        }
    }
}
