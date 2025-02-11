using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Testcontainers.Xunit;
using Xunit.Abstractions;

namespace FormulaOne.Tests.Integration.Repositories;

public class TeamRepositoryTests : ContainerFixture<MsSqlBuilder, MsSqlContainer>
{
    public TeamRepositoryTests(IMessageSink messageSink) : base(messageSink)
    {
    }

    protected override MsSqlBuilder Configure(MsSqlBuilder builder)
    {
        return base.Configure(builder);
    }

    [Fact]
    public void GetItemsAsync_should()
    {

    }
}
