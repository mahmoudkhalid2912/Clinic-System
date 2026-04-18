namespace ClinicManagementSystem.Domain.Entities.Enums;

public enum BookingStatus
{
    Pending = 0,      // لسه متسجل جديد ومستني تأكيد
    Confirmed = 1,    // تم تأكيد الحجز
    Cancelled = 2,    // اتلغى من المريض أو الادمن
    Completed = 3,    // انتهت الزيارة
    Missed = 4        // المريض مجاش
}
