using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SelfiesAWookie.Core.Framework;

namespace SelfieAWookies.Core.Selfies.Infrastructures.Repositories
{
    public class DefaultSelfieRepository : ISelfieRepository
    {
        #region Fields
        private readonly SelfiesContext _context;
        #endregion

        #region Constructors
        public DefaultSelfieRepository(SelfiesContext context)
        {
            _context = context;
        }

        #endregion

        #region Public methods 
        public ICollection<Selfie> GetAll(int wookieId)
        {
            var query = this._context.Selfies.Include(item => item.Wookie).AsQueryable();

            if (wookieId > 0)
            {
                query = query.Where(item => item.WookieId == wookieId);
            }

            return query.ToList();
        }

        public Selfie AddOne(Selfie selfie)
        {
            return this._context.Selfies.Add(selfie).Entity;
        }

        public Picture AddOnePicture(string url)
        {
            return this._context.Pictures.Add(new Picture()
            {
                Url = url,
            }).Entity;
        }

        #endregion
        public IUnitOfWork UnitOfWork => this._context;
        #region Properties

        #endregion
    }
}
