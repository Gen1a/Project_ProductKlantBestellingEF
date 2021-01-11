CREATE TABLE [dbo].[Product_Bestelling] (
    [ProductId]    BIGINT NOT NULL,
    [BestellingId] BIGINT NOT NULL,
    [Aantal]       INT    NOT NULL,
    CONSTRAINT [PK_Product_Bestelling] PRIMARY KEY CLUSTERED ([ProductId] ASC, [BestellingId] ASC),
    CONSTRAINT [FK_Product_Bestelling_Bestelling] FOREIGN KEY ([BestellingId]) REFERENCES [dbo].[Bestelling] ([Id]),
    CONSTRAINT [FK_Product_Bestelling_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

