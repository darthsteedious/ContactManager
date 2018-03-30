CREATE TABLE IF NOT EXISTS users.user(
  id BIGSERIAL NOT NULL UNIQUE,
  first_name TEXT NOT NULL,
  last_name TEXT NOT NULL,
  email TEXT NOT NULL PRIMARY KEY,
  created_at TIMESTAMP NOT NULL DEFAULT now(),
  modified_at TIMESTAMP
);