namespace AppHouse.Tests
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
        0D,
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

        public static IEnumerable<AccountDto> DummyListOfExistingAccountDto { get; } = new List<AccountDto>()
        {
            new
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
            ),
            new
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
            ),
            new
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
            )
        };

        public static LoanDto DummyNewLoanDto { get; } = new LoanDto
        (
            CreatorAccountId: Guid.NewGuid(),
            MaxAmount: 1000M,
            MinAmount: 100M,
            MaxDateFeasible: "2023-12-31",
            MinDateFeasible: "2023-12-31",  
            LoanStyleType: 1,
            LoanQualityRating: 1,
            LoanDescription: "testDescription",
            Id: null,
            DateCreated: null,
            IsActive: null
        );

        public static LoanDto DummyExistingActiveLoanDto { get; } = new LoanDto
        (
        CreatorAccountId: Guid.NewGuid(),
        MaxAmount: 1000M,
        MinAmount: 100M,
        MaxDateFeasible: "2023-12-31",
        MinDateFeasible: "2023-12-31",
        LoanStyleType: 1,
        LoanQualityRating: 1,
        LoanDescription: "testDescription",
        Id: Guid.NewGuid(),
        DateCreated: DateTime.Parse("2023-12-31"),
        IsActive: true
        );
    }
}
