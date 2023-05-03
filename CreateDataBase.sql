-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema CRMdb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema CRMdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `CRMdb` DEFAULT CHARACTER SET utf8 ;
USE `CRMdb` ;

-- -----------------------------------------------------
-- Table `CRMdb`.`Client`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Client` (
  `ID` INT NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `PhoneNumber` VARCHAR(10) NOT NULL,
  `Email` VARCHAR(45) NULL,
  `Discount` INT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Table`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Table` (
  `ID` INT NOT NULL,
  `Places` INT NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Position`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Position` (
  `ID` INT NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Employee`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Employee` (
  `ID` INT NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Login` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  `IsAdmin` TINYINT NOT NULL,
  `Position_ID` INT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Employee_Position1_idx` (`Position_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Employee_Position1`
    FOREIGN KEY (`Position_ID`)
    REFERENCES `CRMdb`.`Position` (`ID`)
    ON DELETE SET NULL
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Order`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Order` (
  `ID` INT NOT NULL,
  `Price` DECIMAL(9,2) NULL,
  `Discount` INT NULL,
  `OrderDateTime` DATETIME NOT NULL,
  `Client_ID` INT NULL,
  `Table_ID` INT NULL,
  `Employee_ID` INT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Order_Client1_idx` (`Client_ID` ASC) VISIBLE,
  INDEX `fk_Order_Table1_idx` (`Table_ID` ASC) VISIBLE,
  INDEX `fk_Order_Employee1_idx` (`Employee_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Order_Client1`
    FOREIGN KEY (`Client_ID`)
    REFERENCES `CRMdb`.`Client` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_Table1`
    FOREIGN KEY (`Table_ID`)
    REFERENCES `CRMdb`.`Table` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_Employee1`
    FOREIGN KEY (`Employee_ID`)
    REFERENCES `CRMdb`.`Employee` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Product` (
  `ID` INT NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Category` VARCHAR(45) NOT NULL,
  `Amount` INT NOT NULL,
  `LifeTime` INT NOT NULL,
  `DeliveryDateTime` DATETIME NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Dish`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Dish` (
  `ID` INT NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Category` VARCHAR(45) NOT NULL,
  `Mass` DECIMAL(6,3) NULL,
  `Price` DECIMAL(9,2) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Product_Dish`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Product_Dish` (
  `ID` INT NOT NULL,
  `Amount` DECIMAL(6,3) NOT NULL,
  `Product_ID` INT NOT NULL,
  `Dish_ID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Product_Dish_Product1_idx` (`Product_ID` ASC) VISIBLE,
  INDEX `fk_Product_Dish_Dish1_idx` (`Dish_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Product_Dish_Product1`
    FOREIGN KEY (`Product_ID`)
    REFERENCES `CRMdb`.`Product` (`ID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Product_Dish_Dish1`
    FOREIGN KEY (`Dish_ID`)
    REFERENCES `CRMdb`.`Dish` (`ID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Order_Dish`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Order_Dish` (
  `ID` INT NOT NULL,
  `Order_ID` INT NOT NULL,
  `Dish_ID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Order_Dish_Order1_idx` (`Order_ID` ASC) VISIBLE,
  INDEX `fk_Order_Dish_Dish1_idx` (`Dish_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Order_Dish_Order1`
    FOREIGN KEY (`Order_ID`)
    REFERENCES `CRMdb`.`Order` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_Dish_Dish1`
    FOREIGN KEY (`Dish_ID`)
    REFERENCES `CRMdb`.`Dish` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`Order_Dish`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`Order_Dish` (
  `ID` INT NOT NULL,
  `Order_ID` INT NOT NULL,
  `Dish_ID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Order_Dish_Order1_idx` (`Order_ID` ASC) VISIBLE,
  INDEX `fk_Order_Dish_Dish1_idx` (`Dish_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Order_Dish_Order1`
    FOREIGN KEY (`Order_ID`)
    REFERENCES `CRMdb`.`Order` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_Dish_Dish1`
    FOREIGN KEY (`Dish_ID`)
    REFERENCES `CRMdb`.`Dish` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`DailyOrder`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`DailyOrder` (
  `ID` INT NOT NULL,
  `Price` DECIMAL(9,2) NULL,
  `Discount` INT NULL,
  `OrderDateTime` DATETIME NOT NULL,
  `IsReserved` TINYINT NOT NULL,
  `IsDone` TINYINT NOT NULL,
  `Employee_ID` INT NULL,
  `Table_ID` INT NULL,
  `Client_ID` INT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_DailyOrder_Employee1_idx` (`Employee_ID` ASC) VISIBLE,
  INDEX `fk_DailyOrder_Table1_idx` (`Table_ID` ASC) VISIBLE,
  INDEX `fk_DailyOrder_Client1_idx` (`Client_ID` ASC) VISIBLE,
  CONSTRAINT `fk_DailyOrder_Employee1`
    FOREIGN KEY (`Employee_ID`)
    REFERENCES `CRMdb`.`Employee` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_DailyOrder_Table1`
    FOREIGN KEY (`Table_ID`)
    REFERENCES `CRMdb`.`Table` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_DailyOrder_Client1`
    FOREIGN KEY (`Client_ID`)
    REFERENCES `CRMdb`.`Client` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`DailyOrder_Dish`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`DailyOrder_Dish` (
  `ID` INT NOT NULL,
  `Dish_ID` INT NOT NULL,
  `DailyOrder_ID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_DailyOrder_Dish_Dish2_idx` (`Dish_ID` ASC) VISIBLE,
  INDEX `fk_DailyOrder_Dish_DailyOrder1_idx` (`DailyOrder_ID` ASC) VISIBLE,
  CONSTRAINT `fk_DailyOrder_Dish_Dish2`
    FOREIGN KEY (`Dish_ID`)
    REFERENCES `CRMdb`.`Dish` (`ID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_DailyOrder_Dish_DailyOrder1`
    FOREIGN KEY (`DailyOrder_ID`)
    REFERENCES `CRMdb`.`DailyOrder` (`ID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CRMdb`.`DailyOrder_Dish`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CRMdb`.`DailyOrder_Dish` (
  `ID` INT NOT NULL,
  `Dish_ID` INT NOT NULL,
  `DailyOrder_ID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_DailyOrder_Dish_Dish2_idx` (`Dish_ID` ASC) VISIBLE,
  INDEX `fk_DailyOrder_Dish_DailyOrder1_idx` (`DailyOrder_ID` ASC) VISIBLE,
  CONSTRAINT `fk_DailyOrder_Dish_Dish2`
    FOREIGN KEY (`Dish_ID`)
    REFERENCES `CRMdb`.`Dish` (`ID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_DailyOrder_Dish_DailyOrder1`
    FOREIGN KEY (`DailyOrder_ID`)
    REFERENCES `CRMdb`.`DailyOrder` (`ID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
