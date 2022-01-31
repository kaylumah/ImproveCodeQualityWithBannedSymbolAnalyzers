// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace DemoApp;

public interface ISystemClock
{
    DateTimeOffset Now { get; }
}

public class SystemClock : ISystemClock
{    public DateTimeOffset Now => DateTimeOffset.Now;
}
