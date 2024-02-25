CREATE TABLE [dbo].[storeProducts] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [ItemName]   VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    [Price]       DECIMAL (18)  NOT NULL,
    [Quantity]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

