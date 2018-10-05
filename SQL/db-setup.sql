-- CREATE TABLE smoothies (
--     id int NOT NULL AUTO_INCREMENT, 
--     name VARCHAR(50) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     price DECIMAL(10, 2) NOT NULL,
--     PRIMARY KEY(id)
-- );

-- INSERT INTO smoothies (
--     name, 
--     description, 
--     price) 
--     VALUES (
--         "Mango Sunrise",
--         "Mangos, Ice, Kaluha",
--          5.99
--     );

-- SELECT * FROM smoothies;

-- ALTER TABLE burgers MODIFY COLUMN price DECIMAL(10, 2);

-- UPDATE burgers SET price = 4.99 WHERE id = 1;

-- DELETE FROM burgers WHERE id = 1;


-- CREATE TABLE users (
--     id VARCHAR(225) NOT NULL,
--     username VARCHAR(50) NOT NULL,
--     email VARCHAR(150) NOT NULL,
--     hash VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
--     UNIQUE KEY email (email)
-- );

-- Favorites table i.e., joinable table relational table
CREATE TABLE userburgers (
    id int NOT NULL AUTO_INCREMENT,
    userId VARCHAR(255) NOT NULL,
    burgerId int NOT NULL,

    PRIMARY KEY (id),
    INDEX (userId),

    FOREIGN KEY (userId) -- this says the values are from another table
        REFERENCES users(id) -- this says what table what column
        ON DELETE CASCADE,
    FOREIGN KEY (burgerId)
        REFERENCES burgers(id)
        ON DELETE CASCADE
    
)