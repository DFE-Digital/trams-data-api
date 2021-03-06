using TramsDataApi.DatabaseModels;

namespace TramsDataApi.Gateways
{
    public interface IA2BApplicationGateway
    {
        A2BApplication GetByApplicationId(string applicationId);
        A2BApplication CreateA2BApplication(A2BApplication request);
    }
}