using System.ServiceModel;
using BancoSoapService.Services.Contracts;

namespace BancoSoapService.Services
{
    [ServiceContract]
    public interface IBancoService
    {
        [OperationContract]
        EvaluateCreditResponse EvaluateCredit(EvaluateCreditRequest request);

        [OperationContract]
        GetAmortizationScheduleResponse GetAmortizationSchedule(GetAmortizationScheduleRequest request);

        [OperationContract]
        GetClientInfoResponse GetClientInfo(GetClientInfoRequest request);
    }
}
