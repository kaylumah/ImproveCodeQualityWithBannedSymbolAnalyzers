
// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using DemoApp;

// IFeeCalculator calculator = new FeeCalculator();
// IFeeCalculator calculator = new DatedFeeCalculator();
IFeeCalculator calculator = new SystemClockFeeCalculator(new SystemClock());

var queue = new PriorityQueue<PaymentOrder, int>();

while (true)
{
    Console.WriteLine("Enter 'done' to stop");
    if ("done".Equals(Console.ReadLine()))
    {
        break;
    }

#pragma warning disable CS8604 // Possible null reference argument.
    Console.WriteLine("Enter Sender");
    var sender = Console.ReadLine();
    Console.WriteLine("Enter Receiver");
    var receiver = Console.ReadLine();
    Console.WriteLine("Enter Amount");
    var amount = decimal.Parse(Console.ReadLine());
    Console.WriteLine("Enter IsPriority");
    var priority = bool.Parse(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.

    var fee = calculator.Calculate(amount, priority);
    queue.Enqueue(new PaymentOrder {
        Sender = sender,
        Receiver = receiver,
        Amount = Math.Round(amount - fee, 2),
    }, priority ? 0 : 100);
    queue.Enqueue(new PaymentOrder {
        Sender = sender,
        Receiver = "FEE Account",
        Amount = Math.Round(fee, 2)
    }, 50);
}

while(queue.TryDequeue(out var item, out var priority))
{
    Console.WriteLine("BankOrder '{0}' has priority {1}", item, priority);
}

Console.WriteLine("Press enter to close");
Console.ReadLine();
