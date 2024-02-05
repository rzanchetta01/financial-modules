CREATE TABLE IF NOT EXISTS loan (
	id UUID PRIMARY KEY,
    date_created TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT true,
    creator_account_id UUID NOT NULL REFERENCES account(id),
    max_Amount DECIMAL NOT NULL,
    min_Amount DECIMAL NOT NULL,
    max_date_feasible DATE NOT NULL,
    min_date_feasible DATE NOT NULL,
    loan_style_type INTEGER NOT NULL REFERENCES loan_style_type(id),
    loan_quality_rating INTEGER NOT NULL,
    loan_description TEXT
);