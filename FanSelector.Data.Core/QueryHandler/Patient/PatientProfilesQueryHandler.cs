using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Patient;

namespace FanSelector.Data.Core.QueryHandler.Patient
{
    //public class PatientProfilesQueryHandler : IQueryHandler<BaseByIdQuery, PatientProfilesQueryResult>
    //{
    //    private readonly IPatientRepository _patientRepository;

    //    public PatientProfilesQueryHandler(IPatientRepository patientRepository)
    //    {
    //        _patientRepository = patientRepository;
    //    }

    //    public async Task<PatientProfilesQueryResult> Retrieve(BaseByIdQuery query)
    //    {
    //        var allData = await _patientRepository.GetAllPatientsTask(query.UserId);
    //        var result = new PatientProfilesQueryResult
    //        {
    //            Patients = allData,
    //        };
    //        return result;
    //    }
    //}
}