using System;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.After
{
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
