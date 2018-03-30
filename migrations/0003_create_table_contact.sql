CREATE TABLE IF NOT EXISTS contact.contact (
  id BIGSERIAL NOT NULL PRIMARY KEY,
  first_name TEXT,
  last_name TEXT,
  created_at TIMESTAMP NOT NULL DEFAULT now(),
  modified_at TIMESTAMP
);