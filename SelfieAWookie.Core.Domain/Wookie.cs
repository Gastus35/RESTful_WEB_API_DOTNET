using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SelfieAWookie.Core.Selfies.Domain
{
    public class Wookie
    {
        #region Properties
        public int Id { get; set; }
        [JsonIgnore]
        public List<Selfie>? Selfies { get; set; }
        #endregion
    }
}
