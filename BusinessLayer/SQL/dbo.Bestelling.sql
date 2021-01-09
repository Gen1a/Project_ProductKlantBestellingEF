CREATE TABLE [dbo].[Bestelling] (
    [Id]      BIGINT   IDENTITY (1, 1) NOT NULL,
    [KlantId] BIGINT   NOT NULL,
    [Datum]   DATETIME NOT NULL,
    [Betaald] BIT      NOT NULL,
    [Prijs] DECIMAL(18, 2) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bestelling_Klant] FOREIGN KEY ([KlantId]) REFERENCES [dbo].[Klant] ([Id])
);

