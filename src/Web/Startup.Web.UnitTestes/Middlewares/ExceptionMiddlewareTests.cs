using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Startup.Web.Middlewares;
using Startup.Web.UnitTestes.Customization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Startup.Web.UnitTestes.Middlewares
{
    public class ExceptionMiddlewareTests
    {
        [Theory]
        [DefaultAutoData]
        public async Task InvokeAsync_NextThrowException_ReturnsStatusCode500([Frozen] RequestDelegate next,
            HttpContext context,
            ExceptionMiddleware sut)
        {
            next.WhenForAnyArgs(x => x(Arg.Any<HttpContext>())).Throw(new Exception());
            Func<Task> func = async () => await sut.InvokeAsync(context).ConfigureAwait(false);

            await func.Should().NotThrowAsync().ConfigureAwait(false);
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
