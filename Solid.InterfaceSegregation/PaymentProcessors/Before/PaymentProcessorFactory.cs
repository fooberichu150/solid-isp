using System;
using System.Collections.Generic;
using System.Text;
using Solid.InterfaceSegregation.Models;

namespace Solid.InterfaceSegregation.PaymentProcessors.Before
{
	public class PaymentProcessorFactory
	{
		public PaymentProcessorBase Build(PaymentItem paymentItem)
		{
			switch (paymentItem.PaymentType)
			{
				case PaymentType.Cash:
					return new CashPaymentProcessor(paymentItem);
				case PaymentType.CreditCard:
					return new CreditCardPaymentProcessor(paymentItem);
				default:
					throw new ArgumentOutOfRangeException(nameof(paymentItem.PaymentType), "Invalid payment type");
			}
		}
	}
}
