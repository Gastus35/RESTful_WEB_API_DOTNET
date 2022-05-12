using MediatR;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookie.Core.Selfies.Domain;

namespace SelfieAWookie.API.UI.Application.Commands
{
    public class AddSelfieHandler : IRequestHandler<AddSelfieCommand, SelfieDto>
    {
        #region Fields
        private readonly ISelfieRepository _repository;
        #endregion

        #region Constructors
        public AddSelfieHandler(ISelfieRepository repository)
        {
            this._repository = repository;
        }
        #endregion


        #region Public methods
        public Task<SelfieDto> Handle(AddSelfieCommand request, CancellationToken cancellationToken)
        {
            SelfieDto result = null;
            Selfie addSelfie = this._repository.AddOne(new Selfie()
            {
                Title = request.Item.Title,
                ImagePath = request.Item.ImagePath,
            }); ;
            this._repository.UnitOfWork.SaveChanges();

            if (addSelfie != null)
            {
                request.Item.Id = addSelfie.Id;
                result = request.Item;
            }

            return Task.FromResult(result);
        }
        #endregion
    }
}
