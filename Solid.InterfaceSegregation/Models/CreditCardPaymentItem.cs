using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.InterfaceSegregation.Models
{
	public class CreditCardPaymentItem : PaymentItem
	{
		public string CardNumber { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string CardVerificationCode { get; set; }
	}
}
