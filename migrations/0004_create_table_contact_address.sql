CREATE TABLE IF NOT EXISTS contact.contact_address(
  id BIGSERIAL NOT NULL PRIMARY KEY,
  unit INT,
  street TEXT,
  line2 TEXT,
  city TEXT,
  state TEXT,
  zip_code TEXT,
  contact_id BIGINT NOT NULL,
  created_at TIMESTAMP NOT NULL DEFAULT now(),
  modified_at TIMESTAMP,
  FOREIGN KEY (contact_id) REFERENCES users.user(id)
);