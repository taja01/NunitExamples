﻿using BaseProject;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentExample
{
    [SetUpFixture]
    internal class TestFixture : BaseSetUpFixture
    {
        public override void RegisterTestFacilities(IServiceCollection serviceCollection)
        {
            base.RegisterTestFacilities(serviceCollection);
        }
    }
}
