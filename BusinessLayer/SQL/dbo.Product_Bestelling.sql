CREATE TABLE [dbo].[Product_Bestelling] (
    [productId]    BIGINT NOT NULL,
    [bestellingId] BIGINT NOT NULL,
    [aantal] INT NOT NULL, 
    CONSTRAINT [PK_Product_Bestelling] PRIMARY KEY CLUSTERED ([productId] ASC, [bestellingId] ASC),
    CONSTRAINT [FK_Product_Bestelling_Bestelling] FOREIGN KEY ([bestellingId]) REFERENCES [dbo].[Bestelling] ([Id]),
    CONSTRAINT [FK_Product_Bestelling_Product] FOREIGN KEY ([productId]) REFERENCES [dbo].[Product] ([Id])
);

