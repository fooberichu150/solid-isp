using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.InterfaceSegregation.Models
{
	public enum PaymentStatus
	{
		Pending = 0,
		Authorized = 1,
		Approved = 2,
		Declined = 3,
		Refunded = 4,
		Voided = 5,
	}
}
