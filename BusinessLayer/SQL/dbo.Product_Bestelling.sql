CREATE TABLE [dbo].[Product_Bestelling]
(
	[productId] BIGINT NOT NULL, 
    [bestellingId] BIGINT NOT NULL, 
    CONSTRAINT [FK_Product_Bestelling_Product] FOREIGN KEY ([productId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [FK_Product_Bestelling_Bestelling] FOREIGN KEY ([bestellingId]) REFERENCES [Bestelling]([Id]), 
    PRIMARY KEY CLUSTERED ([productId], [bestellingId])  
)
