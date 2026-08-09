/*
Script opcional para carga inicial.
*/

USE [DesafioProdutosDb];
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produtos])
BEGIN
    INSERT INTO [dbo].[Produtos]
        ([Nome], [Descricao], [Preco], [DataCadastro])
    VALUES
        (N'Cabo HDMI', N'Cabo HDMI de 2 metros', 39.90, DATEADD(DAY, -12, SYSDATETIME())),
        (N'Cadeira de Escritório', N'Cadeira ergonômica com apoio lombar', 899.90, DATEADD(DAY, -11, SYSDATETIME())),
        (N'Fone de Ouvido', N'Fone de ouvido com fio', 75.00, DATEADD(DAY, -10, SYSDATETIME())),
        (N'Hub USB', N'Hub USB com quatro entradas', 95.00, DATEADD(DAY, -9, SYSDATETIME())),
        (N'Monitor', N'Monitor LED de 24 polegadas', 1200.00, DATEADD(DAY, -8, SYSDATETIME())),
        (N'Mouse sem Fio', N'Mouse com conexão sem fio', 125.00, DATEADD(DAY, -7, SYSDATETIME())),
        (N'Mouse USB', N'Mouse com conexão USB', 50.00, DATEADD(DAY, -6, SYSDATETIME())),
        (N'Notebook', N'Notebook para uso profissional', 3500.00, DATEADD(DAY, -5, SYSDATETIME())),
        (N'SSD 480 GB', N'Unidade de armazenamento SSD', 320.00, DATEADD(DAY, -4, SYSDATETIME())),
        (N'Suporte para Notebook', N'Suporte ajustável para notebook', 140.00, DATEADD(DAY, -3, SYSDATETIME())),
        (N'Teclado Mecânico', N'Teclado mecânico com conexão USB', 250.00, DATEADD(DAY, -2, SYSDATETIME())),
        (N'Webcam', N'Webcam com resolução Full HD', 180.00, DATEADD(DAY, -1, SYSDATETIME()));

    PRINT 'Produtos de exemplo inseridos com sucesso.';
END
ELSE
BEGIN
    PRINT 'A tabela Produtos já possui registros. Nenhum dado foi inserido.';
END;
GO