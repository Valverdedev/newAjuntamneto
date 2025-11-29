-- Database schema for Church domain (ChurchDbContext) + reuse Identity schema when Identity uses the same DB
-- Provider: PostgreSQL
-- Database: churchsuite_church

-- Optional: run db-scripts/identity.sql first if this database also stores Identity tables.

CREATE TABLE IF NOT EXISTS "Tenants" (
    "Id" uuid NOT NULL,
    "CreatedAtUtc" timestamp without time zone NOT NULL DEFAULT (NOW()),
    "UpdatedAtUtc" timestamp without time zone NULL,
    "TenantId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Slug" text NULL,
    CONSTRAINT "PK_ChurchTenants" PRIMARY KEY ("Id"),
    CONSTRAINT "UQ_ChurchTenants_TenantId" UNIQUE ("TenantId")
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Tenants_Slug" ON "Tenants" ("Slug") WHERE "Slug" IS NOT NULL;

CREATE TABLE IF NOT EXISTS "OrganizationalUnits" (
    "Id" uuid NOT NULL,
    "CreatedAtUtc" timestamp without time zone NOT NULL DEFAULT (NOW()),
    "UpdatedAtUtc" timestamp without time zone NULL,
    "TenantId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Type" integer NOT NULL,
    "ParentId" uuid NULL,
    CONSTRAINT "PK_OrganizationalUnits" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_OrganizationalUnits_Tenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES "Tenants" ("TenantId") ON DELETE CASCADE,
    CONSTRAINT "FK_OrganizationalUnits_OrganizationalUnits_ParentId" FOREIGN KEY ("ParentId") REFERENCES "OrganizationalUnits" ("Id") ON DELETE SET NULL
);

CREATE INDEX IF NOT EXISTS "IX_OrganizationalUnits_TenantId" ON "OrganizationalUnits" ("TenantId");
CREATE INDEX IF NOT EXISTS "IX_OrganizationalUnits_ParentId" ON "OrganizationalUnits" ("ParentId");
