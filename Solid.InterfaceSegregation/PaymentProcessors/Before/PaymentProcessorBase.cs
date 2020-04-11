using System;
using System.Collections.Generic;
using System.Text;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.Before
{
	public abstract class PaymentProcessorBase
	{
		public PaymentProcessorBase(PaymentItem paymentItem)
		{
			PaymentItem = paymentItem;
		}

		public abstract bool Authorize();

		public abstract bool Capture();

		public abstract bool Credit();

		public abstract bool Void();

		public virtual bool IsPreauthorizationEnabled => false;

		public PaymentItem PaymentItem { get; }
	}
}
