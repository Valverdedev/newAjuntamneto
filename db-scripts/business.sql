-- Database schema for Business domain (BusinessDbContext)
-- Provider: PostgreSQL
-- Database: churchsuite_business

CREATE TABLE IF NOT EXISTS "Plans" (
    "Id" uuid NOT NULL,
    "CreatedAtUtc" timestamp without time zone NOT NULL DEFAULT (NOW()),
    "UpdatedAtUtc" timestamp without time zone NULL,
    "Name" text NOT NULL,
    "Price" numeric(18,2) NOT NULL,
    "Currency" varchar(10) NOT NULL DEFAULT 'USD',
    "BillingCycleInDays" integer NOT NULL DEFAULT 30,
    CONSTRAINT "PK_Plans" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Customers" (
    "Id" uuid NOT NULL,
    "CreatedAtUtc" timestamp without time zone NOT NULL DEFAULT (NOW()),
    "UpdatedAtUtc" timestamp without time zone NULL,
    "Name" text NOT NULL,
    "Email" text NOT NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Customers_Email" ON "Customers" ("Email");

CREATE TABLE IF NOT EXISTS "Subscriptions" (
    "Id" uuid NOT NULL,
    "CreatedAtUtc" timestamp without time zone NOT NULL DEFAULT (NOW()),
    "UpdatedAtUtc" timestamp without time zone NULL,
    "TenantId" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "PlanId" uuid NOT NULL,
    "StartedOnUtc" timestamp without time zone NOT NULL DEFAULT (NOW()),
    "ExpiresOnUtc" timestamp without time zone NULL,
    "Active" boolean NOT NULL DEFAULT TRUE,
    CONSTRAINT "PK_Subscriptions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Subscriptions_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Subscriptions_Plans_PlanId" FOREIGN KEY ("PlanId") REFERENCES "Plans" ("Id") ON DELETE RESTRICT
);

CREATE INDEX IF NOT EXISTS "IX_Subscriptions_CustomerId" ON "Subscriptions" ("CustomerId");
CREATE INDEX IF NOT EXISTS "IX_Subscriptions_PlanId" ON "Subscriptions" ("PlanId");
CREATE INDEX IF NOT EXISTS "IX_Subscriptions_TenantId" ON "Subscriptions" ("TenantId");

CREATE TABLE IF NOT EXISTS "Tenants" (
    "Id" uuid NOT NULL,
    "CreatedAtUtc" timestamp without time zone NOT NULL DEFAULT (NOW()),
    "UpdatedAtUtc" timestamp without time zone NULL,
    "TenantId" uuid NOT NULL,
    "DisplayName" text NOT NULL,
    "CustomerId" uuid NOT NULL,
    "SubscriptionId" uuid NOT NULL,
    CONSTRAINT "PK_Tenants" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Tenants_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Tenants_Subscriptions_SubscriptionId" FOREIGN KEY ("SubscriptionId") REFERENCES "Subscriptions" ("Id") ON DELETE RESTRICT
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Tenants_TenantId" ON "Tenants" ("TenantId");
CREATE INDEX IF NOT EXISTS "IX_Tenants_CustomerId" ON "Tenants" ("CustomerId");
CREATE INDEX IF NOT EXISTS "IX_Tenants_SubscriptionId" ON "Tenants" ("SubscriptionId");
