CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

CREATE TABLE "Importacoes" (
    "Id" BLOB NOT NULL CONSTRAINT "PK_Importacoes" PRIMARY KEY,
    "NomeArquivo" TEXT NULL,
    "DataImportacao" TEXT NOT NULL
);

CREATE TABLE "Moedas" (
    "Id" BLOB NOT NULL CONSTRAINT "PK_Moedas" PRIMARY KEY,
    "Nome" TEXT NULL,
    "Compra" TEXT NOT NULL,
    "Venda" TEXT NOT NULL,
    "Variacao" TEXT NOT NULL,
    "DataCotacao" TEXT NOT NULL
);

CREATE TABLE "Movimentacoes" (
    "Id" BLOB NOT NULL CONSTRAINT "PK_Movimentacoes" PRIMARY KEY,
    "DataMovimento" TEXT NOT NULL,
    "Descricao" TEXT NULL,
    "Valor" TEXT NOT NULL,
    "SaldoAtual" TEXT NOT NULL,
    "ImportacaoId" BLOB NULL,
    CONSTRAINT "FK_Movimentacoes_Importacoes_ImportacaoId" FOREIGN KEY ("ImportacaoId") REFERENCES "Importacoes" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Movimentacoes_ImportacaoId" ON "Movimentacoes" ("ImportacaoId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190823191303_Init', '2.2.6-servicing-10079');