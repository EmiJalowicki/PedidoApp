-- ============================================================
-- PedidoApp - Creación de base de datos
-- ============================================================

PRAGMA foreign_keys = ON;


-- ============================================================
-- PIZZAS
-- ============================================================

CREATE TABLE IF NOT EXISTS PIZZA (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL UNIQUE,
    Precio NUMERIC NOT NULL,
    EstaActivo INTEGER NOT NULL DEFAULT 1
);


-- ============================================================
-- EMPANADAS
-- ============================================================

CREATE TABLE IF NOT EXISTS EMPANADA (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL UNIQUE,
    Precio NUMERIC NOT NULL,
    EstaActivo INTEGER NOT NULL DEFAULT 1
);


-- ============================================================
-- MILANESAS
-- ============================================================

CREATE TABLE IF NOT EXISTS MILANESA (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL UNIQUE,
    Precio NUMERIC NOT NULL,
    EstaActivo INTEGER NOT NULL DEFAULT 1
);


-- ============================================================
-- BEBIDAS
-- ============================================================

CREATE TABLE IF NOT EXISTS BEBIDA (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    EsAlcoholica INTEGER NOT NULL DEFAULT 0
);


-- ============================================================
-- TAMAÑOS DE BEBIDA
-- ============================================================

CREATE TABLE IF NOT EXISTS TAMANIO_BEBIDA (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Medida TEXT NOT NULL UNIQUE,
	Orden INTEGER NOT NULL
);


-- ============================================================
-- BEBIDA + TAMAÑO
-- ============================================================

CREATE TABLE IF NOT EXISTS BEBIDA_TAMANIO (
    BebidaID INTEGER NOT NULL,
    TamanioBebidaID INTEGER NOT NULL,
    Precio NUMERIC NOT NULL,
    EstaActivo INTEGER NOT NULL DEFAULT 1,

    PRIMARY KEY (BebidaID, TamanioBebidaID),

    FOREIGN KEY (BebidaID)
        REFERENCES BEBIDA(ID),

    FOREIGN KEY (TamanioBebidaID)
        REFERENCES TAMANIO_BEBIDA(ID)
);


-- ============================================================
-- TAMAÑOS DE HELADO
-- ============================================================

CREATE TABLE IF NOT EXISTS TAMANIO_HELADO (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Descripcion TEXT NOT NULL UNIQUE,
	Orden INTEGER NOT NULL,
    Precio NUMERIC NOT NULL,
    EstaActivo INTEGER NOT NULL DEFAULT 1
);

PRAGMA user_version = 1;