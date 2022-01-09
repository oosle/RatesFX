/******************************************************************************************************************/
/* SQL SERVER - FX rates database schema script.                                                                  */
/* Matthew Lewin v1.0                                                                                             */
/******************************************************************************************************************/

USE [RatesDatabase]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/* Add the default windows SYSTEM user to the database to allow windows task to run correctly */
IF NOT EXISTS (SELECT [name] FROM [sys].[database_principals] WHERE [type] = 'S' AND [name] = 'NT AUTHORITY\SYSTEM')
BEGIN
    CREATE USER [NT AUTHORITY\SYSTEM] 
    FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
END

ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO

IF (EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RatesBase'))
BEGIN
    DROP TABLE RatesBase;
END

/* Base currency for this exchange rate record */
CREATE TABLE RatesBase (
    id       INT NOT NULL IDENTITY (1, 1) PRIMARY KEY,
    currency VARCHAR(50),
    trade    BIT,
    country  INT NOT NULL,
    created  DATETIME,
    c_user   VARCHAR(100),
    updated  DATETIME,
    u_user   VARCHAR(100),
    deleted  DATETIME,
    d_user   VARCHAR(100)
);

CREATE NONCLUSTERED INDEX IX_Created ON RatesBase (created ASC);
CREATE NONCLUSTERED INDEX IX_Currency ON RatesBase (currency ASC);
CREATE NONCLUSTERED INDEX IX_Trade ON RatesBase (trade ASC);

IF (EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RatesCurrency'))
BEGIN
    DROP TABLE RatesCurrency;
END

/* The group of exchange rate records for the base currency */
CREATE TABLE RatesCurrency (
    id       INT NOT NULL IDENTITY (1, 1) PRIMARY KEY,
    base_id  INT NOT NULL,
    currency VARCHAR(50) NOT NULL,
    value    FLOAT NOT NULL,
    country  INT NOT NULL,
    created  DATETIME,
    c_user   VARCHAR(100),
    updated  DATETIME,
    u_user   VARCHAR(100),
    deleted  DATETIME,
    d_user   VARCHAR(100)
);

CREATE NONCLUSTERED INDEX IX_BaseId ON RatesCurrency (base_id ASC);
CREATE NONCLUSTERED INDEX IX_Currency ON RatesCurrency (currency ASC);
CREATE NONCLUSTERED INDEX IX_Value ON RatesCurrency (value ASC);
CREATE NONCLUSTERED INDEX IX_Created ON RatesCurrency (created ASC);

IF (EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RatesLookup'))
BEGIN
    DROP TABLE RatesLookup;
END

/* Rates lookup table to enhance the dataset for downloaded rates data */
CREATE TABLE RatesLookup (
    id       INT NOT NULL IDENTITY (1, 1) PRIMARY KEY,
    code     VARCHAR(50) NOT NULL,
    currency VARCHAR(100) NOT NULL,
    country  VARCHAR(50) NOT NULL,
    created  DATETIME,
    c_user   VARCHAR(100),
    updated  DATETIME,
    u_user   VARCHAR(100),
    deleted  DATETIME,
    d_user   VARCHAR(100)
);

CREATE NONCLUSTERED INDEX IX_Code ON RatesLookup (code ASC);
CREATE NONCLUSTERED INDEX IX_Currency ON RatesLookup (currency ASC);
CREATE NONCLUSTERED INDEX IX_Country ON RatesLookup (country ASC);
CREATE NONCLUSTERED INDEX IX_Created ON RatesLookup (created ASC);

INSERT INTO RatesLookup VALUES ('AUD','Dollar','Australia',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('BGN','Lev','Bulgaria',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('BRL','Real','Brazil',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('CAD','Dollar','Canada',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('CHF','Franc','Switzerland',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('CNY','Yuan','China',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('CZK','Koruna','Czech Republic',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('DKK','Krone','Denmark',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('EUR','Euro','European Union',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('GBP','Pound','United Kingdom',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('HKD','Dollar','Hong Kong',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('HRK','Kuna','Croatia',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('HUF','Forint','Hungary',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('IDR','Rupiah','Indonesia',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('ILS','Sheqel','Israel',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('INR','Rupee','India',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('ISK','Krona','Iceland',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('JPY','Yen','Japan',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('KRW','Won','South Korea',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('MXN','Peso','Mexico',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('MYR','Ringgit','Malaysia',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('NOK','Krone','Norway',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('NZD','Dollar','New Zealand',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('PHP','Peso','Philippines',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('PLN','Zloty','Poland',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('RON','Leu','Romania',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('RUB','Ruble','Russia',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('SEK','Krona','Sweden',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('SGD','Dollar','Singapore',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('THB','Baht','Thailand',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('TRY','Lira','Turkey',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('USD','Dollar','United States',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);
INSERT INTO RatesLookup VALUES ('ZAR','Rand','South Africa',GETDATE(),'SYSTEM',NULL,NULL,NULL,NULL);

/*
The RatesLookup lookup table can be used to enhance the exchange rate data with queries like the following:

SELECT
    rb.Created AS CreatedDate,
    rb.Currency AS BaseCode,
	(rl1.Country + ' - ' + rl1.Currency) AS BaseCurrency,
	rc.Currency AS RateCode,
	(rl2.Country + ' - ' + rl2.Currency) AS RateCurrency,
	rc.Value AS ExchangeRate
FROM RatesBase rb
LEFT OUTER JOIN RatesCurrency rc ON rb.id = rc.base_id
INNER JOIN RatesLookup rl1 ON rb.country = rl1.id
INNER JOIN RatesLookup rl2 ON rc.country = rl2.id
WHERE (rb.Currency = 'EUR')
*/
