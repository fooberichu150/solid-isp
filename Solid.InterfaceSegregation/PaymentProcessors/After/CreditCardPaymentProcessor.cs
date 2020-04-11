using System;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.After
{
	public class CreditCardPaymentProcessor : ICapturablePaymentProcessor, IVoidablePaymentProcessor, IAuthorizablePaymentProcessor, ICreditablePaymentProcessor
	{
		public IPaymentItem Payment { get; private set; }

		public void Initialize(IPaymentItem payment)
		{
			Payment = payment;
		}

		public bool Authorize()
		{
			// call into the credit card provider, try to authorize it, blah blah blah...
			Payment.PaymentStatus = PaymentStatus.Authorized;
			return true;
		}

		public bool Capture()
		{
			// call into the credit card provider, try to capture the authorization, blah blah blah...
			Payment.PaymentStatus = PaymentStatus.Approved;
			return true;
		}

		public bool Credit()
		{
			// call into the credit card provider, try to credit/refund the capture, blah blah blah...
			Payment.PaymentStatus = PaymentStatus.Refunded;
			return true;
		}

		public bool Void()
		{
			// call into the credit card provider, try to void the authorization or capture...
			Payment.PaymentStatus = PaymentStatus.Voided;
			return true;
		}
	}
}
