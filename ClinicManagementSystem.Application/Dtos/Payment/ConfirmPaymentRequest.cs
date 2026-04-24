using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Dtos.Payment
{
    public class ConfirmPaymentRequest
    {
        public string PaymentIntentId { get; set; }
    }
}
