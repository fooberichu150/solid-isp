using System;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.After
{
	public interface IPaymentProcessor
	{
		void Initialize(IPaymentItem payment);
	}

	public interface ICapturablePaymentProcessor : IPaymentProcessor
	{
		bool Capture();
	}

	public interface IVoidablePaymentProcessor : IPaymentProcessor
	{
		bool Void();
	}

	public interface IAuthorizablePaymentProcessor : IPaymentProcessor
	{
		bool Authorize();
	}

	public interface ICreditablePaymentProcessor : IPaymentProcessor
	{
		bool Credit();
	}

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

	public class PaymentProcessorFactory
	{
		public IAuthorizablePaymentProcessor GetAuthorizableProcessor(IPaymentItem paymentItem)
		{
			var paymentProcessor = default(IAuthorizablePaymentProcessor);

			if (paymentItem.PaymentType == PaymentType.CreditCard)
				paymentProcessor = new CreditCardPaymentProcessor();

			paymentProcessor?.Initialize(paymentItem);

			return paymentProcessor;
		}

		public ICreditablePaymentProcessor GetCreditablePaymentProcessor(IPaymentItem paymentItem)
		{
			var paymentProcessor = default(ICreditablePaymentProcessor);

			if (paymentItem.PaymentType == PaymentType.CreditCard)
				paymentProcessor = new CreditCardPaymentProcessor();

			paymentProcessor?.Initialize(paymentItem);

			return paymentProcessor;
		}


		public IVoidablePaymentProcessor GetVoidablePaymentProcessor(IPaymentItem paymentItem)
		{
			var paymentProcessor = default(IVoidablePaymentProcessor);

			if (paymentItem.PaymentType == PaymentType.CreditCard)
				paymentProcessor = new CreditCardPaymentProcessor();

			paymentProcessor?.Initialize(paymentItem);

			return paymentProcessor;
		}

		public ICapturablePaymentProcessor GetCapturablePaymentProcessor(IPaymentItem paymentItem)
		{
			ICapturablePaymentProcessor paymentProcessor;

			switch (paymentItem.PaymentType)
			{
				case PaymentType.Cash:
					paymentProcessor = new CashPaymentProcessor();
					break;
				case PaymentType.CreditCard:
					paymentProcessor = new CreditCardPaymentProcessor();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(paymentItem.PaymentType), "Invalid payment type");
			}

			paymentProcessor.Initialize(paymentItem);
			return paymentProcessor;
		}
	}
}
