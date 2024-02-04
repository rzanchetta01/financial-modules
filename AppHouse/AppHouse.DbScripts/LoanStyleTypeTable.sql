CREATE TABLE IF NOT EXISTS loan_style_type (
    id SERIAL PRIMARY KEY,
    style VARCHAR(50) NOT NULL
);

INSERT INTO loan_style_type (style) VALUES 
    ('WEEKLY'),
    ('MONTHLY');