using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Appointment;

namespace FanSelector.Data.Core.QueryHandler.Appointment
{
    //public class AppointmentsQueryHandler : IQueryHandler<BaseByIdQuery, AppointmentsQueryResult>
    //{
    //    private readonly IAppointmentRepository _appointmentRepository;

    //    public AppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
    //    {
    //        _appointmentRepository = appointmentRepository;
    //    }

    //    public async Task<AppointmentsQueryResult> Retrieve(BaseByIdQuery query)
    //    {
    //        var allData = await _appointmentRepository.GetAllPatientAppointmentsTask(query.UserId, query.Id);
    //        var result = new AppointmentsQueryResult
    //        {
    //            Appointments = allData,
    //        };
    //        return result;
    //    }
    //}
}