CREATE TABLE [dbo].Bestelling
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [klantId] BIGINT NOT NULL, 
    [datum] DATETIME NOT NULL, 
    [betaald] BIT NOT NULL, 
    CONSTRAINT [FK_Bestelling_Klant] FOREIGN KEY ([klantId]) REFERENCES [Klant]([Id])
)
