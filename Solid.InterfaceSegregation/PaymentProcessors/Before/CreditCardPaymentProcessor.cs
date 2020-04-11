using System;
using System.Collections.Generic;
using System.Text;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.Before
{
	public class CreditCardPaymentProcessor : PaymentProcessorBase
	{
		public CreditCardPaymentProcessor(PaymentItem paymentItem)
			: base(paymentItem)
		{
		}

		public override bool Authorize()
		{
			// call into the credit card provider, try to authorize it, blah blah blah...
			PaymentItem.PaymentStatus = PaymentStatus.Authorized;
			return true;
		}

		public override bool Capture()
		{
			// call into the credit card provider, try to capture the authorization, blah blah blah...
			PaymentItem.PaymentStatus = PaymentStatus.Approved;
			return true;
		}

		public override bool Credit()
		{
			// call into the credit card provider, try to credit/refund the capture, blah blah blah...
			PaymentItem.PaymentStatus = PaymentStatus.Refunded;
			return true;
		}

		public override bool Void()
		{
			// call into the credit card provider, try to void the authorization or capture...
			PaymentItem.PaymentStatus = PaymentStatus.Voided;
			return true;
		}
	}
}
