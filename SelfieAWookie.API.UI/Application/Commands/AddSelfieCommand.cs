using MediatR;
using SelfieAWookie.API.UI.Application.DTOs;

namespace SelfieAWookie.API.UI.Application.Commands
{
    public class AddSelfieCommand : IRequest<SelfieDto>
    {
        #region Properties
        public SelfieDto Item { get; set; }
        #endregion
    }
}
