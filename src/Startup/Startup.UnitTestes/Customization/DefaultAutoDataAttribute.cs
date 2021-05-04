using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Startup.Web.UnitTestes.Customization
{
    public class DefaultAutoDataAttribute : AutoDataAttribute
    {
        public DefaultAutoDataAttribute(Action<IFixture> fixtureConfigurator)
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });
                fixtureConfigurator?.Invoke(fixture);
                return fixture;
            })
        { }

        public DefaultAutoDataAttribute()
             : this(f => f.Customize(new DefaultCustomization()))
        {

        }
    }

}
