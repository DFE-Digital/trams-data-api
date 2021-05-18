using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories;
using TramsDataApi.Gateways;
using TramsDataApi.RequestModels;
using TramsDataApi.ResponseModels;

namespace TramsDataApi.UseCases
{
    public class CreateAcademyTransferProject : ICreateAcademyTransferProject
    {
        private readonly IAcademyTransferProjectGateway _academyTransferProjectGateway;
        public CreateAcademyTransferProject(IAcademyTransferProjectGateway academyTransferProjectGateway)
        {
            _academyTransferProjectGateway = academyTransferProjectGateway;
        }

        public AcademyTransferProjectResponse Execute(CreateOrUpdateAcademyTransferProjectRequest request)
        {
            var academyTransferProjectToCreate = AcademyTransferProjectFactory.Create(request);
            var createdAcademyTransferModel =
                _academyTransferProjectGateway.CreateAcademyTransferProject(academyTransferProjectToCreate);
            return AcademyTransferProjectResponseFactory.Create(createdAcademyTransferModel);
        }
    }
}