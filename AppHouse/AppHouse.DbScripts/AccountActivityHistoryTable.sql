CREATE TABLE IF NOT EXISTS account_activity_history (
	id UUID PRIMARY KEY,
    date_created TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT true,
    account_id UUID NOT NULL REFERENCES account(id),
    loan_id UUID NOT NULL REFERENCES loan(id),
    is_receiver BOOLEAN NOT NULL
);