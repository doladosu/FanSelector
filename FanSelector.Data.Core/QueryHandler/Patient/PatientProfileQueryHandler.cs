using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Patient;

namespace FanSelector.Data.Core.QueryHandler.Patient
{
    //public class PatientProfileQueryHandler : IQueryHandler<BaseByIdQuery, PatientProfileQueryResult>
    //{
    //    private readonly IPatientRepository _patientRepository;

    //    public PatientProfileQueryHandler(IPatientRepository patientRepository)
    //    {
    //        _patientRepository = patientRepository;
    //    }

    //    public async Task<PatientProfileQueryResult> Retrieve(BaseByIdQuery query)
    //    {
    //        var data = await _patientRepository.GetPatientProfileTask(query.Id);
    //        var result = new PatientProfileQueryResult
    //        {
    //            Patient = data
    //        };
    //        return result;
    //    }
    //}
}