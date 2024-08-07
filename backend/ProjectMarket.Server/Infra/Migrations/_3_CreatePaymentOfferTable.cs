using FluentMigrator;
using FluentMigrator.SqlServer;

namespace ProjectMarket.Server.Infra.Migrations;

[Migration(3)]
public class _3_CreatePaymentOfferTable(IConfiguration? configuration = null) : Migration {
    public override void Up()
	{
        // MaxValue = 1_000_000_000_000_000.00
        const int valueMaxSize = 17;
        const int valuePrecision = 2;

        Create.Table("PaymentOffer")
            .WithColumn("PaymentOfferId").AsInt32().PrimaryKey("pk_payment_offer").Identity().NotNullable()
            .WithColumn("Value").AsDecimal(valueMaxSize,valuePrecision).NotNullable() 
            .WithColumn("PaymentFrequencyName").AsString(32).NotNullable()
            .WithColumn("CurrencyName").AsString(64).NotNullable();

        Create.ForeignKey("fk_payment_offer_payment_frequency")
            .FromTable("PaymentOffer").ForeignColumn("PaymentFrequencyName")
            .ToTable("PaymentFrequency").PrimaryColumn("PaymentFrequencyName");

        Create.ForeignKey("fk_payment_offer_currency")
            .FromTable("PaymentOffer").ForeignColumn("CurrencyName")
            .ToTable("Currency").PrimaryColumn("CurrencyName");

        if(configuration?.GetValue<bool>("Database:UseSeedData") ?? false) {
            DeleteAllRows();

            Insert.IntoTable("PaymentOffer")
                .Row(new { Value = 20.00M, PaymentFrequencyName = "Hourly", CurrencyName = "Dollar" });
            Insert.IntoTable("PaymentOffer")
                .Row(new { Value = 200.00M, PaymentFrequencyName = "Daily", CurrencyName = "Dollar" });
            Insert.IntoTable("PaymentOffer")
                .Row(new { Value = 3500.00M, PaymentFrequencyName = "Once", CurrencyName = "Euro" });
            Insert.IntoTable("PaymentOffer")
                .Row(new { Value = 50000.00M, PaymentFrequencyName = "Hourly", CurrencyName = "Yen" });
            Insert.IntoTable("PaymentOffer")
                .Row(new { Value = 750000.00M, PaymentFrequencyName = "Once", CurrencyName = "Yen" });
        }
	}

	public override void Down()
	{   
        DeleteAllRows();

        Delete.ForeignKey("fk_payment_offer_payment_frequency").OnTable("PaymentOffer");
        Delete.ForeignKey("fk_payment_offer_currency").OnTable("PaymentOffer");
        Delete.Table("PaymentOffer").IfExists();
	}

    public void DeleteAllRows() {
        Delete.FromTable("PaymentOffer").AllRows();
    }
}