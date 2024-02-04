CREATE TABLE IF NOT EXISTS account_activity_history (
	id UUID PRIMARY KEY,
    date_created TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT true,
    account_id UUID NOT NULL REFERENCES accounts(id),
    loan_id UUID NOT NULL REFERENCES loans(id),
    was_paid_correctly BOOLEAN NOT NULL
);