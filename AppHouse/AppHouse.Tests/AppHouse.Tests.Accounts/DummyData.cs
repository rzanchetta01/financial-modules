﻿namespace AppHouse.Accounts.Tests
{
    public static partial class DummyData
    {
        public static AccountDto DummyNewAccountDto { get; } = new AccountDto
        (
        "testName",
        "test@email.com",
        "testPassword",
        "1197264321",
        "1987-12-12",
        "testCountry",
        "testState",
        "testCity",
        "02582123",
        "testAddressWithFakeCountryStateAndCity",
        "testAddressNumber1",
        1000M,
        5.0D,
        null,
        null,
        null
        );

        public static AccountDto DummyExistingActiveAccountDto { get; } = new AccountDto
        (
        "testName",
        "test@email.com",
        "testPassword",
        "1197264321",
        "1987-12-12",
        "testCountry",
        "testState",
        "testCity",
        "02582123",
        "testAddressWithFakeCountryStateAndCity",
        "testAddressNumber1",
        1000M,
        5.0D,
        Guid.NewGuid(),
        DateTime.Parse("2023-12-31"),
        true
        );

        public static AccountDto DummyExistingDisabledAccountDto { get; } = new AccountDto
        (
        "testName",
        "test@email.com",
        "testPassword",
        "1197264321",
        "1987-12-12",
        "testCountry",
        "testState",
        "testCity",
        "02582123",
        "testAddressWithFakeCountryStateAndCity",
        "testAddressNumber1",
        1000M,
        5.0D,
        Guid.NewGuid(),
        DateTime.Parse("2023-12-31"),
        false
        );
    }
}