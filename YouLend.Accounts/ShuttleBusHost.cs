using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Shuttle.Core.Host;
using Shuttle.ESB.Castle;
using Shuttle.ESB.Core;
using Shuttle.ESB.SqlServer;
using Shuttle.ESB.SqlServer.Idempotence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouLend.Loans.Messages;

namespace YouLend.Accounts
{
    public class ShuttleBusHost : IHost
    {
        private static IServiceBus bus;
        private WindsorContainer container;
        
        public void Start()
        {
            container = new WindsorContainer();
            container.Register(Component.For<IAccountApplicationService>().ImplementedBy<AccountApplicationService>());
            container.Register(
                Classes.FromAssemblyInThisApplication()
                .BasedOn(typeof(IMessageHandler<>))
                .WithServiceFromInterface(typeof(IMessageHandler<>)));

            var subscriptionManager = SubscriptionManager.Default();
            subscriptionManager.Subscribe(new []{typeof(LoanCreatedExternalEvent).FullName});

            bus = ServiceBus.Create(c => c.MessageHandlerFactory(new CastleMessageHandlerFactory(container))
                                          .IdempotenceService(IdempotenceService.Default())
                                          .SubscriptionManager(subscriptionManager)).Start();            

            //bus = ServiceBus.Create().Start();

        }
    }
}
