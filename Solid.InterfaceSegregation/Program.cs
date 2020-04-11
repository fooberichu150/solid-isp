using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.InterfaceSegregation
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
				string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

				if (string.IsNullOrWhiteSpace(env))
				{
					env = "Development";
				}

				var builder = new ConfigurationBuilder()
								.SetBasePath(Directory.GetCurrentDirectory())
								.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
								.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
								.AddEnvironmentVariables();

				IConfigurationRoot configuration = builder.Build();
				var services = new ServiceCollection();
				services.AddTransient<Application>();
				//services.Configure<MySettings>(configuration.GetSection("MySettings"));

				var provider = services.BuildServiceProvider();

				var application = provider.GetService<Application>();
				application.Run();
			}
			finally
			{
				Console.WriteLine("\r\nPress any key to exit...");
				Console.ReadKey();
			}
		}
    }

	public class Application
	{
		public bool Run()
		{
			Console.Out.WriteLine("Running \"before\" transactions...");
			RunBefore();

			Console.Out.WriteLine("****************************************");
			Console.Out.WriteLine("Running \"before\" transactions...");
			RunAfter();

			return true;
		}

		private void RunBefore()
		{
			var factory = new PaymentProcessors.Before.PaymentProcessorFactory();
			var cashPayment = new Models.PaymentItem
			{
				Amount = 50m, 
				PaymentType = Models.PaymentType.Cash
			};

			var processor = factory.Build(cashPayment);

			var authorize = new Func<PaymentProcessors.Before.PaymentProcessorBase, bool>((processor) => processor.Authorize());
			var capture = new Func<PaymentProcessors.Before.PaymentProcessorBase, bool>((processor) => processor.Capture());
			var credit = new Func<PaymentProcessors.Before.PaymentProcessorBase, bool>((processor) => processor.Credit());
			var voidPayment = new Func<PaymentProcessors.Before.PaymentProcessorBase, bool>((processor) => processor.Void());

			Console.Out.WriteLine("Running cash transactions...");
			Console.Out.WriteLine($"Trying cash authorization: {TryRun(authorize, processor)}");
			Console.Out.WriteLine($"Trying cash capture: {TryRun(capture, processor)}");
			Console.Out.WriteLine($"Trying cash credit: {TryRun(credit, processor)}");
			Console.Out.WriteLine($"Trying cash voidPayment: {TryRun(voidPayment, processor)}");

			var creditPayment = new Models.CreditCardPaymentItem
			{
				Amount = 50m,
				PaymentType = Models.PaymentType.CreditCard,
				CardNumber = "4556210085229628",
				CardVerificationCode = "123",
				ExpirationDate = DateTime.Today
			};
			processor = factory.Build(creditPayment);

			Console.Out.WriteLine("Running credit card transactions...");
			Console.Out.WriteLine($"Trying credit card authorization: {TryRun(authorize, processor)}");
			Console.Out.WriteLine($"Trying credit card capture: {TryRun(capture, processor)}");
			Console.Out.WriteLine($"Trying credit card credit: {TryRun(credit, processor)}");
			Console.Out.WriteLine($"Trying credit card voidPayment: {TryRun(voidPayment, processor)}");
		}

		private bool TryRun(Func<PaymentProcessors.Before.PaymentProcessorBase, bool> paymentFunc, PaymentProcessors.Before.PaymentProcessorBase processor)
		{
			try
			{
				return paymentFunc(processor);
			}
			catch
			{
				return false;
			}
		}

		private void RunAfter()
		{
			var factory = new PaymentProcessors.After.PaymentProcessorFactory();
			var cashPayment = new Models.PaymentItem
			{
				Amount = 50m,
				PaymentType = Models.PaymentType.Cash
			};

			var authorizable = factory.GetAuthorizableProcessor(cashPayment);
			var capturable = factory.GetCapturablePaymentProcessor(cashPayment);
			var creditable = factory.GetCreditablePaymentProcessor(cashPayment);
			var voidable = factory.GetVoidablePaymentProcessor(cashPayment);

			Console.Out.WriteLine("Running cash transactions...");
			Console.Out.WriteLine($"Trying cash authorization: {authorizable?.Authorize() ?? false}");
			Console.Out.WriteLine($"Trying cash capture: {capturable?.Capture() ?? false}");
			Console.Out.WriteLine($"Trying cash credit: {creditable?.Credit() ?? false}");
			Console.Out.WriteLine($"Trying cash voidPayment: {voidable?.Void() ?? false}");

			var creditPayment = new Models.CreditCardPaymentItem
			{
				Amount = 50m,
				PaymentType = Models.PaymentType.CreditCard,
				CardNumber = "4556210085229628",
				CardVerificationCode = "123",
				ExpirationDate = DateTime.Today
			};

			authorizable = factory.GetAuthorizableProcessor(creditPayment);
			capturable = factory.GetCapturablePaymentProcessor(creditPayment);
			creditable = factory.GetCreditablePaymentProcessor(creditPayment);
			voidable = factory.GetVoidablePaymentProcessor(creditPayment);

			Console.Out.WriteLine("Running credit card transactions...");
			Console.Out.WriteLine($"Trying credit card authorization: {authorizable?.Authorize() ?? false}");
			Console.Out.WriteLine($"Trying credit card capture: {capturable?.Capture() ?? false}");
			Console.Out.WriteLine($"Trying credit card credit: {creditable?.Credit() ?? false}");
			Console.Out.WriteLine($"Trying credit card voidPayment: {voidable?.Void() ?? false}");
		}
	}
}
