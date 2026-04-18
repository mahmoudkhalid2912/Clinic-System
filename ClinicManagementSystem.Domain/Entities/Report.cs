namespace ClinicManagementSystem.Domain.Entities;

public class Report
{
    public Guid Id { get; set; }


    public DateTime ReportDate { get; } = DateTime.Now;// dtae of generating report 

    public DateTime StartRange { get; set; } // start Range


    public DateTime EndRange { get; set; } // end range 


    public string ReportRange { get; set; } = string.Empty;// daily - weekly - monthly - yearly




    public string Type { get; set; } = string.Empty; //  financial, performance, bookings

}
