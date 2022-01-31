// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace DemoApp;

public class PaymentOrder
{
    public string? Sender { get; set; }
    public string? Receiver { get; set; }
    public decimal? Amount { get; set; }

    public override string ToString()
    {
        return $"{Sender} {Receiver} {Amount}";
    }
}
