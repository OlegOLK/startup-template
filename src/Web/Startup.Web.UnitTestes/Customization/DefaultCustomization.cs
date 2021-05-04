using AutoFixture;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Startup.Web.UnitTestes.Customization
{
    public class DefaultCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<RequestDelegate>(b => b.FromFactory(() => Substitute.For<RequestDelegate>()).OmitAutoProperties());
        }
    }
}
