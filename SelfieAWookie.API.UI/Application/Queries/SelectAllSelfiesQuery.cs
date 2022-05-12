using MediatR;
using SelfieAWookie.API.UI.Application.DTOs;

namespace SelfieAWookie.API.UI.Application.Queries
{
    public class SelectAllSelfiesQuery : IRequest<List<SelfieResumeDto>>
    {
        #region Properties
        public int WookieId { get; set; }
        #endregion
    }
}
