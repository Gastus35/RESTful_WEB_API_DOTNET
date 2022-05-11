using SelfiesAWookie.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Core.Selfies.Domain
{
    public interface ISelfieRepository : IRepository
    {
        ICollection<Selfie> GetAll(int wookieId);

        Selfie AddOne(Selfie selfie);

        Picture AddOnePicture(string url);

    }
}
