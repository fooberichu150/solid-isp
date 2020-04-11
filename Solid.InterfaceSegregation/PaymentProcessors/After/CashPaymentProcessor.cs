using System;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.After
{
	public class CashPaymentProcessor : ICapturablePaymentProcessor
	{
		public void Initialize(IPaymentItem payment)
		{
			Payment = payment;
		}

		public IPaymentItem Payment { get; private set; }

		public bool Capture()
		{
			Payment.PaymentStatus = PaymentStatus.Approved;

			return true;
		}
	}
}
