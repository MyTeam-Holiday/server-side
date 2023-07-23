-- Switch to the database context
USE `dev.myteam`;

-- Create the AspNetRoles table
CREATE TABLE Roles (
    Id VARCHAR(255) NOT NULL,
    Name VARCHAR(256),
    NormalizedName VARCHAR(256),
    ConcurrencyStamp LONGTEXT,
    PRIMARY KEY (Id));

-- Create the AspNetUsers table
CREATE TABLE Users (
    Id VARCHAR(255) NOT NULL,
    UserName VARCHAR(256),
    NormalizedUserName VARCHAR(256),
    Email VARCHAR(256),
    NormalizedEmail VARCHAR(256),
    EmailConfirmed TINYINT(1) NOT NULL,
    PasswordHash LONGTEXT,
    SecurityStamp LONGTEXT,
    ConcurrencyStamp LONGTEXT,
    PhoneNumber LONGTEXT,
    PhoneNumberConfirmed TINYINT(1) NOT NULL,
    TwoFactorEnabled TINYINT(1) NOT NULL,
    LockoutEnd DATETIME(6),
    LockoutEnabled TINYINT(1) NOT NULL,
    AccessFailedCount INT NOT NULL,
	EmailConfirmationAttempts INT NOT NULL,
    PRIMARY KEY (Id)
);

-- Create the AspNetRoleClaims table
CREATE TABLE AspNetRoleClaims (
    Id INT AUTO_INCREMENT NOT NULL,
    RoleId VARCHAR(255) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    PRIMARY KEY (Id),
    CONSTRAINT FK_AspNetRoleClaims_AspNetRoles_RoleId FOREIGN KEY (RoleId)
        REFERENCES AspNetRoles (Id) ON DELETE CASCADE
);

-- Create the AspNetUserClaims table
CREATE TABLE AspNetUserClaims (
    Id INT AUTO_INCREMENT NOT NULL,
    UserId VARCHAR(255) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    PRIMARY KEY (Id),
    CONSTRAINT FK_AspNetUserClaims_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

-- Create the AspNetUserLogins table
CREATE TABLE AspNetUserLogins (
    LoginProvider VARCHAR(128) NOT NULL,
    ProviderKey VARCHAR(128) NOT NULL,
    ProviderDisplayName LONGTEXT,
    UserId VARCHAR(255) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

-- Create the AspNetUserRoles table
CREATE TABLE UserRoles (
    UserId VARCHAR(255) NOT NULL,
    RoleId VARCHAR(255) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId FOREIGN KEY (RoleId)
        REFERENCES AspNetRoles (Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

-- Create the AspNetUserTokens table
CREATE TABLE AspNetUserTokens (
    UserId VARCHAR(255) NOT NULL,
    LoginProvider VARCHAR(128) NOT NULL,
    Name VARCHAR(128) NOT NULL,
    Value LONGTEXT,
    PRIMARY KEY (UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);