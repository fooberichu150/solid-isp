using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.InterfaceSegregation.Models
{
	public class PaymentItem : IPaymentItem
	{
		public decimal Amount { get; set; }
		public PaymentType PaymentType { get; set; }
		public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
	}
}
