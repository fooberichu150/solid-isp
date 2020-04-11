using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.InterfaceSegregation.Models
{
	public interface IPaymentItem
	{
		decimal Amount { get; }
		PaymentType PaymentType { get; }
		PaymentStatus PaymentStatus { get; set; }
	}
}
