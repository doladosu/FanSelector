using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Appointment;

namespace FanSelector.Data.Core.QueryHandler.Appointment
{
    //public class AppointmentQueryHandler : IQueryHandler<BaseByIdQuery, AppointmentQueryResult>
    //{
    //    private readonly IAppointmentRepository _appointmentRepository;

    //    public AppointmentQueryHandler(IAppointmentRepository appointmentRepository)
    //    {
    //        _appointmentRepository = appointmentRepository;
    //    }

    //    public async Task<AppointmentQueryResult> Retrieve(BaseByIdQuery query)
    //    {
    //        var data = await _appointmentRepository.GetPatientAppointmentTask(query.Id);
    //        var result = new AppointmentQueryResult
    //        {
    //            Appointment = data
    //        };
    //        return result;
    //    }
    //}
}