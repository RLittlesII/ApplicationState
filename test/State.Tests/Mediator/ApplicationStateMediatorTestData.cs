using System;
using System.Collections;
using System.Collections.Generic;
using ApplicationState.Application.Background;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Mediator;
using ApplicationState.Network.Offline;
using ApplicationState.Network.Online;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace State.Tests.Mediator;

internal class ApplicationStateMediatorTestData : IEnumerable<object[]>
{
    public ApplicationStateMediatorTestData()
    {
        _buildServiceProvider = new ServiceCollection()
            .AddSingleton(new Results())
            .AddTransient(typeof(IApplicationStateHandler<GainedSignalEvent>),
                typeof(ApplicationStateHandler<GainedSignalEvent>))
            .AddTransient(typeof(IApplicationStateHandler<LostSignalEvent>),
                typeof(ApplicationStateHandler<LostSignalEvent>))
            .AddTransient(typeof(IApplicationStateHandler<StartApplicationEvent>),
                typeof(ApplicationStateHandler<StartApplicationEvent>))
            .AddTransient(typeof(IApplicationStateHandler<StopApplicationEvent>),
                typeof(ApplicationStateHandler<StopApplicationEvent>))
            .AddTransient(typeof(IApplicationStateHandler<InitializeApplicationEvent>),
                typeof(ApplicationStateHandler<InitializeApplicationEvent>))
            .AddTransient<IMediator, MediatR.Mediator>()
            .AddTransient<IApplicationStateMediator, ApplicationStateMediator>()
            .AddMediatR(configuration => configuration.Using<ApplicationStateMediator>(),
                typeof(ApplicationStateMediator), typeof(TestInitializeHandler))
            .BuildServiceProvider();
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { _buildServiceProvider };
    }


    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private readonly IServiceProvider _buildServiceProvider;
}