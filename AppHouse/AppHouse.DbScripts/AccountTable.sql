CREATE TABLE IF NOT EXISTS account (
    id UUID PRIMARY KEY,
    date_created TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT true,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    cellphone VARCHAR(20) NOT NULL,
    birth_date DATE NOT NULL,
    country VARCHAR(100) NOT NULL,
    state VARCHAR(100) NOT NULL,
    city VARCHAR(100) NOT NULL,
    postal_code VARCHAR(20) NOT NULL,
    address TEXT NOT NULL,
    address_complement TEXT,
    income NUMERIC(18, 2) NOT NULL,
    credit_score DOUBLE PRECISION NOT NULL
);
