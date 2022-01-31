// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using DemoApp;
using Moq;

namespace Test.Unit;

public sealed class SystemClockMock : Mock<ISystemClock>
{
    public SystemClockMock SetupSystemTime(DateTimeOffset systemTime)
    {
        Setup(x => x.Now).Returns(systemTime);
        return this;
    }
}
