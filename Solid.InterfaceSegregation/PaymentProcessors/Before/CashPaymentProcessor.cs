using System;
using System.Collections.Generic;
using System.Text;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.Before
{
	public class CashPaymentProcessor : PaymentProcessorBase
	{
		public CashPaymentProcessor(PaymentItem paymentItem)
			: base(paymentItem)
		{
		}

		public override bool Authorize()
		{
			throw new NotImplementedException();
		}

		public override bool Capture()
		{
			PaymentItem.PaymentStatus = PaymentStatus.Approved;
			// do other handling crapola here

			return true;
		}

		public override bool Credit()
		{
			throw new NotImplementedException();
		}

		public override bool Void()
		{
			throw new NotImplementedException();
		}
	}
}
