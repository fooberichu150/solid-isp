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
}
